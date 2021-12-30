namespace Simplified.IO
{
    public sealed class SmartAttribute
    {
        public SmartAttribute(int register, string attributeName)
        {
            Register = register;
            Name = attributeName;
        }

        public int Register { get; set; }
        public string Name { get; set; }

        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool IsOK { get; set; }

        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }
    }
}