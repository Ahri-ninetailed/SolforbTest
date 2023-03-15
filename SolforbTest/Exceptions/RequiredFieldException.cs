namespace SolforbTest.Exceptions
{
    public class RequiredFieldException : Exception
    {
        public new string Message { get; set; } = "Это поле обязательно к заполнению.";
    }
}
