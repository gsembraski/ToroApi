using System.Text;

namespace Toro.API.Domain.Resources.Extensions
{
    public static class SecurityExtension
    {
        public static string EncryptMD5(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var password = text += "|qIjF7XqLmk6NPpLZeo_hPw";
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
