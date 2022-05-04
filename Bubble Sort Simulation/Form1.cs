using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubble_Sort_Simulation
{
    public partial class Form1 : Form
    {
        TElement[] Element = new TElement[15];
        
        public Form1()
        {
            InitializeComponent();
        }

        public struct TElement
        {
            public int x;
            public int y;
            public int inf;
            public Color color;
        }

        void SetRandom()
        {
            int L = Element.Length;
            Random rnd = new Random();
            for (int i = 0; i < L - 1; i++)
            {
                Element[i].x = 100;
                Element[i].y = 20 + i * 40;
                Element[i].color = Color.Black;
                Element[i].inf = rnd.Next(100);
            }
        }

        void BubbleSort()
        {
            int L = Element.Length;
            for (int i = 0; i < L - 1; i++)
            {
                for (int j = 0; j < L - i - 1; j++)
                {
                    TElement tmp;
                    if (Element[j].inf > Element[j + 1].inf)
                    {
                        Change(j, j + 1, i, j);
                        tmp = Element[j];
                        Element[j] = Element[j + 1];
                        Element[j + 1] = tmp;
                    }
                }
            }
        }

        void Change(int n1, int n2, int n, int m)
        {
            Element[n1].color = Color.Red;
            Element[n2].color = Color.Red;
            int x1 = Element[n1].x;
            int y1 = Element[n1].y;
            int x2 = Element[n2].x;
            int y2 = Element[n2].y;
            double x;

            for (int t = 1; t <= 15; t++)
            {
                x = (y2 - y1) * t / 15;
                Element[n1].y = y1 + (int)(x);
                switch (t)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        x = 40 * t / 3;
                        Element[n2].x = x1 - (int)(x);
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        x = (y1 - y2) * (t - 4) / 7;
                        Element[n2].y = y2 + (int)(x);
                        break;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        x = 40 * (t - 11) / 4;
                        Element[n2].x = x1 - 40 + (int)(x);
                        break;
                }
                Drawing(n, m);
                //Thread.Sleep(100); 
            }
            Element[n1].color = Color.Black;
            Element[n2].color = Color.Black;
            Drawing(n, m);
        }
        void Drawing(int n, int m)
        {
            const int d = 15;
            int L = Element.Length;
            string s;
            SizeF size;
            Bitmap bitmap;
            bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics gBitmap, gScreen;
            gBitmap = this.CreateGraphics();
            gScreen = this.CreateGraphics();
            gBitmap.Clear(Color.White);
            Pen MyPen = new Pen(Color.Black, 1);
            for (int i = 0; i <= L - 1; i++)
            {
                MyPen.Color = Element[i].color;
                gBitmap.DrawEllipse(MyPen, Element[i].x - d, Element[i].y - d, 2 * d, 2 * d);
                s = Convert.ToString(Element[i].inf);
                size = gBitmap.MeasureString(s, Font);
                gBitmap.DrawString(s, Font, Brushes.Black, Element[i].x - size.Width / 2, Element[i].y - size.Height / 2);
            }
            if (n != -1)
            {
                MyPen.Color = Color.Black;
                gBitmap.DrawLine(MyPen, 120, Element[n].y, 140, Element[n].y);
                s = "I = " + Convert.ToString(n);
                size = gBitmap.MeasureString(s, Font);
                gBitmap.DrawString(s, Font, Brushes.Black, 150, Element[n].y - size.Height / 2);
            }
            if (m != -1)
            {
                MyPen.Color = Color.Red;
                gBitmap.DrawLine(MyPen, 120, Element[m].y, 140, Element[m].y);
                s = "J = " + Convert.ToString(m);
                size = gBitmap.MeasureString(s, Font);
                gBitmap.DrawString(s, Font, Brushes.Black, 150, Element[m].y - size.Height / 2);
            }
            gScreen.DrawImage(bitmap, ClientRectangle);
        }

        private void Random_Click(object sender, EventArgs e)
        {
            SetRandom();
            Drawing(-1, -1);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
