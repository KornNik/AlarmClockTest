using System.Text;

namespace Helpers.Extensions
{
    static class StringBuilderExtender
    {
        public static string CreateString(params string[] text)
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            foreach (var item in text)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }
        public static string CreateStringWithNewLine(params string[] text)
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            foreach (var item in text)
            {
                stringBuilder.Append(item);
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
        public static string FillValueInPlace(string defaultString, string valueToInsert, int insertPlace)
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            stringBuilder.Append(defaultString);
            stringBuilder.Insert(insertPlace, valueToInsert);

            return stringBuilder.ToString();
        }
    }
}
