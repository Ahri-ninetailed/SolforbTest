using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace SolforbTest.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SolforbDbContext solforbDbContext;
        public BaseController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        protected virtual void fillOrderViewOfValidationErrors(Order order)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            if (order.Number is null)
                ViewBag.NumberLabel = errorMsg;
        }
        protected virtual bool isOrderValidate(Order order)
        {
            return (order.Number is null ? false : true);

        }
        protected virtual void fillOrderItemViewOfValidationErrors(OrderItem item)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            if (item.Unit is null)
                ViewData[$"UnitError"] = errorMsg;
            if (item.Name is null)
                ViewData[$"NameError"] = errorMsg;
            else if (item.Name == item.OrderNumber)
                ViewData[$"NameError"] = "Название позиции и номер заказа должны быть разными";
            if (item.Quantity <= 0)
                ViewData[$"QuantityError"] = errorMsg;
        }
        protected virtual bool isOrderItemValidate(OrderItem item)
        {
            if (item.Name is null || item.Unit is null || item.Quantity <= 0)
                return false;
            return true;
        }
    }
}
