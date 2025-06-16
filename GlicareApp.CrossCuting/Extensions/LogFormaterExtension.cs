namespace GlicareApp.CrossCuting.Extensions
{
    public static class LogFormaterExtension
    {
        public static string Format(this Exception exception)
        {
            return
                 $"Data/hora  : {DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}\r\n\r\n" +
                 $"StackTrace : \r\n\r\n   {(string.IsNullOrEmpty(exception.StackTrace) ? "" : exception.StackTrace.Trim())}\r\n\r\n" +
                 $"Exception  : \r\n\r\n   {exception.Message}";
        }
    }
}
