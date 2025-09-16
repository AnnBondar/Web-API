using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Арифметичні операції
        ArithmeticDemo();

        // Логічні оператори та умовні оператори
        ConditionDemo();

        // Цикли
        LoopDemo();

        // Створення об’єкта власного класу
        MyClass obj1 = new MyClass(); // виклик конструктора без параметрів
        MyClass obj2 = new MyClass("Hanna Bondar"); // виклик конструктора з параметром

        obj1.PublicMethod();
        obj2.PublicMethod();
    }

    // Метод для арифметичних операцій
    static void ArithmeticDemo()
    {
        int a = 10;
        double b = 3.5;

        Console.WriteLine($"Addition: {a + b}");
        Console.WriteLine($"Subtraction: {a - b}");
        Console.WriteLine($"Multiplication: {a * b}");
        Console.WriteLine($"Division: {a / b}");
        Console.WriteLine($"Modulo: {a % 3}");
    }

    // Метод з логічними операторами і умовами
    static void ConditionDemo()
    {
        int x = 15;

        if (x > 10 && x < 20)
        {
            Console.WriteLine("x > 10 & < 20");
        }
        else if (x == 10 || x == 20)
        {
            Console.WriteLine("x = 10 / 20");
        }
        else
        {
            Console.WriteLine("A different case");
        }

        // switch
        switch (x)
        {
            case 5:
                Console.WriteLine("x = 5");
                break;
            case 15:
                Console.WriteLine("x = 15");
                break;
            default:
                Console.WriteLine("x is different");
                break;
        }

        // тернарний оператор
        string result = (x > 10) ? "Більше 10" : "Не більше 10";
        Console.WriteLine(result);
    }

    // Метод з циклами
    static void LoopDemo()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        List<string> names = new List<string> { "Olga", "Ihor", "Ira" };

        Console.WriteLine("\n--- for ---");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine(numbers[i]);
        }

        Console.WriteLine("\n--- foreach ---");
        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("\n--- while ---");
        int counter = 0;
        while (counter < 3)
        {
            Console.WriteLine($"while: {counter}");
            counter++;
        }

        Console.WriteLine("\n--- do...while ---");
        int j = 0;
        do
        {
            Console.WriteLine($"do...while: {j}");
            j++;
        } while (j < 3);
    }
}

