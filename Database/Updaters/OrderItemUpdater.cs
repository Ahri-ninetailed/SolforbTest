using Database.Models;

namespace Database.Updaters
{
    public static class OrderItemUpdater
    {
        public static OrderItem SetName(this OrderItem item, string newName)
        {
            item.Name = newName;
            return item;
        }
        public static OrderItem SetUnit(this OrderItem item, string newUnit)
        {
            item.Unit = newUnit;
            return item;
        }
        public static OrderItem SetQuantity(this OrderItem item, decimal newQuantity) 
        {
            item.Quantity = newQuantity;
            return item;
        }
        public static void UpdateOrderItem(this OrderItem item, OrderItem newOrderItem)
        {
            item
                .SetName(newOrderItem.Name)
                .SetQuantity(newOrderItem.Quantity)
                .SetUnit(newOrderItem.Unit);
        }
    }
}
