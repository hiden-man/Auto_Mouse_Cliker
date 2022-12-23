using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace Auto_Mouse_Cliker
{
    public partial class Form1 : Form
    {

        Size ScreenSize = Screen.PrimaryScreen.Bounds.Size;

        const int myCursorPosition_XX = 1270, myCursorPosition_YY = 170;
        public Form1()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0X20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
            {
                Close();
            }
            else if (e.KeyCode == Keys.S)
            {
                ChangeCursorPosition();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(ScreenSize.Width - Size.Width, 0);
            //CDSSD();
        }

        //async void CDSSD()
        //{
        //    await Task.Run(() =>
        //    {
        //        for (; true;)
        //        {
        //            textBox1.Text = $"{Cursor.Position.X},{Cursor.Position.Y}";
        //        }
        //    });
        //}
        public void ChangeCursorPosition()
        {
            int myCursorPosition_X = 55, myCursorPosition_Y = 180;
            byte stepForBreak = 0;
            //Cursor.Position = new Point(myCursorPosition_X, myCursorPosition_Y);
            for (int i = 0; i < 12; i++)
            {
                NextStep:
                Cursor.Position = new Point(myCursorPosition_X, myCursorPosition_Y);
                Thread.Sleep(50);
                Click_Mouse();
                Thread.Sleep(50);
                Cursor.Position = new Point(myCursorPosition_XX, myCursorPosition_YY);
                Thread.Sleep(50);
                Click_Mouse();
                Thread.Sleep(50);
                myCursorPosition_Y += 60;
                if (i == 11)
                {
                    myCursorPosition_X += 58;
                    myCursorPosition_Y = 180;
                    if (stepForBreak < 9)
                    {
                        i = 0;
                        stepForBreak++;
                        goto NextStep;
                    }
                    else
                        break;
                }
            }
        }

        private void Click_Mouse()
        {
            //int c = 0;
            POINT p = new POINT();
            DoMouseLeftClick(p.x, p.y);
            //while (true)
            //{
            //    GetCursorPos(ref p);
            //    ClientToScreen(Handle, ref p);
            //    DoMouseLeftClick(p.x, p.y); // Ліва кнопка миши
            //    //DoMouseRightClick(p.x, p.y); // права кнопка миши
            //    //DoMouseDoubleLeftClick(p.x, p.y); // двойний клік лівою кнопкою миши
            //    c++;
            //    Thread.Sleep(100);
            //    if (c == 50)
            //    {
            //        break;
            //    }
            //}
        }

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);

        public const int MOUSE_EVENT_F_LEFTDOWN = 0X02;
        public const int MOUSE_EVENT_F_LEFTUP = 0X04;

        public const int MOUSE_EVENT_F_RIGHTDOWN = 0x08;
        public const int MOUSE_EVENT_F_RIGHTUP = 0x10;

        private void DoMouseLeftClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);
        }
        private void DoMouseRightClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_RIGHTUP, x, y, 0, 0);
        }
        private void DoMouseDoubleLeftClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);

            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);

    }
}
