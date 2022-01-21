using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEntityFrameworkMemory.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string input)
        {
            return input == null || input.Trim().Length < 1;
        }
    }
}
