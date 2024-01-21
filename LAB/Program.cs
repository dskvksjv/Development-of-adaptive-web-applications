using System;
using System.IO;

class Program
{

    static void LoremIpsum()
    {
        try
        {
            string filePath = @"C:\Users\victo\source\repos\LAB\LAB\LoremIpsum.txt";
            loremIpsumText = File.ReadAllText(filePath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не знайдено.");
            Environment.Exit(1);
        }
    }

    static void WordCount()
    {
        int wordCount = loremIpsumText.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Console.WriteLine($"Кількість слів у тексті: {wordCount}");
    }

    static void Math()
    {
        Console.Write("Напиши число: ");
        double inputNumber;
        if (double.TryParse(Console.ReadLine(), out inputNumber))
        {
            double result = Square(inputNumber);
            Console.WriteLine($"Квадрат введеного числа: {result}");
        }
        else
        {
            Console.WriteLine("Помилочка. Спробуй ще раз!");
        }
    }

    static double Square(double number)
    {
        return number * number;
    }

    static string loremIpsumText;

    static void Main()
    {
        LoremIpsum();

        while (true)
        {
            Console.WriteLine("Оберіть опцію:");
            Console.WriteLine("1. Вивести кількість слів у тексті Lorem Ipsum");
            Console.WriteLine("2. Виконати математичну операцію");
            Console.WriteLine("3. Вийти");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        WordCount();
                        break;
                    case 2:
                        Math();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Сталася помилочка");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Сталася помилочка. Спробуй ще раз!");
            }

            Console.WriteLine();
        }
    }
}

