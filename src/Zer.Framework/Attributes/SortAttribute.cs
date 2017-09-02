using System;

namespace Zer.Framework.Attributes
{
    public class ExportSortAttribute:Attribute
    {
        public int Index { get; private set; }

        public ExportSortAttribute(int index)
        {
            Index = index;
        }
    }
}