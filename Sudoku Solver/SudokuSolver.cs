using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuSolver
    {
        SudokuField[,] numbers = new SudokuField[9,9];

        public SudokuSolver()
        {

        }
        
        public SudokuSolver(SudokuField[,] numbers)
            : this()
        {
            this.numbers = numbers;
        }

        public void LoadNumbers(SudokuField[,] numbers)
        {
            this.numbers = numbers;
        }

        public void LoadNumbers(string path)
        {
            string raw = File.ReadAllText(path);
            raw = raw.Trim();
            string[] rows = raw.Split('\n');

            for (int i = 0; i < 9; i++)
            {
                string[] rowElements = rows[i].Split(' ');

                for (int j = 0; j < 9; j++)
                {
                    numbers[i, j] = new SudokuField(rowElements[j]);
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(numbers[i, j] + " ");

                    if (j == 2 || j == 5)
                        Console.Write("| ");
                }
                Console.WriteLine();

                if (i == 2 || i == 5)
                    Console.WriteLine("------+-------+------");
            }
        }

        public void Solve()
        {

        }
    }
}