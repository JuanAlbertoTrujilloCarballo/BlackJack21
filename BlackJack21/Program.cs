using System.Collections.Generic;
using static Program;


class Program
{
    public enum palosCartas
    {
        diamantes,
        corazones,
        picas,
        treboles
    }

    public class Carta
    {

        public palosCartas palosCartas { get; }
        public int Valor { get; }


        public Carta(palosCartas paloCarta, int valor)
        {
            palosCartas = paloCarta;
            Valor = valor;
        }

        public int conseguirValor()
        {
            return Valor;
        }

        public void mostrarCarta()
        {
            Console.WriteLine($"Carta: {Valor} de {palosCartas}.");
        }

    }

    public class Baraja
    {
        private List<Carta> cartas;
        private Random random;

        public Baraja()
        {
            cartas = new List<Carta>();
            random = new Random();

            foreach (palosCartas paloCarta in Enum.GetValues(typeof(palosCartas)))
            {
                for (int value = 1; value <= 13; value++)
                {
                    cartas.Add(new Carta(paloCarta, value));

                }
            }
        }

        public void mostrarCartas()
        {
            foreach (Carta cartas in cartas)
            {
                Console.WriteLine(cartas.Valor + " " + cartas.palosCartas);
            }
           
               
        }

        public int cantidadCartasBaraja()
        {
            return cartas.Count;
        }

        public void Barajar()
        {
            int n = cantidadCartasBaraja();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Carta carta = cartas[k];
                cartas[k] = cartas[n];
                cartas[n] = carta;
            }
        }

        public Carta robarCarta(int contador)
        {
            Carta cartaRobada = cartas[contador];
            return cartaRobada;
        }

    }

    public class JugadorCartas
    {
        private List<Carta> manoJugador;
        
        public string Nombre { get; }
        

        public JugadorCartas(string nombre)
        {
            Nombre = nombre;
            manoJugador = new List<Carta>();
        }

        public void AgarrarCarta(Carta cartaRobada)
        {
            manoJugador.Add(cartaRobada);
        }

        public void mostrarCartas()
        {
            foreach (Carta cartas in manoJugador)
            {
                Console.WriteLine($"Mano del jugador: {Nombre}");
                Console.WriteLine(cartas.Valor + " " + cartas.palosCartas);
            }
        }

    }

    public class jugadorBlackJack : JugadorCartas
    {
        public int puntuación = 0;
        public jugadorBlackJack(string nombre) : base(nombre)
        {
        }


        public int comprobarAs()
        {    
            if (sumarPuntos() > 10)
            {
                return 1;
            }
            else
            {
                return 11;
            }
        }

        public bool Victoria()
        {
            if (sumarPuntos() > 21)
            {
                return false;
            }
            else if (sumarPuntos() == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    static void Main(string[] args)
    {
        

        Baraja baraja = new Baraja();
        baraja.Barajar();
        //Carta carta;
        
        //carta1.mostrarCarta();
        //JugadorCartas jugador = new JugadorCartas(Console.ReadLine() ?? "");
        jugadorBlackJack jugador = new jugadorBlackJack("Jimmy");
        jugador.AgarrarCarta(baraja.robarCarta(51));
        jugador.AgarrarCarta(baraja.robarCarta(50));
        jugador.mostrarCartas();
        bool comprobar = jugador.Victoria();
        int n = (baraja.cantidadCartasBaraja()-3);
       if (comprobar == true)
        {
            Console.WriteLine("Has ganado");
        }
        else
        {
            while (comprobar)
            {
                jugador.sumarPuntos();
                //Console.WriteLine("puntos "+jugador.sumarPuntos());
                comprobar = jugador.Victoria();
                Console.WriteLine(comprobar);


                jugador.AgarrarCarta(baraja.robarCarta(n));
                jugador.mostrarCartas();


                //Console.WriteLine("n vale " + n);
                n--;
            }
        }


        Console.WriteLine(jugador.Nombre);
    }

}

