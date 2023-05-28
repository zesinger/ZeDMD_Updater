# ZeDMD_Updater
A simple to use installer/updater for your ZeDMD:

![ZeDMD_Updater](https://i.servimg.com/u/f88/17/72/22/03/scree294.png)

Everything is automatic:

- automatic detection of new device connected while program is running
- automatic detection of the latest version available in the Github ZeDMD Installer/Updater
- automatic detection of the ESP32 with no ZeDMD firmware connected in the left listbox (caution, if the ESP32 is not listed, chances are that the device drivers are not installed, I suggest you read this post https://www.esp32.com/viewtopic.php?t=5841 and mainly the answer given by herbert@vitzthum.at)
- automatic detection of the ZeDMD device connected in the center listbox with their format "ZeDMD" or "ZeDMD HD" and installed firmware version, green ones have the latest firmware, red ones may be updated to the latest firmware
- automatic listing of all the available firmwares in the right listbox (after the 3.2.3 as some needed functions were not available before)

To install/update a ZeDMD firmware:

- chose the device in one of the 2 left listboxes
- chose the firmware you wish to install (the latest one at the top, the oldest one at the bottom)
- chose if you wish to install the ZeDMD or ZeDMD HD version (the check box will be at the current state for ZeDMD/ZeDMD HD)
- press on the "Install"/"Update" button

That's all, just run the program regularly to keep your devices updated.
