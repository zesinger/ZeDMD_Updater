using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using ZeDMD;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using System.Threading;
using System.Runtime.CompilerServices;

namespace ZeDMD_Updater
{
    public partial class Form1 : Form
    {
 		private const int MAJ_VERSION=1;
        private const int MIN_VERSION=1;

        public static readonly byte[] CtrlCharacters = { 0x5a, 0x65, 0x64, 0x72, 0x75, 0x6d };
        const int MAX_VERSIONS_TO_LIST=64;
        ZeDMDComm zdc=new ZeDMDComm(); 
        int[] ESP32COMs=new int[64];
        int nESP32COMs=0;
        int[] ZeDMDCOMs=new int[64];
        int[] ZeDMDH = new int[64];
        byte[] majVersion = new byte[64];
        byte[] minVersion = new byte[64];
        byte[] patVersion = new byte[64];
        byte[] brightness = new byte[64];
        byte[] RGBorder = new byte[64];
        byte[] avMVersion=new byte[MAX_VERSIONS_TO_LIST];
        byte[] avmVersion=new byte[MAX_VERSIONS_TO_LIST];
        byte[] avpVersion=new byte[MAX_VERSIONS_TO_LIST];
        byte navVersions=0;
        byte avmajVersion=0;
        byte avminVersion=0;
        byte avpatVersion = 0;
        int nZeDMDCOMs =0;
        int selCOM=0;
        bool is64=false;
        bool isFlashing=false;
        ManagementEventWatcher watcher;
        private int IsZeDMD(int nocom)
        {
            for (int ti=0;ti<nZeDMDCOMs;ti++)
            {
                if (ZeDMDCOMs[ti]==nocom) { return ti; }
            }
            return -1;
        }
        private void GetPortNames()
        {
            nESP32COMs = 0;
            using (var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();
            
                for (int tj=0;tj<portList.Count;tj++)
                {
                    if (portList[tj].Contains("CP210x")||portList[tj].Contains("CH340"))
                    {
				        Match match = Regex.Match(portList[tj], @"COM(\d+)");
					    if (match.Success)
					    {
						    string comNumberString = match.Groups[0].Value;
						    ESP32COMs[nESP32COMs]=int.Parse(comNumberString.Substring(3));
						    nESP32COMs++;
					    }
                    }
                }
            }
        }
        private void UpdateStartButton()
        {
            if (((ESP32List.SelectedIndex<0) && (ZeDMDList.SelectedIndex<0)) || (versionList.SelectedIndex<0))
                { Start.Enabled=false; Start.Text=""; return; }
            if (ESP32List.SelectedIndex>=0)
                 { Start.Enabled=true; Start.Text="Install"; return; }
            int versionsel=navVersions - versionList.SelectedIndex - 1;
            int zedmdsel=ZeDMDList.SelectedIndex;
            if (((ZeDMDH[zedmdsel]==32) && (is64==true)) || ((ZeDMDH[zedmdsel]==64) && (is64==false)))
                { Start.Enabled=true; Start.Text="Install"; return; }
            if ((majVersion[zedmdsel]!=avMVersion[versionsel]) || (minVersion[zedmdsel]!=avmVersion[versionsel]) || (patVersion[zedmdsel]!=avpVersion[versionsel]))
                { Start.Enabled=true; Start.Text="Update"; return; }
            Start.Text="";
            Start.Enabled=false;
        }
        private void PopulateESP(bool settext)
        {
            if (settext)
            {
                ESP32List.Items.Clear();
                ESP32List.Items.Add("Please wait...");
                ZeDMDList.Items.Clear();
                ZeDMDList.Items.Add("... updating");
            }
            Start.Enabled = false;
            nESP32COMs=0;
            nZeDMDCOMs=0;
            GetPortNames();
            int width=128;
			for (uint ti=0;ti<64;ti++) ZeDMDCOMs[ti]=0;
            zdc.Open(out width, ref ZeDMDH,out nZeDMDCOMs,ref ZeDMDCOMs,ref majVersion,ref minVersion,ref patVersion,ref brightness,ref RGBorder);
            ESP32List.Items.Clear();
            ZeDMDList.Items.Clear();
            for (uint ti=0; ti<nESP32COMs; ti++)
            {
                int zedmdpos = IsZeDMD(ESP32COMs[ti]);
                if (zedmdpos>=0)
                {
                    string reso = "ZeDMD";
                    if (majVersion[zedmdpos]==82) majVersion[zedmdpos]=0;
                    if (ZeDMDH[zedmdpos] == 64) reso = "ZeDMD HD";
                    if (majVersion[zedmdpos]!=0) ZeDMDList.Items.Add("COM" + ESP32COMs[ti].ToString() + ": "+reso+" v" + majVersion[zedmdpos].ToString() + "." + minVersion[zedmdpos].ToString() + "." + patVersion[zedmdpos].ToString());
                    else ZeDMDList.Items.Add("COM" + ESP32COMs[ti].ToString() + ": "+reso+" <v3.2.3");
                }
                else ESP32List.Items.Add("COM" + ESP32COMs[ti].ToString());
            }
            ZeDMDList.DrawItem += (sender, e) =>
            {
                if (e.Index < 0 || e.Index >= ZeDMDList.Items.Count)
                    return;

                Rectangle bounds = e.Bounds;

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    // Item is selected, set custom background color
                    using (Brush brush = new SolidBrush(Color.LightGray))
                    {
                        e.Graphics.FillRectangle(brush, bounds);
                    }
                }
                else
                {
                    // Item is not selected, use default background color
                    e.DrawBackground();
                }
                string itemText = ZeDMDList.Items[e.Index].ToString();
                if (!isFlashing)
                {
                    if ((majVersion[e.Index]==avmajVersion)&&(minVersion[e.Index]==avminVersion)&&(patVersion[e.Index]==avpatVersion))
                    {
                        // Get the text and background color for the current item
                        e.Graphics.DrawString(itemText, e.Font, Brushes.Green, e.Bounds);
                    }
                    else
                    {
                        // Get the text and background color for the current item
                        e.Graphics.DrawString(itemText, e.Font, Brushes.Red, e.Bounds);
                    }
                }
                else e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds);
            };
        }

        private bool IsUrlAvailable(string url, int timeoutMilliseconds)
        {
            bool isAvailable = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            // Create a timer to handle the timeout
            using (var timer = new System.Threading.Timer(state => request.Abort(), null, timeoutMilliseconds, Timeout.Infinite))
            {
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        isAvailable = response.StatusCode == HttpStatusCode.OK;
                    }
                }
                catch (WebException ex)
                {
                    // Handle exceptions, e.g., request timed out or failed
                    // You can log the exception or take appropriate action
                    isAvailable = false;
                }
            }
            return isAvailable;
        }
        private bool IsVersionAvailable(byte majv,byte minv,byte patv)
        {
            string URL="https://github.com/PPUC/ZeDMD/releases/download/v"+majv.ToString()+"."+minv.ToString()+"."+patv.ToString()+"/ZeDMD-128x32.zip ";
            return IsUrlAvailable(URL,500);
        }
        int ValVersion(byte M, byte m, byte p)
        {
            return (int)(M << 16) + (int)(m << 8) + (int)p;
        }
        private void PopulateVersions()
        {
            byte majv=3, minv=2, patv=3;
            navVersions=0;
            bool over=false;
            while ((ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion))||over)
            {
                if (IsVersionAvailable(majv,minv,patv))
                {
                    avMVersion[navVersions]=majv;
                    avmVersion[navVersions]=minv;
                    avpVersion[navVersions]=patv;
                    navVersions++;
                    if (navVersions==MAX_VERSIONS_TO_LIST-2) break;
                    patv++;
                }
                else
                {
                    patv=0;
                    minv++;
                    if (ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion))
                    {
                        if (IsVersionAvailable(majv,minv,patv))
                        {
                            avMVersion[navVersions]=majv;
                            avmVersion[navVersions]=minv;
                            avpVersion[navVersions]=patv;
                            navVersions++;
                            if (navVersions==MAX_VERSIONS_TO_LIST-2) break;
                            patv++;
                        }
                        else
                        {
                            minv=0;
                            majv++;
                            if (ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion))
                            {
                                if (IsVersionAvailable(majv,minv,patv))
                                {
                                    avMVersion[navVersions]=majv;
                                    avmVersion[navVersions]=minv;
                                    avpVersion[navVersions]=patv;
                                    navVersions++;
                                    if (navVersions==MAX_VERSIONS_TO_LIST-2) break;
                                    patv++;
                                }
                                else over=true;
                            }
                        }
                    }
                }
            }
            avMVersion[navVersions]=avmajVersion;
            avmVersion[navVersions]=avminVersion;
            avpVersion[navVersions]=avpatVersion;
            navVersions++;
            versionList.Items.Clear();
            //versionList.Items.Add("v"+avmajVersion.ToString()+"."+avminVersion.ToString()+"."+avpatVersion.ToString());
            for (int ti=0;ti<navVersions; ti++) versionList.Items.Add("v"+avMVersion[navVersions-ti-1].ToString()+"."+avmVersion[navVersions-ti-1].ToString()+"."+avpVersion[navVersions-ti-1].ToString());
            versionList.SelectedIndex = 0;
        }
        public void StartWatcher()
        {
            // Initialize and start the watcher

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent");
            watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += DeviceChangeEvent;
            watcher.Start();
        }

        public void StopWatcher()
        {
            // Stop and dispose the watcher

            watcher.Stop();
            watcher.Dispose();
        }

        bool WatcherPaused=false;
        private void DeviceChangeEvent(object sender, EventArrivedEventArgs e)
        {
            Invoke((Action)(() =>
            {
                // UI update code here
                if (!WatcherPaused)
                {
                    WatcherPaused = true;
                    PopulateESP(true);
                    StopWatcher();
                    StartWatcher();
                    WatcherPaused = false;
                }
            }));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateESP(true);
            StartWatcher();
            this.Text="ZeDMD Installer/Updater v"+MAJ_VERSION.ToString()+"."+MIN_VERSION.ToString()+" by Zedrummer";
        }
        public Form1()
        {
            InitializeComponent();
            this.SD.Checked=true;
            this.HD.Checked=false;
            //help.Text="- The left list shows the ESP32 devices connected that does not have the ZeDMD firmware.\r\n- The right list shows the ZeDMDs connected.\r\n- Choose the device you wish to install the ZeDMD firmware to on the left list or the device you want to update the ZeDMD firmware in the right list, then click on \"Install\"/\"Update\" and browse for the bin file to be installed.\r\n- If you want to open the web page where the latest firmware version is available, click on \"Go to download page\" and download the latest firmware corresponding to your ZeDMD resolution.\r\n- If you have connected a new device, click on \"Update lists\".";
            GetAvailableVersion();
            LatestVersion.Text="Latest version available: "+avmajVersion.ToString()+"."+avminVersion.ToString()+"."+avpatVersion.ToString();
            this.Load+=Form1_Load;
            PopulateVersions();
            BVal.Items.Clear();
            for (int ti=1;ti<16;ti++) BVal.Items.Add(ti.ToString());
            BVal.SelectedIndex=0;
            BKeep.Checked=true;
            BVal.Enabled=false;
            OVal.Items.Clear();
            for (int ti=0;ti<6;ti++) OVal.Items.Add(ti.ToString());
            OVal.SelectedIndex=0;
            OKeep.Checked=true;
            OVal.Enabled=false;
        }

        private void ZeDMDList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ZeDMDList.SelectedIndex==-1) return;
			Match match = Regex.Match(ZeDMDList.Text, @"COM(\d+)");
			if (match.Success)
			{
				string comNumberString = match.Groups[0].Value;
				selCOM=int.Parse(comNumberString.Substring(3));
			}
            else selCOM = 0;
            ESP32List.SelectedIndex = -1;
            if (ZeDMDH[ZeDMDList.SelectedIndex]==64)
            {
                HD.Checked = true;
                is64=true;
            }
            else
            {
                SD.Checked = true;
                is64=false;
            }
            UpdateStartButton();
        }

        private void ESP32List_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ESP32List.SelectedIndex==-1) return;
 			Match match = Regex.Match(ESP32List.Text, @"COM(\d+)");
			if (match.Success)
			{
				string comNumberString = match.Groups[0].Value;
				selCOM=int.Parse(comNumberString.Substring(3));
			}
            else selCOM = 0;
            ZeDMDList.SelectedIndex = -1;
            UpdateStartButton();
        }
        private bool GetAvailableVersion()
        {
            string zipFileUrl="https://github.com/PPUC/ZeDMD/releases/latest/download/ZeDMD-128x32.zip";
            string versionFileName = "version.txt";

            using (WebClient client = new WebClient())
            {
                byte[] zipData = client.DownloadData(zipFileUrl);

                using (MemoryStream zipStream = new MemoryStream(zipData))
                using (ZipArchive archive = new ZipArchive(zipStream))
                {
                    ZipArchiveEntry versionEntry = archive.GetEntry(versionFileName);
                    if (versionEntry != null)
                    {
                        using (Stream versionStream = versionEntry.Open())
                        {
                            // Read firmware file into memory
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                versionStream.CopyTo(memoryStream);
                                byte[] bytes = memoryStream.ToArray();
                                string versionData = Encoding.ASCII.GetString(bytes);
                                string[] parts = versionData.Split('.');
                                avmajVersion = byte.Parse(parts[0]);
                                avminVersion = byte.Parse(parts[1]);
                                avpatVersion = byte.Parse(parts[2]);
                            }
                            versionStream.Close();
                        }
                    }
                    else return false;
                }
            }
            return true;
        }

        private void Start_Click(object sender, System.EventArgs e)
        {
            isFlashing=true;
            int zednum=ZeDMDList.SelectedIndex;
            int vernum=versionList.SelectedIndex;
            if (vernum<0) return;
            vernum=navVersions-vernum-1;
            string strVersion="v"+avMVersion[vernum].ToString()+"."+avmVersion[vernum].ToString()+"."+avpVersion[vernum].ToString();
            Start.Enabled = false;
            ESP32List.Items.Clear();
            ESP32List.Items.Add("Please wait...");
            ESP32List.Items.Add("... flashing");
            ZeDMDList.Items.Clear();
            ZeDMDList.Items.Add("Don't disconnect...");
            ZeDMDList.Items.Add("... any device before...");
            ZeDMDList.Items.Add("... these boxes are updated");
            WatcherPaused=true;
            string zipFileUrl;
            if (is64) zipFileUrl= "https://github.com/PPUC/ZeDMD/releases/download/"+strVersion+"/ZeDMD-256x64.zip";
            else zipFileUrl="https://github.com/PPUC/ZeDMD/releases/download/"+strVersion+"/ZeDMD-128x32.zip";
            string firmwareFileName = "ZeDMD.bin";

            using (WebClient client = new WebClient())
            {
                byte[] zipData = client.DownloadData(zipFileUrl);

                using (MemoryStream zipStream = new MemoryStream(zipData))
                using (ZipArchive archive = new ZipArchive(zipStream))
                {
                    ZipArchiveEntry firmwareEntry = archive.GetEntry(firmwareFileName);

                    if (firmwareEntry != null)
                    {
                        using (Stream firmwareStream = firmwareEntry.Open())
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                firmwareStream.CopyTo(memoryStream);
                                byte[] firmwareData = memoryStream.ToArray();
                                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                string filePath = Path.Combine(documentsPath, firmwareFileName);
                                System.IO.File.WriteAllBytes(filePath, firmwareData);
                                string esptoolPath = "esptool.exe";
                                string commandArgs = "--chip esp32 --port COM"+ selCOM.ToString() +"  --baud 921600 --before default_reset --after hard_reset write_flash -z --flash_mode dio --flash_freq 80m --flash_size detect 0 \""+filePath+"\"";

                                ProcessStartInfo processStartInfo = new ProcessStartInfo(esptoolPath, commandArgs);

                                using (Process process = new Process())
                                {
                                    process.StartInfo = processStartInfo;
                                    process.Start();
                                    process.WaitForExit(); // Wait for the process to complete

                                    int exitCode = process.ExitCode;

                                    if (exitCode != 0)
                                    {
                                        MessageBox.Show("The flashing failed. ESP32 are known to have an issue while flashing at connection time, retry pushing the ESP32 'BOOT' button during the connection", "Failed");
                                    }
                                    else if (zednum!=-1)
                                    {
                                        byte brig=brightness[zednum];
                                        byte rgbo=RGBorder[zednum];
                                        if (BSet.Checked==true) brig=(byte)(1+BVal.SelectedIndex);
                                        if (OSet.Checked==true) rgbo=(byte)(OVal.SelectedIndex);
                                        zdc.SetRGBOrderAndBrightness(selCOM,brig,rgbo);
                                    }
                                }
                                System.IO.File.Delete(filePath);
                            }
                            firmwareStream.Close();
                        }
                    }
                }
            }
            isFlashing=false;
            PopulateESP(false);
            //versionList.SelectedIndex = 0;
            WatcherPaused=false;
        }

        private void SD_CheckedChanged(object sender, EventArgs e)
        {
            if (SD.Checked==true)
            {
                is64=false;
                UpdateStartButton();
            }
        }

        private void HD_CheckedChanged(object sender, EventArgs e)
        {
            if (HD.Checked==true)
            {
                is64=true;
                UpdateStartButton();
            }
        }

        private void versionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStartButton();
        }

        private void BKeep_CheckedChanged(object sender, EventArgs e)
        {
            if (BKeep.Checked==true)
            {
                BVal.Enabled = false;
            }
        }

        private void BSet_CheckedChanged(object sender, EventArgs e)
        {
            if (BSet.Checked==true)
            {
                BVal.Enabled = true;
            }
        }

        private void OKeep_CheckedChanged(object sender, EventArgs e)
        {
            if (OKeep.Checked==true)
            {
                OVal.Enabled = false;
            }
        }

        private void OSet_CheckedChanged(object sender, EventArgs e)
        {
            if (OSet.Checked==true)
            {
                OVal.Enabled = true;
            }
        }
    }
}
