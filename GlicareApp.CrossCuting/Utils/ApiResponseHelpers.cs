namespace GlicareApp.CrossCuting.Utils
{
    public static class ApiResponseHelpers
    {
        public static Response InternalServerError(string message = "Request Failed") => new() { Message = message, StatusCode = 500, Title = "Internal Server Error" };
        public static Response BadRequest(string message) => new() { Message = message, StatusCode = 400, Title = "BadRequest" };
        public static Response Unauthorized() => new() { Message = "Fail to access", StatusCode = 400, Title = "Unauthorized" };
        public static Response Ok() => new() { Message = "Succesfully", StatusCode = 200, Title = "Ok" };
        public static Response NoContent() => new() { Message = "Succesfully", StatusCode = 204, Title = "No Content" };

        public static Response<T> Ok<T>(T t) => new() { Message = "Succesfully", Data = t, StatusCode = 200, Title = "Ok" };
        public static Response<T> BadRequest<T>(string message) => new() { Message = message, StatusCode = 400, Title = "BadRequest" };
        public static Response<T> InternalServerError<T>(string message = "Request Failed") => new() { Message = message, StatusCode = 500, Title = "Internal Server Error" };
        public static Response<T> NoContent<T>() => new() { Message = "Succesfully", StatusCode = 204, Title = "No Content" };
    }

    public class Response
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
    public class Response<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
