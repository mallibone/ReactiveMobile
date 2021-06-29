using System;

namespace Rx101
{
    class Program
    {
        static void Main(string[] _)
        {
            Demo01.SimpleComparison();
            // Demo02.FilterEventFlows();
            // Demo03.MergeFlows();
            // Demo04.SimulateAsyncCalls();
            // Demo05();
            Console.ReadLine();
        }

        private static void Demo05()
        {
            var demo05 = new Demo05();
            Console.ReadLine();
            demo05.Dispose();
        }


    }

}