using System;

namespace FPKatas.Payment
{
    public class Parent : IPaymentType
    {
        private readonly PaymentService paymentService;

        public Parent(PaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public T Match<T>(
            Func<PaymentService, T> individual,
            Func<PaymentService, T> parent,
            Func<ChildPaymentService, T> child)
        {
            return parent(paymentService);
        }

        public T Accept<T>(IPaymentTypeVisitor<T> visitor)
            => visitor.VisitParent(paymentService);
    }
}
