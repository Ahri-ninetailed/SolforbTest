namespace SolforbTest.Extensions
{
    public class ExistingOrderException : Exception
    {
        public new string Message { get; set; } = "Такой заказ от поставщика уже существует.";
    }
}
