using System;

namespace FPKatas.Payment
{
    public class Individual : IPaymentType
    {
        private readonly PaymentService paymentService;

        public Individual(PaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public T Match<T>(
            Func<PaymentService, T> individual,
            Func<PaymentService, T> parent,
            Func<ChildPaymentService, T> child)
        {
            return individual(paymentService);
        }

        public T Accept<T>(IPaymentTypeVisitor<T> visitor)
            => visitor.VisitIndividual(paymentService);
    }
}
