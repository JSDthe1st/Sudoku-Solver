using SharpHook;
using SharpHook.Data;

namespace Sudoku_Solver.InputWriter
{
    public class KeyboardOperations
    {
        EventSimulator simulator;

        static public Dictionary<int, KeyCode> KeyCodes = new Dictionary<int, KeyCode>
        {
            { 0, KeyCode.Vc0},
            { 1, KeyCode.Vc1},
            { 2, KeyCode.Vc2},
            { 3, KeyCode.Vc3},
            { 4, KeyCode.Vc4},
            { 5, KeyCode.Vc5},
            { 6, KeyCode.Vc6},
            { 7, KeyCode.Vc7},
            { 8, KeyCode.Vc8},
            { 9, KeyCode.Vc9}
        };

        public KeyboardOperations()
        {
            simulator = new EventSimulator();
        }

        public void PressKey(KeyCode code)
        {
            simulator.SimulateKeyPress(code);
            simulator.SimulateKeyRelease(code);
        }

    }
}
