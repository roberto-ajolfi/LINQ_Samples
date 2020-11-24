using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LINQ.ConsoleApp
{
    // 1. dichiara il delegate
    public delegate void ProcessStarted();
    public delegate void ProcessCompleted(int duration);

    //public delegate void BusinessProcessEvent(int value);

    public class BusinessProcess
    {
        //2. dichiara l'evento
        public event ProcessStarted Started;
        public event ProcessCompleted Completed;

        //public event BusinessProcessEvent Started;
        //public event BusinessProcessEvent Completed;

        public event EventHandler StartedCore;
        public event EventHandler<ProcessEndEventArgs> CompletedCore;

        public void ProcessData()
        {
            Console.WriteLine("=== Starting Process ===");
            Thread.Sleep(2000);

            Console.WriteLine("=== Process Started ===");
            // 'sollevo' l'evento Started ...
            if(Started != null)
                Started();
            if (StartedCore != null)
                StartedCore(this, EventArgs.Empty);

            Thread.Sleep(3000);
            Console.WriteLine("=== Process Completed ===");
            //3. solleva l'evento
            if (Completed != null)
                Completed(5000);
            if (CompletedCore != null)
                CompletedCore(this, 
                    new ProcessEndEventArgs { 
                        Duration = 4500,
                        ShipToCountry = "Italy"
                    });
        }
    }

    public class ProcessEndEventArgs
    {
        public int Duration { get; set; }
        public string ShipToCountry { get; set; }
    }
}
