using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Markup;

namespace MarkupExtension
{
    public class EnumDescriptionExtension : System.Windows.Markup.MarkupExtension
    {
        public object Value { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Value is Enum enumVal)
            {
                FieldInfo field = enumVal.GetType().GetField(enumVal.ToString());
                var attr = field?.GetCustomAttribute<DescriptionAttribute>();
                return attr?.Description ?? enumVal.ToString();
            }

            return Value?.ToString() ?? string.Empty;
        }
    }

    public enum MyEnum
    {
        [Description("첫 번째 항목")]
        First,
        [Description("두 번째 항목")]
        Second
    }
}
