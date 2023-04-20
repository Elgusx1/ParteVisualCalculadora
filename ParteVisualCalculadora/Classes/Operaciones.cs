using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteVisualCalculadora.Classes
{
    public class Operaciones
    {
        public string expresion;
        public List<object> elementos { get; set; }
        public Operaciones()
        {
            elementos = new List<object>();
        }

        public void AgregarYCalculos(string exp)
        {
            try
            {
                if (exp == null)
                {
                    throw new ArgumentNullException(nameof(exp));
                }
                ConvertirAElementos(exp);
                double resultado = RealizarCalculos(elementos);
            }
            catch (FormatException ex)
            {
                throw new Exception(ex.Message);

            }
        }
        private void ConvertirAElementos(string exp)
        {
            try
            {
                char[] caracteres = exp.ToCharArray();
                for (int i = 0; i < caracteres.Length; i++)
                {
                    if (char.IsDigit(caracteres[i]))
                    {
                        LeerNumero(caracteres, ref i);
                    }
                    else if (EsOperador(caracteres[i]))
                    {
                        elementos.Add(caracteres[i]);
                    }
                    else if (caracteres[i] == '(')
                    {
                        if (i > 0 && char.IsDigit(caracteres[i - 1]))
                        {
                            elementos.Add('*');
                        }
                        elementos.Add(caracteres[i]);
                    }
                    else if (caracteres[i] == ')' && i < caracteres.Length - 1 && caracteres[i + 1] == '(')
                    {
                        elementos.Add(')');
                        elementos.Add('*');
                    }

                    else if (caracteres[i] == ')')
                    {
                        elementos.Add(caracteres[i]);
                    }
                    else
                    {
                        throw new FormatException("La expresión contiene caracteres no válidos.");
                    }

                }
            }
            catch (FormatException ex)
            {

                throw ex;
            }

        }

        private void LeerNumero(char[] caracteres, ref int i)
        {
            string numeroString = caracteres[i].ToString();
            int j = i + 1;
            while (j < caracteres.Length && (char.IsDigit(caracteres[j]) || caracteres[j] == '.'))
            {
                numeroString += caracteres[j];
                j++;
            }
            double numero = double.Parse(numeroString);
            i = j - 1;
            elementos.Add(numero);
        }

        private bool EsOperador(char caracter)
        {
            return caracter == '+' || caracter == '-' || caracter == '*' || caracter == '/';
        }
        private int ObetenerJerarquia(char operador)
        {
            switch (operador)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }
        private double CalculosConParentesis(List<object> subElementos)
        {
            subElementos.RemoveAt(subElementos.Count - 1);
            subElementos.RemoveAt(0);
            double resultado = RealizarCalculos(subElementos);
            return resultado;

        }

        private double RealizarCalculos(List<object> elementos)
        {
            double resultado = 0;

            while (elementos.Contains('('))
            {
                int indiceAbre = elementos.LastIndexOf('(');
                int indiceCierra = indiceAbre + 1;
                int contador = 1;
                while (contador > 0)
                {
                    if (elementos[indiceCierra] is char)
                    {
                        if ((char)elementos[indiceCierra] == '(')
                        {
                            contador++;
                        }
                        else if ((char)elementos[indiceCierra] == ')')
                        {

                            contador--;
                        }
                    }
                    indiceCierra++;
                }
                List<object> subElementos = elementos.GetRange(indiceAbre, indiceCierra - indiceAbre);
                double resultadoParcial = CalculosConParentesis(subElementos);
                elementos.RemoveRange(indiceAbre, indiceCierra - indiceAbre);
                elementos.Insert(indiceAbre, resultadoParcial);

            }


            List<object> elementosOrdenados = new List<object>();
            Stack<char> pilaOperadores = new Stack<char>();

            foreach (object elemento in elementos)
            {
                if (elemento is double)
                {
                    elementosOrdenados.Add(elemento);
                }
                else if (elemento is char)
                {
                    char nuevoOperador = (char)elemento;
                    while (pilaOperadores.Count > 0 && ObetenerJerarquia(pilaOperadores.Peek()) >= ObetenerJerarquia(nuevoOperador))
                    {
                        elementosOrdenados.Add(pilaOperadores.Pop());
                    }
                    pilaOperadores.Push(nuevoOperador);
                }
            }
            while (pilaOperadores.Count > 0)
            {
                elementosOrdenados.Add(pilaOperadores.Pop());
            }

            Stack<double> pilaOperandos = new Stack<double>();

            foreach (object elemento in elementosOrdenados)
            {
                if (elemento is double)
                {
                    pilaOperandos.Push((double)elemento);
                }
                else if (elemento is char)
                {
                    double operando2 = pilaOperandos.Pop();
                    double operando1 = pilaOperandos.Pop();
                    switch ((char)elemento)
                    {
                        case '+':
                            pilaOperandos.Push(operando1 + operando2);
                            break;
                        case '-':
                            pilaOperandos.Push(operando1 - operando2);
                            break;
                        case '*':
                            pilaOperandos.Push(operando1 * operando2);
                            break;
                        case '/':
                            pilaOperandos.Push(operando1 / operando2);
                            break;
                        default:
                            throw new ArgumentException("Operador no válido.");
                    }
                }
            }
            resultado = pilaOperandos.Pop();


            return resultado;
        }
    }
}
