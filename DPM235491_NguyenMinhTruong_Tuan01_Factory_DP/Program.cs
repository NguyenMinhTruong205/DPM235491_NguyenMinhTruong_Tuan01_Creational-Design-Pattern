using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Factory_DP
{
    // 1. Product Interface
    public interface IProduct
    {
        string Operation();
    }

    // 2. Concrete Products
    public class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    public class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    // 3. Creator Abstract Class
    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();

        public string SomeOperation()
        {
            var product = FactoryMethod();
            var result = "Creator: The same creator's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    // 4. Concrete Creators
    public class ConcreteCreator1 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    public class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    // 5. Client Code
    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

        public void ClientCode(Creator creator)
        {
            Console.WriteLine("Client: I'm not aware of the creator's class, but it still works.\n"
                + creator.SomeOperation());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
            Console.ReadLine();
        }
    }
}