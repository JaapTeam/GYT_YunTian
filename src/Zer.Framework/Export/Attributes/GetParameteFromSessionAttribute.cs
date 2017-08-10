using System;

namespace Zer.Framework.Export.Attributes
{
    public class GetParameteFromSessionAttribute : Attribute
    {
        public GetParameteFromSessionAttribute(string sessionParameterName)
        {
            SessionParameterName = sessionParameterName;
        }

        public string SessionParameterName { get; private set; }
    }
}