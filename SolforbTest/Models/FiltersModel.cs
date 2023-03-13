using SolforbTest.Services;

namespace SolforbTest.Models
{
    /// <summary>
    /// Модель фильтров на главной странице
    /// </summary>
    public class FiltersModel
    {
        public IEnumerable<string> OrdersNumbers { get; set; }
        public IEnumerable<int> ProvidersId { get; set; }
        public IEnumerable<string> ItemsNames { get; set; }
        public IEnumerable<decimal> ItemsQuantities { get; set; }
        public IEnumerable<string> ItemsUnits { get; set; }
        public IEnumerable<string> ProvidersNames { get; set; }
        public DateTime FirstDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime SecondDate { get; set; } = DateTime.Now;

    }
}
