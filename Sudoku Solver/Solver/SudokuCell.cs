using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Sudoku
{
    public class SudokuCell
    {
        char value;
        public char Value
        {
            get { return value; }
            set
            {
                if ((value >= '0' && value <= '9') || value == '-')
                {
                    this.value = value;
                    RemovePossibleNumber(value);
                }

                throw new ArgumentException("Invalid value for SudokuCell, must be digit or dash.");
            }
        }

        List<char> possibleNumbers = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public List<char> PossibleNumbers => possibleNumbers;

        public bool IsFilledIn 
        { 
            get => Value != '-';
        }

        public SudokuCell(char value)
        {
            Value = value;
        }

        public SudokuCell(string value)
            : this(value[0])
        { }

        public void RemovePossibleNumber(char number)
        {
            possibleNumbers.Remove(number);
        }

        public void AddPossibility(char number)
        {
            possibleNumbers.Add(number);
            //PossibleNumbers.Sort();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
