using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Sudoku
{
    public class SudokuCell
    {
        public char Value;

        public List<char> PossibleNumbers = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public bool IsFilledIn 
        { 
            get => Value != '-';
        }

        public SudokuCell(char value = '-')
        {
            if ((value >= '0' && value <= '9') || value == '-')
            {
                Value = value;
                RemovePossibleNumber(value);
            }

            throw new ArgumentException("Invalid value for SudokuCell, must be digit or dash.");
        }

        public SudokuCell(string value)
            : this(value[0])
        { }

        public void RemovePossibleNumber(char number)
        {
            PossibleNumbers.Remove(number);
        }

        public void AddPossibility(char number)
        {
            PossibleNumbers.Add(number);
            //PossibleNumbers.Sort();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
