namespace SolforbTest.Exceptions
{
    public class ValidationExceptions : Exception
    {
        public ValidationExceptions(string? message) : base(message) { }
        public ValidationExceptions(IEnumerable<Exception> exceptions)
        {
            ArrayOfExceptions = exceptions;
        }
        public IEnumerable<Exception> ArrayOfExceptions { get; set; }
    }
}
