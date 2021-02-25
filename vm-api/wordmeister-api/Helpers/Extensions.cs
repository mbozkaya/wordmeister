using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static wordmeister_api.Helpers.Enums;

namespace wordmeister_api.Helpers
{
    public static class Extensions
    {
        public static string TurkishCharReplace(this string text)
        {
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            return text;
        }

        public static bool IsNotNullAndAny<T>(this IList<T> source)
        {
            if (source != null && source.FirstOrDefault() != null && source.Any())
                return true;

            return false;
        }

        public static bool IsNullOrDefault<T>(this T value)
        {
            return object.Equals(value, default(T));
        }

        public static int GetUserId(this ClaimsPrincipal context)
        {
            int userId = 0;
            int.TryParse(context.Claims.First(x => x.Type == "id").Value, out userId);

            return userId;
        }

        public static string GetDirectoryName(this UploadFileType type)
        {
            string directory = "error";
            var fileTypeDictionary = new Dictionary<int, string> { { 1, "PP" } };

            fileTypeDictionary.TryGetValue((int)type, out directory);

            return directory;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return (IQueryable<TSource>)source.Where(predicate);
            else
                return source;
        }
    }
}
