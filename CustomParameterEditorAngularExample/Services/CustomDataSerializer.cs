using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CustomParameterEditorAngularExample;
using DevExpress.XtraReports.Native;

namespace CustomParameterEditorAngularExample
{
    [TypeConverter(typeof(CustomParameterTypeConverter))]
    public class CustomDataSerializer : IDataSerializer
    {
        public const string Name = "myCustomDataSerializer";

        public bool CanDeserialize(string value, string typeName, object extensionProvider)
        {
            return typeName == typeof(CustomParameterType).FullName;
        }

        public bool CanSerialize(object data, object extensionProvider)
        {
            return data is CustomParameterType;
        }

        public object Deserialize(string value, string typeName, object extensionProvider)
        {
            if (typeName == typeof(CustomParameterType).FullName)
            {
                return new CustomParameterType { Value = value };
            }
            return null;
        }

        public string Serialize(object data, object extensionProvider)
        {
            var parameter = data as CustomParameterType;
            return parameter != null ? parameter.Value : null;
        }
    }
}
