using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Extensions;

public static class EnumExtensions
    {
        public static string GetDescription(this Enum? enumeration)
        {
            if (enumeration == null)
                return string.Empty;

            var value = enumeration.ToString();
            var type = enumeration.GetType();
            var descAttribute =
                type.GetField(value)?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return descAttribute is {Length: > 0} ? descAttribute[0].Description : value;
        }

        public static T? GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null)!;
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null)!;
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public static IEnumerable<SelectListItem> ConvertEnumToSelectListItems<T>(T type) where T : Type
        {
            return Enum.GetValues(type)
                .Cast<object>()
                .Select(item => new
                {
                    item,
                    enumText = ((Enum)Enum.Parse(type, item.ToString() ?? string.Empty)).GetDescription()
                })
                .Select(t => new SelectListItem(t.enumText, (int)t.item));
        }
    }
