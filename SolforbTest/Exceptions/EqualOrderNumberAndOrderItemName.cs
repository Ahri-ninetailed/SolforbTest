namespace SolforbTest.Exceptions
{
    public class EqualOrderNumberAndOrderItemName : Exception
    {
        public new string Message { get; set; } = "Название позиции и номер заказа должны быть разными.";
    }
}
