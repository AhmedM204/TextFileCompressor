namespace FileCompressor.Extensions
{
    internal static class IntExtensions
    {
        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, 2);
        }
    }
}
