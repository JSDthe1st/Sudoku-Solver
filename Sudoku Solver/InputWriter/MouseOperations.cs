
using SharpHook;
using SharpHook.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver.InputWriter
{
    //https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c
    public class MouseOperations
    {
        EventSimulator simulator;
        public MouseOperations()
        {
            simulator = new EventSimulator();
        }

        public Point GetMousePosition()
        {
            return Cursor.Position;
        }

        public void MoveTo(short x, short y)
        {
            simulator.SimulateMouseMovement(x, y);
        }

        public void MoveUp(short pixels)
        {
            simulator.SimulateMouseMovementRelative(0, (short)(-pixels));
        }

        public void MoveDown(short pixels)
        {
            simulator.SimulateMouseMovementRelative(0, pixels);
        }

        public void MoveLeft(short pixels)
        {
            simulator.SimulateMouseMovementRelative((short)(-pixels), 0);
        }

        public void MoveRight(short pixels)
        {
            simulator.SimulateMouseMovementRelative(pixels, 0);
        }

        public void ClickLeft()
        {
            simulator.SimulateMousePress(MouseButton.Button1);
            simulator.SimulateMouseRelease(MouseButton.Button1);
        }

        public void ClickRight()
        {
            simulator.SimulateMousePress(MouseButton.Button2);
            simulator.SimulateMouseRelease(MouseButton.Button2);
        }
    }
}
