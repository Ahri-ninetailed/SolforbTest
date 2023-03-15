namespace SolforbTest.Exceptions
{
    public class EqualOrderNumberAndOrderItemNameException : Exception
    {
        public new string Message { get; set; } = "Название позиции и номер заказа должны быть разными.";
    }
}
