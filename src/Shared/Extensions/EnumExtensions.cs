using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trailblazor.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static bool TryGetAttribute<T>(this Enum value, out T attribute) where T : Attribute
        {
            attribute = value.GetAttribute<T>();
            return attribute is not null;
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (!attributes.Any())
                return null;

            return (T)attributes[0];
        }

        public static string GetName(this Enum value) => value.GetAttribute<Attribute>() switch
        {
            DisplayNameAttribute attr => attr.DisplayName,
            DisplayAttribute attr => attr.Name,
            _ => value.ToString()
        };

        public static string GetShortName(this Enum value) => value.GetAttribute<DisplayAttribute>()?.ShortName ?? value.ToString();
    }
}
