using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public static class Utils
    {
        public static T ParseNullable<T>(string value)
        {
            if (value == null || value.Trim() == string.Empty)
            {
                return default(T);
            }
            else
            {
                try { return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value.ToString()); }
                catch
                {
                    return default(T);
                }
            }
        }
    }
}