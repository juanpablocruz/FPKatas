using System;

namespace FPKatas.Payment
{
    public interface IPaymentType
    {
        T Match<T>(
            Func<PaymentService, T> individual,
            Func<PaymentService, T> parent,
            Func<ChildPaymentService, T> child);

        T Accept<T>(IPaymentTypeVisitor<T> visitor);
    }
}
