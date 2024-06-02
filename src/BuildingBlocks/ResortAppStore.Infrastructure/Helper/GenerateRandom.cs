using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class GenerateRandom
    {

        public const string LowerCaseAlphabet = "abcdefghijklmnopqrstuvwyxz";
        public const string UpperCaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetSerialCode(long Id)
        {
            string defaultSerial = "1";
            string serial = "";
            Int64 nextSerial = Id;
            if (Id != 0)
                serial = (nextSerial + 1).ToString();
            else
                serial = defaultSerial;
            if (serial.Length > 0 && serial.Length <= defaultSerial.Length)
            {
                defaultSerial = defaultSerial.Remove(defaultSerial.Length - serial.Length);
                serial = defaultSerial + serial;
            }
            return serial;
        }
        public static string GenerateCustomRandomString(string prefix = "", int size = 4, bool lowerCase = false, bool isWithTime = true, bool isReadable = false)
        {
            DateTime ksaDt = DateTime.UtcNow.AddHours(3);
            var day = ksaDt.Day.ToString("D2");
            var month = ksaDt.Month.ToString("D2");
            var year = ksaDt.Year.ToString().Substring(2, 2);
            var time = isWithTime ? ksaDt.Hour.ToString("D2") + ksaDt.Minute.ToString("D2") + ksaDt.Second.ToString("D2") : string.Empty;
            var code = prefix + year + month + day + time;
            if (isReadable)
                return code + "-" + GenerateString(size, lowerCase) + RandomNumber();
            return code + GenerateString(size, lowerCase);
        }
        public static void SetCode(this ClaimsPrincipal principal, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null) return;

            var existingClaim = identity.FindFirst("Code");
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            identity.AddClaim(new Claim("Code", value, null, "ResortAPI", "ResortAPI"));

        }
        public static void AddCode(this IPrincipal currentPrincipal, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst("Code");
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim("Code", value, "http://www.w3.org/2001/XMLSchema#string", "ResortAPI", "ResortAPI"));
            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
        }

        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);

            // ?. prevents a exception if claim is null.
            return claim?.Value;
        }
        public static string GenerateString(int size, bool lowerCase)
        {
            string alphabet = lowerCase ? LowerCaseAlphabet : UpperCaseAlphabet;
            var random = new Random();
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(chars);
        }


        public static string RandomNumber(int size = 3)
        {
            Random rand = new Random();
            int value = rand.Next(1000);
            return value.ToString($"D{size}");
        }
        public static string GenerateTreeId(long id)
        {
            return id.ToString("D9");
        }
        public static string GenerateCode(long? parentId, int levelId)
        {
            if (parentId == 0 || parentId == null)
            {
                return "1";
            }
            else
            {
                var parentCode = GenerateCode(parentId, levelId - 1);
                var code = $"{parentCode}{levelId.ToString("D2")}";
                return code;
            }
        }

    
        public static string GenerateIdInt64(string parent, Int64 max, int digit = 2)
        {
            string maxStr = "";
            Int64 newLastDigit = 0;
            string lastDigitsStr = "";
            string newId = "";
            string prefix = "0";
            if (parent == "")
                prefix = "";
            //for (int i = 1; i < digit-1; i++)
            //{
            //    prefix += "0";
            //}
            for (int i = 1; i < digit; i++)
            {
                prefix += "0";
            }
            maxStr = max.ToString();

            if (maxStr.Length % digit != 0)
                maxStr = "0" + maxStr;
            if (maxStr.Length - digit >= 0)
            {
                lastDigitsStr = maxStr.Substring(maxStr.Length - digit, digit);

            }
            else
            {
                lastDigitsStr = "0";
            }
               
            newLastDigit = Convert.ToInt64(lastDigitsStr) + 1;
            newId = parent + prefix + newLastDigit;
            return newId;
        }
        public static string GetSerial(Int64 id)
        {
            string sefaultSerial = "00000001";
            string serial = "";
            Int64 nextSerial = id;
            if (id != 0)
                serial = (nextSerial + 1).ToString();
            else
                serial = sefaultSerial;
            if (serial.Length > 0 && serial.Length <= sefaultSerial.Length)
            {
                sefaultSerial = sefaultSerial.Remove(sefaultSerial.Length - serial.Length);
                serial = sefaultSerial + serial;
            }
            return serial;
        }
    }
}
