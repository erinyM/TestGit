using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models.models_ado
{
    public static class Extensions
    {
        public static string KiloFormat(this string s)
        {
            float num1 = float.Parse("0" + s);
            int num = (int)num1;
            if (num >= 100000000)
                return (num / 1000000.0).ToString("#,0M");

            if (num >= 1000000)
                return (num / 1000000.0).ToString("0.##") + "M";

            if (num >= 100000)
                return (num / 1000.0).ToString("#,0K");

            if (num >= 10000)
                return (num / 1000.0).ToString("0.##") + "K";

            return num.ToString("#,0");
        }
    }
}