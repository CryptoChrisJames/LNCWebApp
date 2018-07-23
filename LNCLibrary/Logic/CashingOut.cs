using System;
using System.Collections.Generic;
using System.Text;
using Stripe;

namespace LNCLibrary.Logic
{
    public class CashingOut
    {
        public bool StripePayment(string StripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            try
            {
                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = StripeEmail,
                    SourceToken = stripeToken
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = 500,
                    Description = "Sample Charge",
                    Currency = "usd",
                    CustomerId = customer.Id
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
