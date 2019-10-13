namespace Converter
{
    public abstract class Result
    {
        public object Value { get; protected set; }
    }

    public sealed class Success: Result
    {
        public Success(object value) => Value = value;
    }

    public sealed class Error : Result
    {
        public Error(string errorMsg, object value)
        {
            ErrorMessage = errorMsg;
            Value = value;
        }

        public Error(object value): this(string.Empty, value) { }

        public Error(string errorMsg) : this(errorMsg, default) { }

        public string ErrorMessage { get; }
    }
}