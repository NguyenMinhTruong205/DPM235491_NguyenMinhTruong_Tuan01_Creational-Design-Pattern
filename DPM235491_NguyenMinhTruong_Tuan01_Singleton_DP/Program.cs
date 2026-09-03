using System;

namespace DPM235491_NguyenMinhTruong_Tuan01_Singleton_DP
{
    // Lớp Singleton đảm bảo an toàn đa luồng (Thread-safe)
    public sealed class Singleton
    {
        private Singleton() { }

        private static Singleton _instance;
        private static readonly object _lock = new object();

        public static Singleton GetInstance(string value)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        public string Value { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== REFACTORING GURU: SINGLETON PATTERN ===\n");

            Singleton s1 = Singleton.GetInstance("FOO");
            Singleton s2 = Singleton.GetInstance("BAR");

            if (s1 == s2)
            {
                Console.WriteLine("Singleton hoat dong: Ca hai bien deu tro toi cung mot the hien (Instance).");
                Console.WriteLine($"Gia tri s1: {s1.Value}");
                Console.WriteLine($"Gia tri s2: {s2.Value}");
            }
            else
            {
                Console.WriteLine("Singleton that bai: Cac bien tro toi cac the hien khac nhau.");
            }

            Console.ReadLine();
        }
    }
}