using System.Text.RegularExpressions;

namespace QuantTrade.Utils
{
    public static class RegexUtil
    {
        public static Regex ValidEmailAddress()
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static Regex MinLength(int length)
        {
            return new Regex(@"(^\s*(\S)\s*){" + length + @",}");
        }
    }
}