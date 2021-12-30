using System.Collections.Generic;

namespace Simplified.IO
{
    public class SmartAttributeCollection : List<SmartAttribute>
    {
        public SmartAttribute GetAttribute(int registerID)
        {
            foreach (var item in this)
            {
                if (item.Register == registerID)
                    return item;
            }

            return null;
        }
    }
}
