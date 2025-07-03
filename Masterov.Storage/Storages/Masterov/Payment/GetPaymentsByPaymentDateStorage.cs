using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

public class GetPaymentsByPaymentDateStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByPaymentDateStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByPaymentDate(DateTime paymentDate, CancellationToken cancellationToken)
    {
        // Фильтрация по дате (без учёта времени)
        var startOfDay = paymentDate.Date;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.OrderPayments
            .Where(payDate => payDate.PaymentDate >= startOfDay && payDate.PaymentDate < endOfDay)
            .Include(o => o.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(orders);
    }
}