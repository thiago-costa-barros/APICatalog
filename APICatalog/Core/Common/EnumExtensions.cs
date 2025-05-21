using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace APICatalog.Core.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (member == null) return enumValue.ToString();

            var attribute = member.GetCustomAttribute<DisplayAttribute>();
            return attribute?.GetName() ?? enumValue.ToString();
        }
    }
}
