using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // === Частина 1 ===
        ConcreteClass obj = new ConcreteClass();
        obj.AbstractMethod();
        obj.NormalMethod();
        Console.WriteLine("Process(5) = " + obj.Process(5));
        obj.ShowMessage("Hello World!");

        Console.WriteLine("\n--- Tasks demo ---");

        // === Частина 2 ===
        await TaskExamples.RunTasksAndWait();
        await TaskExamples.RunTasksAndPrintFirstResult();
    }
}