namespace SolforbTest.Models
{
    public class CreatePageModel
    {
        public string Provider { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItm> OrderItms { get; set; } = new List<OrderItm> { new OrderItm(), new OrderItm(), };
    }
    public class OrderItm
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
