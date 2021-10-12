using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        static bool IsBusy;
        static async Task Main(string[] args)
        {
            Console.Title = "C# Contextual keywords, async and await";

            IsBusy = true;
            Console.WriteLine("Long running task started...");

            //Here we are executing a long running task like a service call, but we can execute the rest of the code without waiting for it to be completed since it's an async method.
            var longRunningTaskReturn = LongRunningTask();

            //Inform the user that we are doing something.
            ShowBusyIndicator();

            //Here if we need the result from the LongRunningTask() async method, before executing the rest of the code, we can await the result of LongRunningTask(). It means below code wont execute until the result is returned.
            await longRunningTaskReturn;

            //If we pass the awaited long running task result, it means the task is completed and the result is returned.
            Console.WriteLine("Long running task completed...");

            Console.WriteLine("Hiding busy indicator...");

            //We can now turnoff the busy indicator and use the result.
            IsBusy = false;

            Console.WriteLine("Displaying result...");

            //Use the returned result from the long running task.
            var longRunningTaskResult = longRunningTaskReturn.Result;

            foreach (var item in longRunningTaskResult)
            {
                Console.WriteLine($"{longRunningTaskResult.IndexOf(item)}-{item}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Completed...");
            Console.ReadLine();
        }

        //Long running async task that returns a list of string.
        public static async Task<List<string>> LongRunningTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            return new List<string> { "Result1", "Result2", "Result3", "Result4", "Result5" };
        }

        //Method to execute while we are running an async task
        public static async void ShowBusyIndicator()
        {
            while (IsBusy)
            {
                Console.WriteLine("Showing busy indicator...");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
