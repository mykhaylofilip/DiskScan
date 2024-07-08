using System;
using System.IO;
using System.Windows.Forms;

namespace DiskScan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayDiskInformation();
        }

        private void DisplayDiskInformation()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            int totalDrives = allDrives.Length;

            listBox1.Items.Add($"Total Drives Found: {totalDrives}");
            listBox1.Items.Add(""); 

            foreach (DriveInfo drive in allDrives)
            {
                string status = drive.IsReady ? "Online" : "Offline";

                listBox1.Items.Add($"Drive {drive.Name}");
                listBox1.Items.Add($"- Status: {status}");

                if (drive.IsReady)
                {
                    listBox1.Items.Add($"- Name: {drive.VolumeLabel}");
                    listBox1.Items.Add($"- Format: {drive.DriveFormat}");

                    
                    string partitionStyle = "";
                    switch (drive.DriveType)
                    {
                        case DriveType.Fixed:
                        case DriveType.Removable:
                        case DriveType.CDRom:
                        case DriveType.Network:
                            if (drive.DriveType == DriveType.Fixed && drive.DriveFormat.ToLower() == "fat32")
                            {
                                partitionStyle = "UEFI";
                            }
                            else
                            {
                                partitionStyle = "EFI";
                            }
                            break;
                        default:
                            partitionStyle = "Unknown";
                            break;
                    }

                    listBox1.Items.Add($"- Partition Style: {partitionStyle}");

                    long totalSize = drive.TotalSize;
                    long totalFreeSpace = drive.TotalFreeSpace;
                    long usedSpace = totalSize - totalFreeSpace;

                    listBox1.Items.Add($"- Storage: {usedSpace / (1024 * 1024 * 1024)} GB / {totalSize / (1024 * 1024 * 1024)} GB");
                }

                listBox1.Items.Add(""); 
            }
        }
    }
}
