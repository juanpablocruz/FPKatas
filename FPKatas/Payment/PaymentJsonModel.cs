using FPKatas.Church;

namespace FPKatas.Payment
{
    public class PaymentJsonModel
    {
        public string Name { get; set; }

        public string Action { get; set; }

        public IChurchBoolean StartRecurrent { get; set; }

        public IMaybe<string> TransactionKey { get; set; }
    }

    public static class PaymentJsonModelExtensions
    {
        public static PaymentJsonModel ToJson(this IPaymentType payment)
            => payment.Accept(new PaymentTypeToJsonVisitor());
    }

    public class PaymentTypeToJsonVisitor : IPaymentTypeVisitor<PaymentJsonModel>
    {
        public PaymentJsonModel VisitIndividual(PaymentService individual)
            => new PaymentJsonModel
            {
                Name = individual.Name,
                Action = individual.Action,
                StartRecurrent = new ChurchFalse(),
                TransactionKey = new Nothing<string>()
            };
        public PaymentJsonModel VisitParent(PaymentService parent)
        {
            return new PaymentJsonModel
            {
                Name = parent.Name,
                Action = parent.Action,
                StartRecurrent = new ChurchTrue(),
                TransactionKey = new Nothing<string>()
            };
        }

        public PaymentJsonModel VisitChild(ChildPaymentService child)
        {
            return new PaymentJsonModel
            {
                Name = child.PaymentService.Name,
                Action = child.PaymentService.Action,
                StartRecurrent = new ChurchFalse(),
                TransactionKey = new Just<string>(child.OriginalTransactionKey)
            };
        }
    }
}
