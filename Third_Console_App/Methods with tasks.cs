using System;
using System.Threading.Tasks;

public class TaskExamples
{
    // метод, що запускає 3 таски з випадковою затримкою та очікує завершення
    public static async Task RunTasksAndWait()
    {
        Random rnd = new Random();

        Task[] tasks = new Task[3];

        for (int i = 0; i < 3; i++)
        {
            int taskNum = i + 1;
            tasks[i] = Task.Run(async () =>
            {
                int delay = rnd.Next(1000, 3001);
                await Task.Delay(delay);
                Console.WriteLine($"Task {taskNum} finished after {delay} ms");
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All tasks finished!");
    }

    // метод, що запускає 3 таски і пише результат першої завершеної
    public static async Task RunTasksAndPrintFirstResult()
    {
        Random rnd = new Random();

        Task<string>[] tasks = new Task<string>[3];

        for (int i = 0; i < 3; i++)
        {
            int taskNum = i + 1;
            tasks[i] = Task.Run(async () =>
            {
                int delay = rnd.Next(1000, 3001);
                await Task.Delay(delay);
                return $"Task {taskNum} result (after {delay} ms)";
            });
        }

        Task<string> firstFinished = await Task.WhenAny(tasks);
        Console.WriteLine("First completed: " + await firstFinished);
    }
}
