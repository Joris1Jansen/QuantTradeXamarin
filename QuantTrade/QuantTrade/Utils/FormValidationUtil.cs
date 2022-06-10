using System;
using System.Linq;

namespace QuantTrade.Utils
{
    public static class FormValidationUtil
    {
        public static string ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return String.Empty;
            }
            if (!RegexUtil.ValidEmailAddress().IsMatch(email))
            {
                return "Invalid Email";
            }

            return String.Empty;
        }
        public static string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return String.Empty;
            }
            if (password.Length < 6)
            {
                return "Passwords too short, 6 characters required";
            }
            
            return String.Empty;
        }
        
        public static string ValidateVerificationPassword(string password, string verifyPassword)
        {
            if (string.IsNullOrEmpty(verifyPassword))
            {
                return String.Empty;
            }
            if (password != verifyPassword)
            {
                return "Passwords do not match";
            }
            return String.Empty;
        }

        public static bool EntriesHaveTekst(params string[] list)
        {
            return list.All(x => !string.IsNullOrEmpty(x));
        }

        public static bool EntriesHaveNoError(params string[] list)
        {
            return list.All(x => string.IsNullOrEmpty(x));
        }

        public static bool WhichBroker(string checkBroker, string brokerName)
        {
            if (checkBroker == brokerName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}