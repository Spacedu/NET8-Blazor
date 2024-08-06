using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Gestao.Client.Libraries.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            //Atributo
            var enumType = enumValue.GetType();
            var memberInfo = enumType.GetMember(enumValue.ToString());

            if(memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    if(displayAttribute.Name != null)
                        return displayAttribute.Name;
                }
            }

            //None, Weekly....
            return enumValue.ToString();
        }
    }
}
