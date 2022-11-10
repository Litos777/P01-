using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace P01
{
    class Program
    {
        static DateTime dateTime = DateTime.Parse("00:00");
        static PanelControl panel = new PanelControl();
        static void AvanzarHora(int i)
        {
            if (dateTime.Hour == 0 && dateTime.Minute == 0)
            {
                Console.WriteLine("0" + dateTime.Hour + ":" + "0" + dateTime.Minute + "\n");
                Thread.Sleep(1000);
                Console.Clear();
            }
            dateTime = dateTime.AddMinutes(30);
            if (dateTime.Hour <= 9 && dateTime.Minute == 0)
                Console.WriteLine("0" + dateTime.Hour + ":" + "0" + dateTime.Minute + "\n");
            else if (dateTime.Hour < 10 && dateTime.Minute == 30)
                Console.WriteLine("0" + dateTime.Hour + ":" + dateTime.Minute + "\n");
            else
            {
                if (dateTime.Minute == 30 && dateTime.Hour > 9)
                    Console.WriteLine(dateTime.Hour + ":" + dateTime.Minute + "\n");
                else if (dateTime.Minute == 0 && dateTime.Hour > 9)
                    Console.WriteLine(dateTime.Hour + ":" + "0" + dateTime.Minute + "\n");
            }
            CambioDeEstado();
            Thread.Sleep(1000);
            if (i != 7)
                Console.Clear();
        }
        static bool ValidarHoraIngresada(string shora)
        {
            DateTime dHora = new DateTime();
            try
            {
                dHora = DateTime.Parse(shora);
                if (dHora.Minute != 0 && dHora.Minute != 30)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return true;
            }
        }
        static bool ValidarHoraFinMayor(DateTime horaI, string horaF)
        {
            DateTime dHoraF = DateTime.Parse(horaF);
            if (horaI > dHoraF)
                return true;
            else
                return false;
        }
        static string EstadoEncendido(bool estado)
        {
            if (estado)
                return "encendido";
            else
                return "apagado";
        }
        static void CambioDeEstado()
        {
            if (panel.ventilacion.horaInicio == dateTime)
            {
                panel.ventilacion.encendido = true;
                Console.WriteLine("Se encendió el sistema de ventilacion");
            }
            if (panel.ventilacion.horaFin == dateTime)
            {
                panel.ventilacion.encendido = false;
                Console.WriteLine("Se apagó el sistema de ventilacion");
            }
        }
        static void Main(string[] args)
        {

            int opcion = 0;
            bool valVentilacion = true, estado = true;
            Console.WriteLine("Ingrese la hora en formato hh:mm");
            Console.WriteLine("El proyecto solo acepta horas completas(10:00) y mitad de hora (10:30)");
            do
            {
                // menu de opciones
                try
                {
                    Console.WriteLine(
                    "SELECCIONE UNA OPCION:\n " +
                    "********************************\n" +
                    "*       1 - Ventilacion        *\n" +
                    "*       2 - Calefaccion        *\n" +
                    "*       3 - Iluminacion        *\n" +
                    "*       4 - Salir              *\n" +
                    "********************************");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    while (opcion <= 0 || opcion > 5)
                    {
                        Console.WriteLine("ERROR! INGRESE UNA OPCION VALIDA");
                        Console.WriteLine(
                        "SELECCIONE UNA OPCION:\n" +
                        "********************************\n" +
                        "*       1 - Ventilacion        *\n" +
                        "*       2 - Calefaccion        *\n" +
                        "*       3 - Iluminacion        *\n" +
                        "*       4 - Salir              *\n" +
                        "********************************");
                        opcion = Convert.ToInt32(Console.ReadLine());

                    }
                }
                // validacion de error al ingresar opcion
                catch
                {
                    Console.WriteLine("ERROR!");
                    continue;
                }
                switch (opcion)
                {
                    //ventilacion
                    case 1:
                        {
                            bool validar = false;
                            string horaEncendido = "", horaFin = "";
                            double nivelHumedad = 0;
                            if (valVentilacion)
                            {
                                do//hora encendido
                                {
                                    Console.Write("Ingrese la hora a la que desea encender el sistema de ventilacion ");
                                    horaEncendido = Console.ReadLine();
                                    validar = ValidarHoraIngresada(horaEncendido);
                                } while (validar);
                                panel.ventilacion.horaInicio = DateTime.Parse(horaEncendido);
                                validar = false;
                                do//hora apagado
                                {
                                    Console.Write("Ingrese la hora a la que apagar el sistema de ventilacion ");
                                    horaFin = Console.ReadLine();
                                    validar = ValidarHoraIngresada(horaFin);
                                    if (validar == false)
                                        validar = ValidarHoraFinMayor(panel.ventilacion.horaInicio, horaFin);
                                } while (validar);
                                panel.ventilacion.horaFin = DateTime.Parse(horaFin);
                                validar = false;
                                do
                                {
                                    Console.Write("Ingrese el nivel de humedad que desea mantener: ");
                                    try
                                    {
                                        nivelHumedad = Convert.ToDouble(Console.ReadLine());
                                        validar = false;
                                        if (nivelHumedad > 70)
                                        {
                                            Console.WriteLine("ERROR! Nivel de humedad elevado");
                                        }
                                        else
                                        {
                                            panel.ventilacion.nivelHumadad = nivelHumedad;
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Error! Ingrese un numero");
                                        validar = true;
                                    }
                                } while (validar);
                                //se valida si llego la hora de fin de la ventilacion para luego ser modificada nuevamente
                                valVentilacion = false;
                            }
                            else
                                Console.WriteLine("Aun no puede modificarse la ventilacion del hogar");
                            break;
                        }
                    // calefaccion
                    case 2:
                        {
                            bool validar = false;
                            string encendido = "";
                            double temperatura = 0.0;
                            Console.Write("Desea encender la calefaccion? (si/no) ");
                            encendido = Console.ReadLine().ToLower();
                            while (encendido != "si" && encendido != "no")
                            {
                                Console.WriteLine("Ingrese una opcion valida");
                                Console.Write("Desea encender la calefaccion? (si/no) ");
                                encendido = Console.ReadLine().ToLower();
                            }
                            if (encendido == "si")
                            {
                                do
                                {
                                    panel.calefaccion.encendido = true;
                                    Console.WriteLine("Ingrese una temperatura ");
                                    try
                                    {
                                        temperatura = Convert.ToDouble(Console.ReadLine());
                                        if (temperatura > 22 || temperatura < 18)
                                        {
                                            Console.WriteLine("ERROR! Ingrese una temperatura entre 18 y 22°C");
                                            validar = true;
                                        }
                                        else
                                        {
                                            panel.calefaccion.temperatura = temperatura;
                                            panel.calefaccion.AgregarTemp();
                                            validar = false;
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("ERROR! Ingrese un numero");
                                        validar = true;
                                    }
                                } while (validar);
                            }
                            else
                            {
                                if (panel.calefaccion.encendido == true)
                                {
                                    Console.WriteLine("Se apago el sistema de calefaccion");
                                    panel.calefaccion.encendido = false;
                                }
                            }
                            break;
                        }
                    // iluminacion
                    case 3:
                        {
                            string opcionH = "";
                            do
                            {
                                Console.WriteLine("SELECCIONE UNA OPCION\n" +
                                    "****************************************\n" +
                                    "*               1 - Sala               *\n" +
                                    "*               2 - Cocina             *\n" +
                                    "*         3 - Habitacion principa      *\n" +
                                    "*       4 - Habitacion de huespedes    *\n" +
                                    "****************************************\n");
                                opcionH = Console.ReadLine();
                            } while (opcionH != "1" && opcionH != "2" && opcionH != "3" && opcionH != "4");

                            string encendido = "";
                            Console.Write("Desea encender las luces? (si/no) ");
                            encendido = Console.ReadLine().ToLower();
                            while (encendido != "si" && encendido != "no")
                            {
                                Console.WriteLine("Ingrese una opcion valida");
                                Console.Write("Desea encender las luces? (si/no) ");
                                encendido = Console.ReadLine().ToLower();
                            }
                            if (encendido == "si")
                            {
                                if (opcionH == "1")
                                {
                                    panel.iluminacion.encendido[0] = true;
                                    Console.WriteLine("Las luces de la {0} se encendieron", panel.iluminacion.habitaciones[0]);
                                }
                                else if (opcionH == "2")
                                {
                                    panel.iluminacion.encendido[1] = true;
                                    Console.WriteLine("Las luces de la {0} se encendieron", panel.iluminacion.habitaciones[1]);
                                }
                                else if (opcionH == "3")
                                {
                                    panel.iluminacion.encendido[2] = true;
                                    Console.WriteLine("Las luces de la {0} se encendieron", panel.iluminacion.habitaciones[2]);
                                }
                                else
                                {
                                    panel.iluminacion.encendido[3] = true;
                                    Console.WriteLine("Las luces de la {0} se encendieron", panel.iluminacion.habitaciones[3]);
                                }
                            }
                            else
                            {
                                if (opcionH == "1")
                                {
                                    panel.iluminacion.encendido[0] = false;
                                    Console.WriteLine("Las luces de la {0} se apagaron", panel.iluminacion.habitaciones[0]);
                                }
                                else if (opcionH == "2")
                                {
                                    panel.iluminacion.encendido[1] = false;
                                    Console.WriteLine("Las luces de la {0} se apagaron", panel.iluminacion.habitaciones[1]);
                                }
                                else if (opcionH == "3")
                                {
                                    panel.iluminacion.encendido[2] = false;
                                    Console.WriteLine("Las luces de la {0} se apagaron", panel.iluminacion.habitaciones[2]);
                                }
                                else
                                {
                                    panel.iluminacion.encendido[3] = false;
                                    Console.WriteLine("Las luces de la {0} se apagaron", panel.iluminacion.habitaciones[3]);
                                }
                            }
                            break;
                        }
                }
                // agregar persona que usa el panel de contorl
                Console.Write("Ingrese su  nombre: ");
                panel.AddPersona(Console.ReadLine());
                /*simular que las horas avanzan en el transcurso del dia y verificacion de las horas 
                 de encendido y apagado de los dispositivos de la casa inteligente*/
                if (opcion != 4)
                {
                    for (int i = 0; i < 8; i++) AvanzarHora(i);
                }
            } while (opcion != 4);

            Console.WriteLine("RESUMEN DEL DIA: ");
            Console.WriteLine("Hora finalizada: " + dateTime);
            Console.WriteLine("VENTILACION:\n");
            estado = panel.ventilacion.encendido;
            Console.WriteLine("Estado {0} Nivel de humedad {1}% Ultima hora de encendido {2} Hora de apagado {3}",
                               EstadoEncendido(estado), panel.ventilacion.nivelHumadad, panel.ventilacion.horaInicio, panel.ventilacion.horaFin);
            Console.WriteLine("\nCALEFACCION:\n");
            estado = panel.calefaccion.encendido;
            Console.WriteLine("Estado {0} Ultima temperatura {1} Temperatura Maxima {2} Temperatura Minima {3}\n" +
                "Promedio de temperatura {4}Promedio temperatura maximas {5} Promedio temperaturas minimas {6}"
                , EstadoEncendido(estado), panel.calefaccion.temperatura, panel.calefaccion.CalcularTempMax(), panel.calefaccion.CalcularTempMin()
                , panel.calefaccion.CalcularTempProm(), panel.calefaccion.CalcualrPromTempMax(), panel.calefaccion.CalcularPromTempMin());
            Console.WriteLine("\nILUMINACION:\n");
            for (int i = 0; i < 4; i++)
            {
                estado = panel.iluminacion.encendido[i];
                Console.WriteLine("Habitacion " + panel.iluminacion.habitaciones[i] + " Estado " + EstadoEncendido(estado));
            }
            Console.WriteLine("Las personas que utilizaron el panel de control son: \n" + panel.MostrarPersonas());
            Console.ReadKey();
        }
    }
}
