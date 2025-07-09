using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.DeletePayment.Command;
using Masterov.Domain.Masterov.Payment.GetPaymentById;

namespace Masterov.Domain.Masterov.Payment.DeletePayment;

public class DeletePaymentUseCase(IValidator<DeletePaymentCommand> validator, 
    IDeletePaymentStorage storage, 
    IGetPaymentByIdStorage  getPaymentByIdStorage) : IDeletePaymentUseCase
{
    public async Task<bool> Execute(DeletePaymentCommand deletePaymentCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deletePaymentCommand, cancellationToken);
        
        var paymentExists = await getPaymentByIdStorage.GetPaymentById(deletePaymentCommand.PaymentId, cancellationToken);
        
        if (paymentExists is null)
            throw new NotFoundByIdException(deletePaymentCommand.PaymentId, "Платеж");
        
        //TODO При удалении сравнение цены готового мебельного изделия с внесенными платежами и далее менять статус заказа при удалении платежа
        //TODO если платежей не хватает до готовой цены готового мебельного изделия то Partial
        //TODO если удаляются все платежи то тогда статус заказа Draft соответсвенно,
        // TODO должна появится возможность в ProductionOrder поставить статус заказа в Canceled
        
        return await storage.DeletePayment(deletePaymentCommand.PaymentId, cancellationToken);
    }
}