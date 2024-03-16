using System;
using AnotherDecoratorPatternExample;
// Decorator pattern
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class StarbuzzCofee
    {
        static void Main(string[] args)
        {
            // Order up an espresso, no condiments and print the details
            Beverage beverage = new Espresso();

            System.Console.WriteLine(beverage.GetDescription() + " $" + beverage.Cost());

            // Make a DarkRoast object
            Beverage beverage2 = new DarkRoast();
            // Wrap it with Mocha
            beverage2 = new Mocha(beverage2);
            // Wrap it with a second mocha
            beverage2 = new Mocha(beverage2);
            // Wrap it with Whip
            beverage2 = new Whip(beverage2);

            System.Console.WriteLine(beverage2.GetDescription() + " $ " + beverage2.Cost());

            // create a HouseBlend with Soy, Mocha and Whip
            Beverage beverage3 = new HouseBlend();
            beverage3 = new Soy(beverage3);
            beverage3 = new Mocha(beverage3);
            beverage3 = new Whip(beverage3);

            System.Console.WriteLine(beverage3.GetDescription() + " $ " + beverage3.Cost());


            // This is another example of decorator pattern
            Client client = new Client();
            var simple = new ConcreteComponent();
            System.Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simple);
            System.Console.WriteLine();

            // ...as well as decorated ones.

            // Note how decorators can wrap not only simple components but the other decorators
            // as well
            ConcreteDecoratorA decorator1 = new ConcreteDecoratorA(simple);
            System.Console.WriteLine("Client Now I've got a decorated component:");
            client.ClientCode(decorator1);

            // solving the discount problmen
            IOrder baseOrder = new BaseOrder(100);

            // 10% discount
            IOrder orderWithPercentageDiscount = new PercentageDiscount(baseOrder, 10);
            System.Console.WriteLine("Total after applying 10% discount: $" + orderWithPercentageDiscount.CalculateTotal());

            IOrder orderWithFixedAmountDiscount = new FixedAmountDiscount(baseOrder, 20);
            System.Console.WriteLine("Total after applying $20 discount: $", orderWithFixedAmountDiscount.CalculateTotal());

            //Applying multiple discounts
            IOrder orderWithMultipleDiscounts = new PercentageDiscount(new FixedAmountDiscount(baseOrder, 20), 15);
            System.Console.WriteLine("Total after applying $20 fixed amount discount and 15% percentage discount: $" + orderWithMultipleDiscounts.CalculateTotal());

        }

    }

    public abstract class Beverage
    {
        public virtual string Description { get; } = "Unknown Beverage";

        public virtual string GetDescription()
        {
            return Description;
        }
        public abstract double Cost();
    }

    public abstract class CondimentDecorator : Beverage
    {
        public abstract override string GetDescription();
    }

    public class Espresso : Beverage
    {
        public override string Description { get; } = "Espresso";
        public override double Cost()
        {
            return 1.99;
        }
    }

    public class HouseBlend : Beverage
    {
        public override string Description { get; } = "House Blend Coffee";
        public override double Cost()
        {
            return .89;
        }
    }

    public class DarkRoast : Beverage
    {
        public override string Description { get; } = "Dark Roast Coffee";
        public override double Cost()
        {
            return .99;
        }
    }

    public class Mocha : CondimentDecorator
    {
        Beverage beverage;
        public Mocha(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override double Cost()
        {
            return .20 + beverage.Cost();
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Mocha";
        }
    }

    public class Soy : CondimentDecorator
    {
        Beverage beverage;
        public Soy(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override double Cost()
        {
            return .15 + beverage.Cost();
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Soy";
        }
    }

    public class Whip : CondimentDecorator
    {
        Beverage beverage;

        public Whip(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override double Cost()
        {
            return .10 + beverage.Cost();
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Whip";
        }
    }

    // You are developing a software application for a restaurant management system.
    // In this system, you need to implement varous types of discounts that can be applied to orders.
    // These discounts may include percentage discounts, fixed amount discounts, or any custom
    // based discounts. However, you want to ensure that adding new types of discounts or 
    // modifying existing ones doesn't heavily impact the core codebase.

    interface IOrder
    {
        double CalculateTotal();
    }

    // concrete implementation of an order
    class BaseOrder : IOrder
    {
        private double _total;
        public BaseOrder(double total)
        {
            this._total = total;
        }
        public double CalculateTotal()
        {
            return _total;
        }
    }

    abstract class OrderDecorator : IOrder
    {
        protected IOrder _order;
        public OrderDecorator(IOrder order)
        {
            _order = order;
        }

        public abstract double CalculateTotal();
    }

    // concrete decorator class for applying percentage discount

    class PercentageDiscount : OrderDecorator
    {
        private double _discountPercentage;
        public PercentageDiscount(IOrder order, double discountPercentage) : base(order)
        {
            _discountPercentage = discountPercentage;
        }

        public override double CalculateTotal()
        {
            double originalTotal = _order.CalculateTotal();
            double discountPercentage = originalTotal * (_discountPercentage / 100);
            return originalTotal - discountPercentage;
        }
    }

    class FixedAmountDiscount : OrderDecorator
    {
        private double _discountAmount;

        public FixedAmountDiscount(IOrder order, double discountAmount) : base(order)
        {
            _discountAmount = discountAmount;
        }

        public override double CalculateTotal()
        {
            double originalTotal = _order.CalculateTotal();
            return originalTotal - _discountAmount;
        }
    }

}
