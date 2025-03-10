namespace Gymnastic.Application.UseCases.Commons.Bases
{
    public class BaseResponse<T> : BaseResponseGeneric<T>
    {
        public static BaseResponse<T> Success(T data, string message = "", int statusCode = 200)
        {
            return new BaseResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static BaseResponse<T> Fail(string message, int statusCode = 400, IEnumerable<Object>? errors = null)
        {
            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
