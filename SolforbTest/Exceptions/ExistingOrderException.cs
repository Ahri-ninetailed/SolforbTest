namespace SolforbTest.Exceptions
{
    public class ExistingOrderException : Exception
    {
        public new string Message { get; set; } = "Такой заказ от поставщика уже существует.";
    }
}
