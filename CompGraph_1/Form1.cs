using CompGraph_1.Figures;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CompGraph_1
{
    public partial class Form1 : Form
    {
        Figure figure, coord;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            figure = new Figure("D:\\Project\\VisualStudioProject\\CompGraph_1\\CompGraph_1\\Word.txt");
            figure.Draw(panel1.CreateGraphics());
            coord = new Figure("D:\\Project\\VisualStudioProject\\CompGraph_1\\CompGraph_1\\Coord.txt");
            coord.Draw(panel1.CreateGraphics());
        }    
        /// <summary>
        /// Шо-то там рисует
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {  
            figure.Draw(e.Graphics);
            coord.Draw(e.Graphics);
        }
        /// <summary>
        /// Кнопочки, тыкать на кнопочки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                switch (e.KeyCode.ToString())
                {
                    case "W":
                        figure.Moving(0, 10, 0);
                        break;
                    case "S":
                        figure.Moving(0, -10, 0);
                        break;
                    case "D":
                        figure.Moving(-10, 0, 0);
                        break;
                    case "A":
                        figure.Moving(10, 0, 0);
                        break;
                    case "Q":
                        figure.Moving(0, 0, 10);
                        break;
                    case "E":
                        figure.Moving(0, 0, -10);
                        break;
                    default:
                        break;
                }           
            else
                switch (e.KeyCode.ToString())
                {
                    case "W": figure.RotationX(5);
                        break;
                    case "S":
                        figure.RotationX(-5);
                        break;
                    case "A":
                        figure.RotationY(5);
                        break;
                    case "D":
                        figure.RotationY(-5);
                        break;
                    case "Q":
                        figure.RotationZ(-5);
                        break;
                    case "E":
                        figure.RotationZ(5);
                        break;
                    default:
                        break;
                }
            //
            // Вернуть все на путь истинный
            //
            if (e.KeyCode == Keys.F5)
            {
                figure = new Figure("D:\\Project\\VisualStudioProject\\CompGraph_1\\CompGraph_1\\Word.txt");
            }           
            // 1.Перемещение (анимированное)  вдоль произвольной прямой на заданное расстояние с замедлением перед остановкой.
            else if (e.KeyValue == 32) // аля пробел
            {
                Random random = new Random();              
                SHIT(random.Next(0, 3));
                return;
            }              
            panel1.Invalidate();           
        }

        private void SHIT(int random)
        {
            for (int i = 16; i > 0; i--)
            {
                switch (random)
                {
                    case 0: figure.Moving(-i * i / 4, 0, 0);
                        break;
                    case 1:
                        figure.Moving(0, i * i / 4, 0);
                        break;
                    case 2:
                        figure.Moving(0, 0, i * i / 4);
                        break;

                    default:
                        break;
                }
                
                panel1.Refresh(); // Invalidate х**ня и работает криво
                Thread.Sleep(30);
            }
            
        }
        
        /// <summary>
        /// НАДО для плавного отображения
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
