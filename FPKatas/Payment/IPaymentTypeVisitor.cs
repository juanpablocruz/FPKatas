using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Payment
{
    public interface IPaymentTypeVisitor<T>
    {
        T VisitIndividual(PaymentService individual);
        T VisitParent(PaymentService parent);
        T VisitChild(ChildPaymentService child);
    }
}
