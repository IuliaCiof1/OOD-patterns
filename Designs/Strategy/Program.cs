using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strategy
{
    /* Strategy pattern - Behavioral pattern
     * - defines a family of algorithms and puts them in a separate class and makes their objects interchangeable
     * - used when you want to use different variants of an algorithm wihin an object and be able to switch the algorithm at run time
     * - used when there are a lot of similar classes that only differ in the way they execute some behavior
     * - used to isolate bussines logic of a class from the implementation details of algorithms
     * - useful when your class has a massive conditional statement that switches between variants of the same algorithm
     * - not useful when you have just a couple of algorithms
    */

    class CreditCard
    {
        public int amount;
        public int Amount { get { return amount; } set { amount = value; } }
    }


    //**** Strategy Interface ****
    //Declares operations common to all supported versions of some algorithms
    interface PaymentStrategy
    {
        void pay(int amaount);
    }

    // **** ConcreteStrategies ****
    //The interfaces makes them interchangeable in the context
    class PaymentByCreditCard : PaymentStrategy
    {
        CreditCard card;

        public void pay(int amount)
        {
            card = new CreditCard();
            Console.WriteLine($"Paying {amount} with Credit Card");
            card.Amount = card.Amount - amount;
        }
    }


    class PaymentByPayPal : PaymentStrategy
    {
        string email;
        string password;

        public void pay(int amount)
        {
            email = "email";
            password = "pass";
            Console.WriteLine($"Paying {amount} with PayPal");
        }
    }

    // **** Context ****
    //Provides a setter so that the strategy can be swiched at runtime
    class PaymentService
    {
        int cost;
        PaymentStrategy strategy;

        public void processOrder()
        {
            strategy.pay(cost);
        }

        public void setStrategy(PaymentStrategy strategy)
        {
            this.strategy = strategy; 
        }
    }

    // **** Client ****
    //Picks a concrete strategy and passes it to the context
    class Program 
    {
        static void Main(string[] args)
        {
            PaymentService service = new PaymentService();

            service.setStrategy(new PaymentByPayPal());
            service.processOrder();
            Console.ReadKey();
        }
    }
}
