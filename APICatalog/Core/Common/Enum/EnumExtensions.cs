using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace APICatalog.Core.Common.Enum
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (member == null) return enumValue.ToString();

            var attribute = member.GetCustomAttribute<DisplayAttribute>();
            if (attribute == null) return enumValue.ToString();

            if (attribute.ResourceType != null)
            {
                var property = attribute.ResourceType.GetProperty(attribute.Name, BindingFlags.Static | BindingFlags.Public);
                return property?.GetValue(null)?.ToString() ?? enumValue.ToString();
            }

            return attribute.Name ?? enumValue.ToString();
        }

    }
}
