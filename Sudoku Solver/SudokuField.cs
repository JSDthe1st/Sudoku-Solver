using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuField
    {
        public byte? Value;

        public List<byte> PossibleNumbers = new List<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public bool IsSet 
        { 
            get => Value is not null;
        }


        public SudokuField(byte? value = null)
        {
            this.Value = value;
        }

        public SudokuField(string text)
        {
            if (text == "-")
                Value = null;
            else
                Value = 0;
        }

        public void RemovePossibility(byte number)
        {
            PossibleNumbers.Remove(number);
        }

        public override string ToString()
        {
            if (Value is byte number)
                return number.ToString();

            return "-";
        }
    }
}
