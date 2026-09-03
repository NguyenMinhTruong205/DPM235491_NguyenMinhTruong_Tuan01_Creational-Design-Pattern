using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Builder_DP
{
    // 1. Sản phẩm 1: Chiếc Ô tô (Car)
    public class Car
    {
        public int Seats { get; set; }
        public string Engine { get; set; }
        public bool HasTripComputer { get; set; }
        public bool HasGPS { get; set; }

        public void DisplayInfo()
        {
            Console.WriteLine($"[Car Details] Seats: {Seats}, Engine: {Engine}, Trip Computer: {HasTripComputer}, GPS: {HasGPS}");
        }
    }

    // 2. Sản phẩm 2: Sách hướng dẫn sử dụng (Manual)
    public class Manual
    {
        public string SeatInstructions { get; set; }
        public string EngineInstructions { get; set; }
        public string TripComputerInstructions { get; set; }
        public string GPSInstructions { get; set; }

        public void DisplayInfo()
        {
            Console.WriteLine("=== CAR MANUAL ===");
            Console.WriteLine($"- Seats: {SeatInstructions}");
            Console.WriteLine($"- Engine: {EngineInstructions}");
            Console.WriteLine($"- Trip Computer: {TripComputerInstructions}");
            Console.WriteLine($"- GPS: {GPSInstructions}\n");
        }
    }

    // 3. Giao diện Builder chung
    public interface IBuilder
    {
        void Reset();
        void SetSeats(int seats);
        void SetEngine(string engine);
        void SetTripComputer(bool hasTripComputer);
        void SetGPS(bool hasGPS);
    }

    // 4. Concrete Builder 1: Xây dựng đối tượng Car
    public class CarBuilder : IBuilder
    {
        private Car _car;

        public CarBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._car = new Car();
        }

        public void SetSeats(int seats)
        {
            this._car.Seats = seats;
        }

        public void SetEngine(string engine)
        {
            this._car.Engine = engine;
        }

        public void SetTripComputer(bool hasTripComputer)
        {
            this._car.HasTripComputer = hasTripComputer;
        }

        public void SetGPS(bool hasGPS)
        {
            this._car.HasGPS = hasGPS;
        }

        public Car GetProduct()
        {
            Car product = this._car;
            this.Reset(); // Đặt lại để chuẩn bị tạo xe mới
            return product;
        }
    }

    // 5. Concrete Builder 2: Xây dựng đối tượng Manual
    public class CarManualBuilder : IBuilder
    {
        private Manual _manual;

        public CarManualBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._manual = new Manual();
        }

        public void SetSeats(int seats)
        {
            this._manual.SeatInstructions = $"Instruction for installing {seats} seats.";
        }

        public void SetEngine(string engine)
        {
            this._manual.EngineInstructions = $"Instructions for operating {engine}.";
        }

        public void SetTripComputer(bool hasTripComputer)
        {
            this._manual.TripComputerInstructions = hasTripComputer
                ? "Instructions for using Trip Computer."
                : "No Trip Computer installed.";
        }

        public void SetGPS(bool hasGPS)
        {
            this._manual.GPSInstructions = hasGPS
                ? "Instructions for using GPS Navigation."
                : "No GPS installed.";
        }

        public Manual GetProduct()
        {
            Manual product = this._manual;
            this.Reset(); // Đặt lại để chuẩn bị tạo sách mới
            return product;
        }
    }

    // 6. Director Class: Điều phối quy trình xây dựng sản phẩm
    public class Director
    {
        public void ConstructSportsCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(2);
            builder.SetEngine("Sport Engine V8");
            builder.SetTripComputer(true);
            builder.SetGPS(true);
        }

        public void ConstructSUV(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(7);
            builder.SetEngine("SUV Engine V6");
            builder.SetTripComputer(true);
            builder.SetGPS(false);
        }
    }

    // 7. Client Code (Hàm main)
    internal class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();

            // 1. Tạo xe thể thao (Sports Car)
            CarBuilder carBuilder = new CarBuilder();
            director.ConstructSportsCar(carBuilder);
            Car sportsCar = carBuilder.GetProduct();
            sportsCar.DisplayInfo();

            // 2. Tạo sách hướng dẫn tương ứng cho xe thể thao (Sports Car Manual)
            CarManualBuilder manualBuilder = new CarManualBuilder();
            director.ConstructSportsCar(manualBuilder);
            Manual sportsCarManual = manualBuilder.GetProduct();
            sportsCarManual.DisplayInfo();

            // 3. Tạo xe SUV
            director.ConstructSUV(carBuilder);
            Car suvCar = carBuilder.GetProduct();
            suvCar.DisplayInfo();

            Console.ReadLine();
        }
    }
}