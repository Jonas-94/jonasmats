using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Logic.DAL
{
    static class Regex
    {
        static string ErrorMessage = "";
        /*
        public static bool VerifyRegNr(string regNr)
        {
            regNr = System.Text.RegularExpressions.Regex.Match
        }*/
        public static bool VerifyEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normaliserr domänen
                email = System.Text.RegularExpressions.Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Undersöker domändelen av e-postmeddelandet och normaliserar det
                string DomainMapper(Match match)
                {
                    // Använd IdnMapping-klassen för att konvertera Unicode-domännamn.
                    var idn = new IdnMapping();

                    // Dra ut och bearbeta domännamn(kastar ArgumentException på ogiltigt)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return System.Text.RegularExpressions.Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool VerifyPassword(string password)
        {

            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new System.Text.RegularExpressions.Regex(@"[0-9]+");
            var hasUpperChar = new System.Text.RegularExpressions.Regex(@"[A-Z]+");
            //var hasMiniMaxChars = new Regex(@".{4-20}");
            var hasLowerChar = new System.Text.RegularExpressions.Regex(@"[a-z]+");
            var hasSymbols = new System.Text.RegularExpressions.Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars(input))
            {
                ErrorMessage = "Password should not be less than 4 or greater than 20 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool checkForEmail(string email)
        {
            bool IsValid = false;
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(email))
            {
                IsValid = true;
            }
            return IsValid;
        }

        public static bool hasMiniMaxChars(string input)
        {
            if (input.Length >= 4 && input.Length <= 20)
            {
                return true;
            }
            return false;
        }
    }
}

