namespace Gymnastic.Application.UseCases.Commons.Bases
{
    public class BaseResponseGeneric<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<Object>? Errors { get; set; }
    }
}
