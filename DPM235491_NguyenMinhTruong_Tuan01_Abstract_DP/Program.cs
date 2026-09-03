using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Abstract_DP
{
    // 1. Abstract Products
    public interface IButton
    {
        void Paint();
    }

    public interface ICheckbox
    {
        void Paint();
    }

    // 2. Concrete Products (Windows Variant)
    public class WinButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("Render a button in Windows style.");
        }
    }

    public class WinCheckbox : ICheckbox
    {
        public void Paint()
        {
            Console.WriteLine("Render a checkbox in Windows style.");
        }
    }

    // 3. Concrete Products (macOS Variant)
    public class MacButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("Render a button in macOS style.");
        }
    }

    public class MacCheckbox : ICheckbox
    {
        public void Paint()
        {
            Console.WriteLine("Render a checkbox in macOS style.");
        }
    }

    // 4. Abstract Factory
    public interface IGUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    // 5. Concrete Factories
    public class WinFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }

        public ICheckbox CreateCheckbox()
        {
            return new WinCheckbox();
        }
    }

    public class MacFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ICheckbox CreateCheckbox()
        {
            return new MacCheckbox();
        }
    }

    // 6. Client Code (Application)
    public class Application
    {
        private readonly IGUIFactory _factory;
        private IButton _button;
        private ICheckbox _checkbox;

        public Application(IGUIFactory factory)
        {
            _factory = factory;
        }

        public void CreateUI()
        {
            _button = _factory.CreateButton();
            _checkbox = _factory.CreateCheckbox();
        }

        public void Paint()
        {
            _button.Paint();
            _checkbox.Paint();
        }
    }

    // 7. Application Configurator & Main Entry
    internal class Program
    {
        static void Main(string[] args)
        {
            IGUIFactory factory;

            // Giả lập đọc cấu hình hệ điều hành môi trường
            string osName = "Windows"; // Hoặc có thể đổi thành "Mac"

            if (osName == "Windows")
            {
                factory = new WinFactory();
            }
            else if (osName == "Mac")
            {
                factory = new MacFactory();
            }
            else
            {
                throw new Exception("Error! Unknown operating system.");
            }

            // Khởi tạo ứng dụng với nhà máy tương ứng
            Application app = new Application(factory);
            app.CreateUI();
            app.Paint();

            Console.ReadLine();
        }
    }
}