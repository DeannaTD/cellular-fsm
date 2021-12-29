using FSM;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
            int imageWidth = 0, imageHeight = 0;

            Console.Write("Input image's width: ");
            int.TryParse(Console.ReadLine(), out imageWidth);

            Console.Write("Input image's height: ");
            int.TryParse(Console.ReadLine(), out imageHeight);

            do
            {
                Console.Write("Input the rule (0-256): ");
                intRule = GetRule(Console.ReadLine());
            }
            while (intRule == -1);

            ElementaryCellularMachine machine = new ElementaryCellularMachine(intRule, false, imageWidth);

            int N = imageWidth;

            int globalY = 0;

            Bitmap map = new Bitmap(imageWidth, imageHeight);

            for (int i = 0; i < machine.Size; i++)
            {
                if (!machine.State[i]) map.SetPixel(i, globalY, Color.White);
                else map.SetPixel(i, globalY, Color.Black);
            }
            globalY++;

            string binRule = Convert.ToString(intRule, 2);
            while(binRule.Length != 8)
            {
                binRule = binRule.Insert(0, "0");
            }

            while (globalY < imageHeight)
            {
                machine.GenerateNextState();
                for (int x = 0; x < machine.Size; x++)
                {
                    if (!machine.State[x]) map.SetPixel(x, globalY, Color.White);
                    else map.SetPixel(x, globalY, Color.Black);
                }
                globalY++;
            }

            map.Save("map.png", ImageFormat.Png);
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