using System;
using System.Diagnostics;
using System.Management;

namespace Simplified.IO
{
    public sealed class WmiController
    {
        public static DriveCollection GetSmartInformation()
        {
            var drives = new DriveCollection();
            try
            {
                // TODO: 2017-12-19 - Refactor regions into separate methods.
                foreach (var device in new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive").Get())
                {
                    #region Drive Info

                    var drive = new Drive
                    {
                        DeviceID = device.GetPropertyValue("DeviceID").ToString(),
                        PnpDeviceID = device.GetPropertyValue("PNPDeviceID").ToString(),
                        Model = device["Model"]?.ToString().Trim(),
                        Type = device["InterfaceType"]?.ToString().Trim(),
                        Serial = device["SerialNumber"]?.ToString().Trim()

                    };

                    #endregion

                    #region Get drive letters

                    foreach (var partition in new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + device.Properties["DeviceID"].Value
                        + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                    {

                        foreach (var disk in new ManagementObjectSearcher(
                            "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                            + partition["DeviceID"]
                            + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                        {
                            drive.DriveLetters.Add(disk["Name"].ToString());
                        }

                    }

                    #endregion

                    #region Overall Smart Status       

                    var scope = new ManagementScope("\\\\.\\ROOT\\WMI");
                    var query = new ObjectQuery(@"SELECT * FROM MSStorageDriver_FailurePredictStatus Where InstanceName like ""%"
                                                + drive.PnpDeviceID.Replace("\\", "\\\\") + @"%""");
                    var searcher = new ManagementObjectSearcher(scope, query);
                    var queryCollection = searcher.Get();
                    foreach (ManagementObject m in queryCollection)
                    {
                        drive.IsOK = (bool)m.Properties["PredictFailure"].Value == false;
                    }

                    #endregion

                    #region Smart Registers

                    drive.SmartAttributes.AddRange(Helper.GetSmartRegisters(Resource.SmartAttributes));

                    searcher.Query = new ObjectQuery(@"Select * from MSStorageDriver_FailurePredictData Where InstanceName like ""%"
                                                     + drive.PnpDeviceID.Replace("\\", "\\\\") + @"%""");

                    foreach (ManagementObject data in searcher.Get())
                    {
                        var bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                        for (var i = 0; i < 42; ++i)
                        {
                            try
                            {
                                int id = bytes[i * 12 + 2];

                                int flags = bytes[i * 12 + 4]; // least significant status byte, +3 most significant byte, but not used so ignored.
                                                               //bool advisory = (flags & 0x1) == 0x0;
                                var failureImminent = (flags & 0x1) == 0x1;
                                //bool onlineDataCollection = (flags & 0x2) == 0x2;

                                int value = bytes[i * 12 + 5];
                                int worst = bytes[i * 12 + 6];
                                var vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
                                if (id == 0) continue;

                                var attr = drive.SmartAttributes.GetAttribute(id);
                                attr.Current = value;
                                attr.Worst = worst;
                                attr.Data = vendordata;
                                attr.IsOK = failureImminent == false;
                            }
                            catch (Exception ex)
                            {
                                // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }

                    searcher.Query = new ObjectQuery(@"Select * from MSStorageDriver_FailurePredictThresholds Where InstanceName like ""%"
                                                     + drive.PnpDeviceID.Replace("\\", "\\\\") + @"%""");
                    foreach (ManagementObject data in searcher.Get())
                    {
                        var bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                        for (var i = 0; i < 42; ++i)
                        {
                            try
                            {
                                int id = bytes[i * 12 + 2];
                                int thresh = bytes[i * 12 + 3];
                                if (id == 0) continue;

                                var attr = drive.SmartAttributes.GetAttribute(id);
                                attr.Threshold = thresh;

                                // Debug
                                // Console.WriteLine("{0}\t {1}\t {2}\t {3}\t " + attr.Data + " " + ((attr.IsOK) ? "OK" : ""), attr.Name, attr.Current, attr.Worst, attr.Threshold);
                            }
                            catch (Exception ex)
                            {
                                // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }

                    #endregion

                    drives.Add(drive);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Smart data for one or more drives. " + ex.Message);
            }

            return drives;
        }
    }
}
