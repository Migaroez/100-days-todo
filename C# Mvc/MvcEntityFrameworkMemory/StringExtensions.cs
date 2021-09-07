using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFrameworkMemory
{
    public static class StringExtensions
    {
        public static string EnableHtmlLineBreaks(this string input)
        {
            return input.Replace("\r\n", "<br/>");
        }
    }
}
