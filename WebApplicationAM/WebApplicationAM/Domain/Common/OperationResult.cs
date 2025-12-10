namespace WebApplicationAM.Domain.Common
{
    public class OperationResult<T>
        where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Result { get; set; }
        public Exception? Exception { get; set; }
        public static OperationResult<T> Success(T result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            return new OperationResult<T> { Result = result, IsSuccess = true, Exception = null };
        }

        public static OperationResult<T> Error(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            return new OperationResult<T> { Result = null, Exception = exception, IsSuccess = false };
        }

        public static OperationResult<T> Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException(nameof(errorMessage));

            return new OperationResult<T> { Result = null, Exception = new Exception(errorMessage), IsSuccess = false };
        }
    }
}
