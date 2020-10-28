using System;

namespace FPKatas.Payment
{
    public class Child : IPaymentType
    {
        private readonly ChildPaymentService childPaymentService;

        public Child(ChildPaymentService childPaymentService)
        {
            this.childPaymentService = childPaymentService;
        }

        public T Match<T>(
            Func<PaymentService, T> individual,
            Func<PaymentService, T> parent,
            Func<ChildPaymentService, T> child)
        {
            return child(childPaymentService);
        }

        public T Accept<T>(IPaymentTypeVisitor<T> visitor)
            => visitor.VisitChild(childPaymentService);
    }
}
