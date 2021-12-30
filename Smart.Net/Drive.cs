using System.Collections.Generic;

namespace Simplified.IO
{
    public class Drive
    {
        public Drive()
        {
            SmartAttributes = new SmartAttributeCollection();
            DriveLetters = new List<string>();
        }

        public int Index { get; set; }

        public string DeviceID { get; set; }
        public string PnpDeviceID { get; set; }

        public List<string> DriveLetters { get; set; }
        public bool IsOK { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }
        public SmartAttributeCollection SmartAttributes { get; set; }
    }
}