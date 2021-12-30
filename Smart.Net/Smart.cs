namespace Simplified.IO
{
    public static class Smart
    {
        public static DriveCollection GetDrives()
        {
            return WmiController.GetSmartInformation();
        }
    }
}
