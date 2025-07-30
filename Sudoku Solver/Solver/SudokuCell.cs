
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
                this.value = '-';
                if (value >= '0' && value <= '9')
                {
                    this.value = value;
                    possibleNumbers.Clear();
                }
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
        {
            if (value.Length == 0 || value == null)
                Value = '-';
            else
                Value = value[0];
        }

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
