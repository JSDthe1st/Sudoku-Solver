using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuField
    {
        public char Value;

        public List<char> PossibleNumbers = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public bool IsSet 
        { 
            get => Value == '-';
        }


        public SudokuField(char value = '-')
        {
            this.Value = value;
        }

        public SudokuField(string value)
            : this(value[0])
        { }

        public void RemovePossibility(char number)
        {
            PossibleNumbers.Remove(number);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
