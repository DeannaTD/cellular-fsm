using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FSM
{
    class ElementaryCellularMachine
    {
        private int _rule;
        public int Rule { 
            get
            {
                return _rule;
            }
            set
            {
                if (value < 0 || value > 256) _rule = 0;
                else _rule = value;
            }
        }

        private string _ruleBinary;
        public bool[] State { get; private set; }

        public int Size { get; set; }

        private const int _defaultSize = 256;

        public ElementaryCellularMachine(int rule, bool randomConfig, int size = _defaultSize)
        {
            Rule = rule;
            _ruleBinary = Convert.ToString(rule, 2);

            while (_ruleBinary.Length != 8)
            {
                _ruleBinary = _ruleBinary.Insert(0, "0");
            }

            if (size < 0) size = _defaultSize;
            Size = size;

            State = new bool[size];

            if (randomConfig)
            {
                Random random = new Random();
                for (int index = 0; index < Size; index++) State[index] = random.Next(2) == 0 ? false : true;
            }
            else
            {
                State[Size / 2] = true;
            }
        }

        public ElementaryCellularMachine()
        {
            Rule = (new Random()).Next(0, 256);
            Size = _defaultSize;
        }

        public void GenerateNextState()
        {
            bool[] prevState = new bool[Size];
            for (int index = 0; index < Size; index++)
            {
                prevState[index] = State[index];
            }

            string cellRule = String.Empty;
            for(int index = 0; index < Size; index++)
            {
                cellRule = "";
                //previous cell's value
                if (index == 0) cellRule += prevState[Size - 1] ? "1" : "0";
                else cellRule += prevState[index - 1] ? "1" : "0";
                cellRule += prevState[index] ? "1" : "0";

                //next cell's value
                if (index == Size - 1) cellRule += prevState[0] ? "1" : "0";
                else cellRule += prevState[index + 1] ? "1" : "0";

                switch (cellRule)
                {
                    case "000": State[index] = _ruleBinary[7] == '0' ? false : true; break;
                    case "001": State[index] = _ruleBinary[6] == '0' ? false : true; break;
                    case "010": State[index] = _ruleBinary[5] == '0' ? false : true; break;
                    case "011": State[index] = _ruleBinary[4] == '0' ? false : true; break;
                    case "100": State[index] = _ruleBinary[3] == '0' ? false : true; break;
                    case "101": State[index] = _ruleBinary[2] == '0' ? false : true; break;
                    case "110": State[index] = _ruleBinary[1] == '0' ? false : true; break;
                    case "111": State[index] = _ruleBinary[0] == '0' ? false : true; break;
                }
            }
        }

        public Bitmap GenerateImage(int imageHeight)
        {
            int width = Size, height = imageHeight < 0 ? 256 : imageHeight;

            Bitmap bitmap = new Bitmap(width, height);

            for(int y = 0; y < width; y++)
            {
                for(int x = 0; x < height; x++)
                {
                    if (!State[x]) bitmap.SetPixel(x, y, Color.White);
                    else bitmap.SetPixel(x, y, Color.Black);
                }
                GenerateNextState();
            }

            return bitmap;
        }
    }
}