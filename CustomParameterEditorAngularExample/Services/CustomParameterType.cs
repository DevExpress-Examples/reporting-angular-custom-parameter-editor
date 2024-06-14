using System;
using System.ComponentModel;
using System.Globalization;

[TypeConverter(typeof(CustomParameterTypeConverter))]
public class CustomParameterType {
    public string Value { get; set; }
    public override string ToString() {
        return Value;
    }
}

public class CustomParameterTypeConverter : TypeConverter {
    public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture, object value, Type destinationType) {
        if (destinationType == typeof(string)) {
            return ((CustomParameterType)value).Value;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
    public override bool CanConvertTo(ITypeDescriptorContext context,
        Type destinationType) {
        return destinationType == typeof(string) ||
            base.CanConvertTo(context, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }
    public override object ConvertFrom(ITypeDescriptorContext context,
        CultureInfo culture, object value) {
        var valueString = value as string;
        if (valueString != null) {
            return new CustomParameterType { Value = valueString };
        }
        return base.ConvertFrom(context, culture, value);
    }
}