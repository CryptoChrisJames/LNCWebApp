using System;
using System.Collections.Generic;
using System.Text;
using Stripe;

namespace LNCLibrary2.Logic
{
    public class CashingOut
    {
        public bool StripePayment(string StripeEmail, string stripeToken, int StripeTotal)
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
                    Amount = StripeTotal,
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

        public string ConfirmationNumber()
        {

            Random random = new Random();
            string cnum = "";
            int i;
            for (i = 1; i < 16; i++)
            {
                cnum += random.Next(0, 9);
            }
            return cnum;
        }
    }
}
