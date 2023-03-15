using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Exceptions;

namespace SolforbTest.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SolforbDbContext solforbDbContext;
        public BaseController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        protected virtual void fillOrderViewOfValidationErrors(Exception exception)
        {
            switch (exception)
            {
                case RequiredFieldException requiredFieldException:

                    ViewBag.NumberLabel = requiredFieldException.Message;
                    break;
                case ExistingOrderException existingOrderException:
                    ViewBag.NumberLabel = existingOrderException.Message;
                    break;
            }
        }
        protected virtual bool isOrderValidate(Order order)
        {
            return (order.Number is null ? false : true);

        }
        protected virtual void fillOrderItemViewOfValidationErrors(ValidationExceptions exceptions)
        {
            foreach (var exception in exceptions.ArrayOfExceptions)
            {
                switch (exception)
                {
                    case RequiredOrderItemUnitException requiredOrderItemUnitException:
                        ViewBag.UnitError = requiredOrderItemUnitException.Message;
                        break;
                    case RequiredOrderItemNameException requiredOrderItemNameException:
                        ViewBag.NameError = requiredOrderItemNameException.Message;
                        break;
                    case EqualOrderNumberAndOrderItemNameException equalOrderNumberAndOrderItemName:
                        ViewBag.NameError = equalOrderNumberAndOrderItemName.Message;
                        break;
                    case RequiredOrderItemQuantityException requiredOrderItemQuantityException:
                        ViewBag.QuantityError = requiredOrderItemQuantityException.Message;
                        break;
                }
            }
        }
        protected virtual bool isOrderItemValidate(OrderItem item)
        {
            if (item.Name is null || item.Unit is null || item.Quantity <= 0)
                return false;
            return true;
        }
    }
}
