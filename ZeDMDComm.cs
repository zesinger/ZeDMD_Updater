using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace ZeDMD
{
	public class ZeDMDComm // Class for locating the COM port of the ESP32 and communicating with it
	{
		public string nCOM;
		public const int BaudRate = 921600;
		public bool Opened = false;
		private SerialPort _serialPort;
		private const int MAX_SERIAL_WRITE_AT_ONCE = 256;
		public const int N_CTRL_CHARS = 6;
		public const int N_INTERMEDIATE_CTR_CHARS = 4;
		private const bool COMPRESSION_ACTIVE = false;
		public static readonly byte[] CtrlCharacters = { 0x5a, 0x65, 0x64, 0x72, 0x75, 0x6d };
		private string _portName;

		private void SafeClose()
		{
			try
			{
				// In case of error discard serial data and close
				//_serialPort.DiscardInBuffer();
				//_serialPort.DiscardOutBuffer();
				_serialPort.Close();
				System.Threading.Thread.Sleep(100); // otherwise the next device will fail
			}
			catch { };
		}
		public bool IsReady()
		{
			try
			{
				byte[] answerbuffer=new byte[1];
				_serialPort.Read(answerbuffer, 0, 1);
				if (answerbuffer[0] != 'R') return false;
			}
			catch { return false; }
			return true;
		}
		private void FreeReadBuffer()
		{
			while (_serialPort.BytesToRead > 0)
			{
				int getval=_serialPort.ReadByte();
			}
		}
		public bool SendFunctionGet(byte function,int lenanswer, ref byte[] answerbuffer)
		{
			FreeReadBuffer();
			try
			{
				_serialPort.Write(CtrlCharacters.Concat(new byte[] { function }).ToArray(), 0, CtrlCharacters.Length + 1);
				if (lenanswer>0)
				{
					System.Threading.Thread.Sleep(100);
					_serialPort.Read(answerbuffer, 0, lenanswer);
					System.Threading.Thread.Sleep(100);
				}
			}
			catch
			{
				if (_serialPort != null && _serialPort.IsOpen) SafeClose();
				return false;
			}
			return true;
		}
		public bool SendFunctionSet(byte function,int lenval, byte[] valbuffer)
		{
			FreeReadBuffer();
			try
			{
				byte[] bytes = new byte[lenval + 1];
				bytes[0] = function;
				for (int ti = 0; ti < lenval; ti++) bytes[1 + ti] = valbuffer[ti];
				byte[] sendbyte=CtrlCharacters.Concat(bytes).ToArray();
				_serialPort.Write(sendbyte, 0, sendbyte.Length);
				System.Threading.Thread.Sleep(100);
			}
			catch
			{
				if (_serialPort != null && _serialPort.IsOpen) SafeClose();
				return false;
			}
			return true;
		}
		public bool Connect(string port, out int width, out int height)
		{
			// Try to find an ESP32 on the COM port and check if it answers with the shake-hand bytes
			try
			{
				_serialPort = new SerialPort(port, BaudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
				{
					ReadTimeout = 100,
					WriteBufferSize = MAX_SERIAL_WRITE_AT_ONCE,
					WriteTimeout = 100//SerialPort.InfiniteTimeout
				};
				_serialPort.Open();
				_serialPort.Write(CtrlCharacters.Concat(new byte[] { 12 }).ToArray(), 0, CtrlCharacters.Length + 1);
				System.Threading.Thread.Sleep(200);
				var result = new byte[Math.Max(N_CTRL_CHARS + 1, N_INTERMEDIATE_CTR_CHARS + 4)];
				_serialPort.Read(result, 0, N_INTERMEDIATE_CTR_CHARS + 4);
				System.Threading.Thread.Sleep(200);
				if (!result.Take(4).SequenceEqual(CtrlCharacters.Take(4)))
				{
					SafeClose();
					width = 0;
					height = 0;
					return false;
				}
				width = result[N_INTERMEDIATE_CTR_CHARS] + result[N_INTERMEDIATE_CTR_CHARS + 1] * 256;
				height = result[N_INTERMEDIATE_CTR_CHARS + 2] + result[N_INTERMEDIATE_CTR_CHARS + 3] * 256;
				nCOM = port;
				Opened = true;
				return true;

			}
			catch
			{
				if (_serialPort != null && _serialPort.IsOpen) SafeClose();
			}
			width = 0;
			height = 0;
			return false;
		}
		private void Disconnect()
		{
			Opened = false;
			if (_serialPort != null) SafeClose();
		}
		
		public bool StreamBytes(byte[] pBytes, int nBytes)
		{
			double accomp = 2;
			if ((nBytes > 1000) && COMPRESSION_ACTIVE)
			{
				byte[] outputBuffer;

				using (MemoryStream inputStream = new MemoryStream(pBytes, 1, nBytes - 1)) // we don't want the mode byte to be compressed
				{
					using (MemoryStream outputStream = new MemoryStream())
					{
						using (DeflateStream compressionStream = new DeflateStream(outputStream, CompressionLevel.Optimal, true))
						{
							inputStream.CopyTo(compressionStream);
						}
						outputBuffer = outputStream.ToArray();
						accomp = (float)(outputBuffer.Length + 5) / (float)nBytes;
					}
				}
				if (accomp < 1.0f)
				{
					pBytes[0] |= 0x80; // if we send compressed data, we set the bit 7 of the mode byte
					pBytes[1] = (byte)((outputBuffer.Length >> 24) & 0xFF); // and the 4 bytes after the mode
					pBytes[2] = (byte)((outputBuffer.Length >> 16) & 0xFF); // are giving the length
					pBytes[3] = (byte)((outputBuffer.Length >> 8) & 0xFF); // of the compressed data
					pBytes[4] = (byte)(outputBuffer.Length & 0xFF);
					Buffer.BlockCopy(outputBuffer, 0, pBytes, 5, outputBuffer.Length);
					nBytes = outputBuffer.Length + 5;
				}
			}

			if (_serialPort.IsOpen)
			{
				try
				{
					if (_serialPort.ReadByte() == 'R')
					{
						// first send
						byte[] pBytes2 = new byte[MAX_SERIAL_WRITE_AT_ONCE];
						for (int ti = 0; ti < N_CTRL_CHARS; ti++) pBytes2[ti] = CtrlCharacters[ti];
						int tosend = (nBytes < MAX_SERIAL_WRITE_AT_ONCE - N_CTRL_CHARS) ? nBytes : (MAX_SERIAL_WRITE_AT_ONCE - N_CTRL_CHARS);
						for (int ti = 0; ti < tosend; ti++) pBytes2[ti + N_CTRL_CHARS] = pBytes[ti];
						_serialPort.Write(pBytes2, 0, tosend + N_CTRL_CHARS);
						if (_serialPort.ReadByte() != 'A') return false;
						// next ones
						int bufferPosition = tosend;
						while (bufferPosition < nBytes)
						{
							tosend = (nBytes - bufferPosition < MAX_SERIAL_WRITE_AT_ONCE) ? nBytes - bufferPosition : MAX_SERIAL_WRITE_AT_ONCE;
							_serialPort.Write(pBytes, bufferPosition, tosend);
							if (_serialPort.ReadByte() == 'A')
							{
								// Received (A)cknowledge, ready to send the next chunk.
								bufferPosition += tosend;
							}
							else
							{
								// Something went wrong. Terminate current transmission of the buffer and return.
								return false;
							}
						}
						return true;
					}
				}
				catch
				{
					return false;
				}
			}
			return false;
		}
		public void ResetPalettes()
		{
			// Reset ESP32 palette
			byte[] tempbuf = new byte[1];
			tempbuf[0] = 0x6;  // command byte 6 = reset palettes
			StreamBytes(tempbuf, 1);
		}
		public void SetRGBOrderAndBrightness(int COMnb,byte brightness,byte rgborder)
		{
			int width=0, height=0;
            System.Threading.Thread.Sleep(1000); // needed after flash before reconnecting
			bool IsAvailable = Connect("COM"+COMnb.ToString(), out width, out height);
			byte[] data=new byte[1];
			if (IsAvailable)
			{
				data[0]=brightness;
				SendFunctionSet(22, 1, data);
				data[0]=rgborder;
				SendFunctionSet(23, 1, data);
                SendFunctionSet(30, 0, data); 
				FreeReadBuffer();
			}
			Close();
		}
		public void Open(out int width, ref int[] pheight, out int ncoms,ref int[] coms, ref byte[] vmaj,ref byte[] vmin,ref byte[] vpat, ref byte[] bright, ref byte[] rgbord)
		{
			// Try to find an ZeDMD on each COM port available
			bool IsAvailable = false;
			ncoms=0;
			var ports = SerialPort.GetPortNames();
			width = 0;
			int height = 0;
			for (uint ti=0; ti<64; ti++) coms[ti]=0;
			foreach (var portName in ports)
			{
				IsAvailable = Connect(portName, out width, out height);
				_portName = portName;
				if (IsAvailable&&(ncoms<64))
				{
					while (IsReady() == false) ;
					byte[] retdata = new byte[8];
                    if (SendFunctionGet(32, 3, ref retdata) == false) return;
					vmaj[ncoms] = retdata[0];
                    vmin[ncoms] = retdata[1];
                    vpat[ncoms] = retdata[2];
                    if (SendFunctionGet(24, 1, ref retdata) == false) return;
					bright[ncoms]=retdata[0];
                    if (SendFunctionGet(25, 1, ref retdata) == false) return;
					rgbord[ncoms]=retdata[0];
                    pheight[ncoms] = height;
				    Match match = Regex.Match(portName, @"COM(\d+)");
					if (match.Success)
					{
						string comNumberString = match.Groups[0].Value;
						coms[ncoms]=int.Parse(comNumberString.Substring(3));
						ncoms++;
					}
					SendFunctionSet(31, 0, retdata); 
				}
				Close();
			}
		}

		public bool Close()
		{
			if (Opened)
			{
				Disconnect();
				ResetPalettes();
			}

			Opened = false;
			return true;
		}

	}
}

