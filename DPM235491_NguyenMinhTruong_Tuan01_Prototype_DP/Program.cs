using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Prototype_DP
{
    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    public class Person
    {
        public int Age;
        public string Name;
        public IdInfo IdInfo;

        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person)this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            p1.Age = 42;
            p1.Name = "Jack Daniels";
            p1.IdInfo = new IdInfo(666);

            // Shallow Copy (Sao chép nông)
            Person p2 = p1.ShallowCopy();
            // Deep Copy (Sao chép sâu)
            Person p3 = p1.DeepCopy();

            Console.WriteLine("--- BAN DAU ---");
            Console.WriteLine($"p1: {p1.Name}, {p1.Age}, ID: {p1.IdInfo.IdNumber}");
            Console.WriteLine($"p2: {p2.Name}, {p2.Age}, ID: {p2.IdInfo.IdNumber}");
            Console.WriteLine($"p3: {p3.Name}, {p3.Age}, ID: {p3.IdInfo.IdNumber}");

            // Thay đổi giá trị của p1
            p1.Age = 32;
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 777;

            Console.WriteLine("\n--- SAU KHI THAY DOI P1 ---");
            Console.WriteLine($"p1: {p1.Name}, {p1.Age}, ID: {p1.IdInfo.IdNumber}");
            Console.WriteLine($"p2 (Shallow): {p2.Name}, {p2.Age}, ID: {p2.IdInfo.IdNumber}");
            Console.WriteLine($"p3 (Deep): {p3.Name}, {p3.Age}, ID: {p3.IdInfo.IdNumber}");

            Console.ReadLine();
        }
    }
}