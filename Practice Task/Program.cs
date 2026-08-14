using System;

namespace PaymentSystem
{

    public class PaymentMethod
    {

        public decimal Balance { get; protected set; }

        public PaymentMethod(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        public virtual bool Charge(decimal amount)
        {
            if (amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }
    }


    public class CreditCard : PaymentMethod
    {
        public decimal CreditLimit { get; }

        public CreditCard(decimal initialBalance, decimal limit)
            : base(initialBalance)
        {
            CreditLimit = limit;
        }

        public override bool Charge(decimal amount)
        {

            if (amount > Balance + CreditLimit)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }
    }

    public class DebitCard : PaymentMethod
    {
        public DebitCard(decimal initialBalance)
            : base(initialBalance)
        {
        }
    }

    internal class Program
    {
        public static bool ProcessPayment(PaymentMethod paymentMethod, decimal amount)
        {
            if (paymentMethod == null) return false;
            return paymentMethod.Charge(amount);
        }

        static void Main(string[] args)
        {
            CreditCard card01 = new CreditCard(500, 300);
            bool result01 = ProcessPayment(card01, 700);

            if (result01)
            {
                Console.WriteLine("Payment Successful!");
                Console.WriteLine($"Remaining Balance: {card01.Balance}");
            }
            else
            {
                Console.WriteLine("Payment Failed: Credit limit exceeded.");
            }

            Console.WriteLine("-----------------------------------");


            DebitCard card02 = new DebitCard(500);
            bool result02 = ProcessPayment(card02, 700);

            if (result02)
            {
                Console.WriteLine("Payment Successful!");
                Console.WriteLine($"Remaining Balance: {card02.Balance}");
            }
            else
            {
                Console.WriteLine("Payment Failed: Insufficient balance.");
            }

            Console.ReadKey();
        }
    }
}