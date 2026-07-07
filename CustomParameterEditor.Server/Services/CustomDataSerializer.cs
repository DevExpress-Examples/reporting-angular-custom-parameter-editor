using DevExpress.Utils.Serializing;
using System.ComponentModel;

namespace CustomParameterEditor {
    [TypeConverter(typeof(CustomParameterTypeConverter))]
    public class CustomDataSerializer : IObjectDataSerializer {
        public const string Name = "myCustomDataSerializer";

        public bool CanDeserialize(string value, string typeName) {
            bool canDeserialize = typeName == typeof(CustomParameterType).FullName;
            return canDeserialize;
        }

        public bool CanSerialize(object data) {
            return data is CustomParameterType;
        }

        public object Deserialize(string value, string typeName) {
            if(typeName == typeof(CustomParameterType).FullName) {
                return new CustomParameterType { Value = value };
            }
            return null;
        }

        public string Serialize(object data) {
            var parameter = data as CustomParameterType;
            return parameter != null ? parameter.Value : null;
        }
    }
}