using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Rx101
{
    public static class Demo04
    {
        public static void SimulateAsyncCalls()
        {
            Console.WriteLine("Sim async calls");
            // Setup
            var httpCallSim = HttpCallSim();
            
            // Execution
            httpCallSim
                .Select(httpCall => httpCall().ToObservable())
                .Switch()
                .Subscribe(value => Console.WriteLine($"Value: {value}"));
        }

        private static IObservable<Func<Task<float>>> HttpCallSim()
        {
            return new List<Func<Task<float>>>
            {
                async () =>
                {
                    // Simulate a slow preceding request
                    await Task.Delay(5000);
                    return 13.37f;
                },
                async () =>
                {
                    // Simulate a faster second request
                    await Task.Delay(2000);
                    return 42;
                },
            }.ToObservable();
        }
    }
}