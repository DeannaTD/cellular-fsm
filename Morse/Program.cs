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
            Console.OutputEncoding = Encoding.UTF8;

            int intRule = 0;
            int imageWidth = 0, imageHeight = 0;

            imageWidth = GetCheckedValue("Input image's width", (int value) => value > 0 && value < 2048);
            imageHeight = GetCheckedValue("Input image's height", (int value) => value > 0 && value < 2048);
            intRule = GetCheckedValue("Input the rule", (int value) => value >= 0 && value < 256);

            ElementaryCellularMachine machine = new ElementaryCellularMachine(intRule, false, imageWidth);

            Bitmap map = machine.GenerateImage(imageHeight);

            map.Save("map.png", ImageFormat.Png);
        }

        static int GetCheckedValue(string message, Func<int, bool> checkFunc)
        {
            int result = 0;
            Console.WriteLine(message);
            do
            {
                if (!int.TryParse(Console.ReadLine(), out result)) Console.WriteLine("Value should be an integer, try again");
                else if (!checkFunc(result)) Console.WriteLine("Error, value is out of bounds, try again");
                else return result;
            }
            while(true);
        }
    }
}