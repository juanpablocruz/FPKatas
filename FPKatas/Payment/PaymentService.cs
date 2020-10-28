using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Payment
{
    public class PaymentService
    {
        public string Name { get; }
        public string Action { get; }
        public PaymentService(string name, string action)
        {
            this.Name = name;
            this.Action = action;
        }
    }

    public class ChildPaymentService
    {
        public string OriginalTransactionKey { get; }
        public PaymentService PaymentService { get; }

        public ChildPaymentService(
            string originalTransactionKey,
            PaymentService paymentService)
        {
            this.OriginalTransactionKey = originalTransactionKey;
            this.PaymentService = paymentService;
        }
    }
}
