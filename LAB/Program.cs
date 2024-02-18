using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // 3 - виклики методів
        Thread1();
        Thread2();
        Thread3();

        Async1();
        Async2();
        Async3();

    }

    //1 - thread
    static void Thread1()
    {
        Thread thread = new Thread(() =>
        {
            Console.WriteLine("Перший метод...");
            Thread.Sleep(1000);
            Console.WriteLine("Перший метод завершено.");
        });
        thread.Start();
    }

    static void Thread2()
    {
        Thread thread = new Thread(() =>
        {
            Console.WriteLine("Другий метод...");
            Thread.Sleep(2000);
            Console.WriteLine("Другий метод завершено.");
        });
        thread.Start();
    }

    static void Thread3()
    {
        Thread thread = new Thread(new ParameterizedThreadStart((obj) =>
        {
            Console.WriteLine($"Третій метод запущено з параметром: {obj}");
            Thread.Sleep(1500);
            Console.WriteLine("Третій метод завершено.");
        }));
        thread.Start("параметр");
    }

    // 2 - async - await
    static async void Async1()
    {
        Console.WriteLine("Перший метод аsync...");
        await Task.Delay(2500); 
        Console.WriteLine("Перший метод аsync завершено.");
    }

    static async void Async2()
    {
        Console.WriteLine("Другий метод аsync...");
        await Task.Delay(3000);
        Console.WriteLine("Другий метод аsync завершено.");
    }

    static async void Async3()
    {
        Console.WriteLine("Третій метод аsync...");
        await Task.Delay(1500);
        Console.WriteLine("Третій метод аsync завершено.");
    }
}
