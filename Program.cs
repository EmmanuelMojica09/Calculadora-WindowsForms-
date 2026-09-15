using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculadoraGrafica
{
    //Creamos el Contenido 
    public class VentanaCalculadora : Form
    {
        private TextBox Pantalla;
        private double num1 = 0;
        private double num2 = 0;
        private string operacion = "";

        public VentanaCalculadora()
        {
            Text = "Calculadora";
            Size = new Size(260, 360);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // PANTALLA — SIN LÍMITE DE CARACTERES 
            Pantalla = new TextBox();
            Pantalla.Size = new Size(210, 50);
            Pantalla.Location = new Point(10, 10);
            Pantalla.Font = new Font("Arial", 20, FontStyle.Bold);
            Pantalla.TextAlign = HorizontalAlignment.Right;
            Pantalla.Text = "0";
            Pantalla.ReadOnly = true;
            Pantalla.MaxLength = 0;    
            Controls.Add(Pantalla);

            // BOTONES DE OPERACIONES
            CrearBoton("+", 10, 70, Color.LightBlue);
            CrearBoton("-", 65, 70, Color.LightBlue);
            CrearBoton("*", 120, 70, Color.LightBlue);
            CrearBoton("/", 175, 70, Color.LightBlue);

            // FILA 1 - NÚMEROS
            CrearBoton("7", 10, 120);
            CrearBoton("8", 65, 120);
            CrearBoton("9", 120, 120);

            // FILA 2 - NÚMEROS
            CrearBoton("4", 10, 170);
            CrearBoton("5", 65, 170);
            CrearBoton("6", 120, 170);

            // FILA 3 - NÚMEROS
            CrearBoton("1", 10, 220);
            CrearBoton("2", 65, 220);
            CrearBoton("3", 120, 220);

            // FILA 4 - 0, IGUAL Y BORRAR
            CrearBoton("0", 10, 270, 120, 45);
            CrearBoton("=", 130, 270, 60, 45, Color.LightGreen);
            CrearBoton("CE", 175, 120, 60, 95, Color.LightPink);
        }

        // FUNCIÓN PARA CREAR BOTONES
        void CrearBoton(string texto, int x, int y, Color color)
        {
            CrearBoton(texto, x, y, 50, 45, color);
        }

        void CrearBoton(string texto, int x, int y)
        {
            CrearBoton(texto, x, y, 50, 45, Color.WhiteSmoke);
        }

        void CrearBoton(string texto, int x, int y, int ancho, int alto, Color? color = null)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Size = new Size(ancho, alto);
            btn.Location = new Point(x, y);
            btn.Font = new Font("Arial", 14, FontStyle.Bold);
            btn.BackColor = color ?? Color.WhiteSmoke;
            btn.Click += BotonPresionado;
            Controls.Add(btn);
        }

        // LO QUE PASA AL PRESIONAR CADA BOTÓN
        void BotonPresionado(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            // NÚMEROS DEL 0 AL 9
            if (b.Text == "0" || b.Text == "1" || b.Text == "2" || b.Text == "3" ||
                b.Text == "4" || b.Text == "5" || b.Text == "6" || b.Text == "7" ||
                b.Text == "8" || b.Text == "9")
            {
                if (Pantalla.Text == "0")
                    Pantalla.Text = b.Text;
                else
                    Pantalla.Text += b.Text;
            }

            // OPERACIONES + - * /
            else if (b.Text == "+" || b.Text == "-" || b.Text == "*" || b.Text == "/")
            {
                num1 = Convert.ToDouble(Pantalla.Text);
                operacion = b.Text;
                Pantalla.Text = "0";
            }

            // BOTÓN IGUAL = → CALCULAR RESULTADO
            else if (b.Text == "=")
            {
                num2 = Convert.ToDouble(Pantalla.Text);
                double res = 0;

                switch (operacion)
                {
                    case "+": res = num1 + num2; break;
                    case "-": res = num1 - num2; break;
                    case "*": res = num1 * num2; break;
                    case "/":
                        if (num2 == 0) { Pantalla.Text = "Error"; return; }
                        res = num1 / num2;
                        break;
                }

                Pantalla.Text = res.ToString();
                num1 = res;
            }

            // BOTÓN CE → BORRAR TODO
            else if (b.Text == "CE")
            {
                Pantalla.Text = "0";
                num1 = 0;
                num2 = 0;
                operacion = "";
            }
        }

        // PUNTO DE INICIO DEL PROGRAMA
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new VentanaCalculadora());
        }
    }
}