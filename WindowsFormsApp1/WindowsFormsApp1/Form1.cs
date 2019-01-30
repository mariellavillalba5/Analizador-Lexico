using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int x;
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            //verificamos si la tabla de simbolos tiene mas de un dato
            if (dgvTokens.RowCount >= 1)
            {
                dgvTokens.RowCount = 1;
            }
            AnalizadorLexico();  //llamamos a un procedimiento analizar
        }
        private void AnalizadorLexico()
        {   //declaramos un arreglo de caracteres donde vamos almacenar nuestro texto fuente para poder recorrer con mas facilidad
            Char[] cadena = richTextBox1.Text.ToCharArray();
            int i=0;  //declarar una variable que nos recorra toda la cadena
            string conca = ""; //declaramos una variable para poder concatenar
            while (i < cadena.Length)  //mientras i menor longitud de la cadena
            {
                //verificar que no sea salto de linea o espacio y un tabulador
                if (cadena[i] == ' ' || cadena[i] == '\n' || cadena[i] == '\t')
                {
                    i = i + 1;   //avanzamos al siguiente caracter
                }

                else if (char.IsLetter(cadena[i]))    //si es letra
                {
                    x = i; // almacenamos la posicion donde empieza
                    conca = conca + cadena[i];  //concatenamos ese caracter
                    i = i + 1;    //avanzamos al sgte caracter
                    if (i < cadena.Length - 1)  //verificamos que no sea el ultimo caracter
                    {
                        //mientras sea numero o letra o guion bajo (_)
                        while (char.IsLetterOrDigit(cadena[i]) || cadena[i] == '_')
                        {
                            conca = conca + cadena[i]; //concatenar y recorrer el sgte. caracter
                            i = i + 1;
                            if (i == cadena.Length)  //verificar si llegamos al ultimo caracter
                            {
                                break; // si es asi salir del while
                            }
                        }
                        //determinamos si es una palabra reservada o un identificador llamaremos a nuestro metodo PR
                    }
                    PR(conca);  // mandamos como parametro la palabra concatenada
                    conca = ""; //iniciamos lo que concatenamos
                }
                //**************
                else if (char.IsDigit(cadena[i]))    //si es numero
                {
                    x = i; // almacenamos la posicion donde empieza
                    conca = conca + cadena[i];  //concatenamos ese caracter
                    i = i + 1;    //avanzamos al sgte caracter
                    if (i < cadena.Length - 1)  //verificamos que no sea el ultimo caracter
                    {
                        //mientras sea numero o punto (.)
                        while (char.IsDigit(cadena[i]) || cadena[i] == '.')
                        {
                            conca = conca + cadena[i]; //concatenar y recorrer el sgte. caracter
                            i = i + 1;
                            if (i == cadena.Length)  //verificar si llegamos al ultimo caracter
                            {
                                break; // si es asi salir del while
                            }
                        }
                    }
                    PR(conca);  // mandamos como parametro la palabra concatenada
                    conca = ""; //iniciamos lo que concatenamos
                }
                //**************   si son caracteres especiales
                else if (cadena[i] == '(' || 
                         cadena[i] == ')' || 
                         cadena[i] == '[' || 
                         cadena[i] == ']' || 
                         cadena[i] == '{' || 
                         cadena[i] == '}' || 
                         cadena[i] == '+' || 
                         cadena[i] == '-' || 
                         cadena[i] == '*' || 
                         cadena[i] == '/' || 
                         cadena[i] == '=' || 
                         cadena[i] == '%' || 
                         cadena[i] == '&' || 
                         cadena[i] == '|' || 
                         cadena[i] == ',' || 
                         cadena[i] == ';' || 
                         cadena[i] == '<' || 
                         cadena[i] == '>')
                {
                    x = i;
                    conca = conca + cadena[i]; //concatenar y recorrer el sgte. caracter
                    i = i + 1;
                    PR(conca);  // mandamos como parametro la palabra concatenada
                    conca = ""; //iniciamos lo que concatenamos
                }
            }
        }
        private void PR(string conca)
        {
            switch (conca)
            {  //usaremos caso para saber si es una palabra reservada
                case "procedimiento": insertar("1", conca, "PAL_RES-PR", x); break;
                case "funcion": insertar("2", conca, "PAL_RES-PR", x); break;
                case "principal": insertar("3", conca, "PAL_RES-PR", x); break;
                case "entero": insertar("4", conca, "PAL_RES-PR", x); break;
                case "decimal": insertar("5", conca, "PAL_RES-PR", x); break;
                case "doble": insertar("6", conca, "PAL_RES-PR", x); break;
                case "caracter": insertar("7", conca, "PAL_RES-PR", x); break;
                case "cadena": insertar("8", conca, "PAL_RES-PR", x); break;
                case "booleano": insertar("9", conca, "PAL_RES-PR", x); break;
                case "si": insertar("10", conca, "PAL_RES-PR", x); break;
                case "entonces": insertar("11", conca, "PAL_RES-PR", x); break;
                case "sino": insertar("12", conca, "PAL_RES-PR", x); break;
                case "para": insertar("13", conca, "PAL_RES-PR", x); break;
                case "mientras": insertar("14", conca, "PAL_RES-PR", x); break;
                case "hacer": insertar("15", conca, "PAL_RES-PR", x); break;

                case "(": insertar("16", conca, "PAR_A", x); break;
                case ")": insertar("17", conca, "PAR_C", x); break;
                case "[": insertar("18", conca, "COR_A", x); break;
                case "]": insertar("19", conca, "COR_C", x); break;
                case "{": insertar("20", conca, "LLAVE_A", x); break;
                case "}": insertar("21", conca, "LLAVE_C", x); break;

                case "+": insertar("22", conca, "OPER_S", x); break;
                case "-": insertar("23", conca, "OPER_R", x); break;
                case "*": insertar("24", conca, "OPER_M", x); break;
                case "/": insertar("25", conca, "OPER_D", x); break;

                case "=": insertar("26", conca, "OPER_AS", x); break;
                case "%": insertar("27", conca, "OPER_RES", x); break;

                case "&": insertar("28", conca, "OPER_AND", x); break;
                case "|": insertar("29", conca, "OPER_OR", x); break;

                case ",": insertar("30", conca, "DEL_SEP", x); break;
                case ";": insertar("31", conca, "DEL_PYC", x); break;

                case ">": insertar("32", conca, "OPER-COM_MAY", x); break;
                case "<": insertar("33", conca, "OPER-COM_MEN", x); break;

                default: insertar("59", conca, "variable o identificador-ID", x); break;
            }
        }
        private void insertar(string codigo, string simbolo, string token, int posicion)
        {   //agregamos una fila a nuestra tabla de simbolos
            DataGridViewRow row = (DataGridViewRow) dgvTokens.Rows[0].Clone();
            row.Cells[0].Value = codigo;
            row.Cells[1].Value = simbolo;
            row.Cells[2].Value = token;
            row.Cells[3].Value = posicion;

            dgvTokens.Rows.Add(row);
        }
        private void dgvTokens_CellContentClick(object sender, DataGridViewCellEventArgs e){}
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
