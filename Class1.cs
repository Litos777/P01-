using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace P01
{
    class PanelControl
    {
        public Ventilacion ventilacion = new Ventilacion();
        public Calefaccion calefaccion = new Calefaccion();
        public Iluminacion iluminacion = new Iluminacion();

        private string[] personas = new string[100];
        private int n;
        public PanelControl()
        {
            n = 0;
        }
        public void AddPersona(string persona)
        {
            personas[n] = persona;
            n++;
        }
        public string MostrarPersonas()
        {
            string mostrar = "";
            for (int i = 0; i < n; i++)
            {
                mostrar += personas[i] + " - ";
                if (i % 10 == 0 && i != 0)
                {
                    mostrar += "\n";
                }
            }
            return mostrar;
        }
    }
    class Ventilacion
    {
        public double nivelHumadad;
        public DateTime horaInicio;
        public DateTime horaFin;

        public bool encendido;
        public Ventilacion()
        {
            nivelHumadad = 70;
            horaInicio = DateTime.Parse("00:00");
            horaFin = DateTime.Parse("23:59");
            encendido = true;
        }
    }
    class Calefaccion
    {
        private int n;
        private double[] temperaturas = new double[100];

        public bool encendido;
        public double temperatura;
        public Calefaccion()
        {
            n = 0;
            encendido = true;
            temperaturas[0] = 22;
        }
        public void AgregarTemp()
        {
            temperaturas[n] = temperatura;
            n++;
        }
        public double CalcularTempProm()
        {
            int cant = n;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += temperaturas[i];
            }
            return Math.Round(sum / cant, 2);

        }
        public double CalcularTempMax()
        {
            double tMax = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    tMax = temperaturas[i];
                }
                else
                {
                    if (tMax < temperaturas[i])
                        tMax = temperaturas[i];
                }
            }
            return tMax;
        }
        public double CalcularTempMin()
        {
            double tMin = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    tMin = temperaturas[i];
                }
                else
                {
                    if (tMin > temperaturas[i])
                        tMin = temperaturas[i];
                }
            }
            return tMin;
        }
        public double CalcualrPromTempMax()
        {
            double tprom = CalcularTempProm();
            int cMax = 0;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (temperaturas[i] >= tprom)
                {
                    sum += temperaturas[i];
                    cMax++;
                }
            }
            return Math.Round(sum / cMax, 2);
        }
        public double CalcularPromTempMin()
        {
            double tprom = CalcularTempProm();
            int cMin = 0;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (temperaturas[i] <= tprom)
                {
                    sum += temperaturas[i];
                    cMin++;
                }
            }
            return Math.Round(sum / cMin, 2);
        }
    }
    class Iluminacion
    {
        public string[] habitaciones = { "Sala", "Cocina", "Habitacion principal", "Habitacion de huespedes" };
        public bool[] encendido;
        public Iluminacion()
        {
            encendido = new bool[4];
        }
        public string ReturnHabitacion()
        {
            string imprimir = "", encendidoT = "";
            for (int i = 0; i < 4; i++)
            {
                if (encendido[i])
                    encendidoT = "encendida";
                else
                    encendidoT = "apagada";
                imprimir += "En la " + habitaciones[i] + " la luz se encuentra " + encendido[i];
            }

            return imprimir;
        }
    }
}

