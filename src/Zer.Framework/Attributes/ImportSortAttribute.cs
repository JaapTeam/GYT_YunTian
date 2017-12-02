using System;

namespace Zer.Framework.Attributes
{
    public class ImportSortAttribute : Attribute
    {
        public ImportSortAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }
    }
}