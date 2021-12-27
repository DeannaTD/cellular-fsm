using System;
using System.Text;

namespace Morse
{
    class Program
    {
        /*
         * 73 - 0100 1001
         * 90 - 0101 1010
         * 184 - 1011 1000
         *|-----------------------------------------------|
         *| 111 | 110 | 101 | 100 | 011 | 010 | 001 | 000 |
         *|-----+-----+-----+-----+-----+-----+-----+-----|
         *|  0  |  1  |  0  |  0  |  1  |  0  |  0  |  1  |
         *|-----------------------------------------------|
        
         *|-----------------------------------------------|
         *| 111 | 110 | 101 | 100 | 011 | 010 | 001 | 000 |
         *|-----+-----+-----+-----+-----+-----+-----+-----|
         *|  0  |  1  |  0  |  1  |  1  |  0  |  1  |  0  |
         *|-----------------------------------------------|
         *
         * 0000010000
         */
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            int intRule = -1;
            do
            {
                Console.Write("Input the rule (0-256): ");
                intRule = GetRule(Console.ReadLine());
            }
            while (intRule == -1);
            
            int N = 512;

            int[] config = new int[N];
            int[] prev = new int[N];
            Random random = new Random();
            for (int i = 0; i < N; i++) config[i] = random.Next(2);
            for (int i = 0; i < N; i++) prev[i] = config[i];

            //for (int i = 0; i < N; i++) config[i] = 0;
            //config[N / 2] = 1;
            //for (int i = 0; i < N; i++) prev[i] = config[i];
            Show(config);
            string result = "";
            int rule = 0;

            string binRule = Convert.ToString(intRule, 2);
            while(binRule.Length != 8)
            {
                binRule.Insert(0, "0");
            }

            while (true)
            {
                for(int i = 0; i<N; i++)
                {
                    result = "";
                    if (i == 0) result += prev[N - 1];
                    else result += prev[i - 1];
                    result += prev[i];
                    if (i == N - 1) result += prev[0];
                    else result += prev[i + 1];
                    rule = Convert.ToInt32(result, 2);
                    switch (rule)
                    {
                        case 0: config[i] = Convert.ToInt32(binRule[7].ToString()); break;
                        case 1: config[i] = Convert.ToInt32(binRule[6].ToString()); break;
                        case 2: config[i] = Convert.ToInt32(binRule[5].ToString()); break;
                        case 3: config[i] = Convert.ToInt32(binRule[4].ToString()); break;
                        case 4: config[i] = Convert.ToInt32(binRule[3].ToString()); break;
                        case 5: config[i] = Convert.ToInt32(binRule[2].ToString()); break;
                        case 6: config[i] = Convert.ToInt32(binRule[1].ToString()); break;
                        case 7: config[i] = Convert.ToInt32(binRule[0].ToString()); break;
                    }
                }
                Show(config);
                for (int i = 0; i < N; i++) prev[i] = config[i];
            }
        }

        static void Show(int[] config)
        {
            for(int i = 0; i<config.Length; i++)
            {
                if (config[i] == 0) Console.Write(" ");
                else Console.Write("●");
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(50);
        }

        static int GetRule(string rule)
        {
            int answer = -1;
            try
            {
                answer = Convert.ToInt32(rule);
            }
            catch
            {
                Console.Clear();
            }
            answer = answer >= 0 && answer < 256 ? answer : -1;
            if(answer == -1)
            {
                Console.WriteLine("Error! Rule must be a number between 0 and 256");
            }
            return answer;
        }

    }
}


/*
 public class Example
{
   private static System.Timers.Timer aTimer;
   
   public static void Main()
   {
      SetTimer();

      Console.WriteLine("\nPress the Enter key to exit the application...\n");
      Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
      Console.ReadLine();
      aTimer.Stop();
      aTimer.Dispose();
      
      Console.WriteLine("Terminating the application...");
   }

   private static void SetTimer()
   {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                          e.SignalTime);
    }
}
*/