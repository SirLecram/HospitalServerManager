using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.InterfacesAndEnums
{
    public static class Extensions
    {
        public static string GetEnumDescription(this Enum e)
        {
            // DescriptionAttribute.GetCustomAttributes(e.GetType().GetMember(()))
            var enumMember = e.GetType().GetMember(e.ToString()).FirstOrDefault();
            DescriptionAttribute descriptionAttribute =
                enumMember == null
                ? default(DescriptionAttribute)
                : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return descriptionAttribute.Description;
        }
        public static T GetEnumFromDescription<T>(this string description)
        {
            // Found in: https://stackoverflow.com/questions/4367723/get-enum-from-description-attribute

            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");

        }
    }
}
