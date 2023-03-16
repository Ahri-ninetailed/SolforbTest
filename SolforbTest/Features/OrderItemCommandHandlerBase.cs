using SolforbTest.Exceptions;

namespace SolforbTest.Features
{
    public class OrderItemCommandHandlerBase
    {
        protected virtual IEnumerable<Exception> fillValidationExceptionsList(IOrderItemRequest request)
        {
            List<Exception> listOfValidationExceptions = new List<Exception>();
            if (request.OrderItem.Unit is null)
                listOfValidationExceptions.Add(new RequiredOrderItemUnitException());
            if (request.OrderItem.Name is null)
                listOfValidationExceptions.Add(new RequiredOrderItemNameException());
            else if (request.OrderItem.Name == request.OrderItem.OrderNumber)
                listOfValidationExceptions.Add(new EqualOrderNumberAndOrderItemNameException());
            if (request.OrderItem.Quantity <= 0)
                listOfValidationExceptions.Add(new RequiredOrderItemQuantityException());
            return listOfValidationExceptions;
        }
    }
}
