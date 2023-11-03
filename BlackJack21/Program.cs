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
                    //Console.WriteLine($"Carta: {value} de {paloCarta}.");
                }
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
                //Console.WriteLine($"{carta.palosCartas} + {carta.Valor}");
            }
        }

        public Carta robarCarta(int contador)
        {
            Carta cartaRobada = cartas[contador];
            //Console.WriteLine(cartaRobada.Valor);
            return cartaRobada;
        }

    }

    public class JugadorCartas
    {
        private List<Carta> manoJugador;

        public string nombre { get; }
        

        public JugadorCartas(string Nombre)
        {
            Nombre = nombre;
            manoJugador = new List<Carta>();
        }
        //Console.ReadLine()
        public void AgarrarCarta(Carta cartaRobada)
        {
            manoJugador.Add(cartaRobada);
        }

        public void mostrarCartas()
        {
            foreach (Carta cartas in manoJugador)
            {
                Console.WriteLine(cartas.Valor + " " + cartas.palosCartas);
            }
        }
    }


    static void Main(string[] args)
    {
  
        Baraja baraja = new Baraja();
        baraja.Barajar();
        Carta carta1 = new Carta(palosCartas.picas, 2);
        //carta1.mostrarCarta();
        //JugadorCartas jugador = new JugadorCartas(Console.ReadLine() ?? "");
        JugadorCartas jugador = new JugadorCartas("Jimmy");
        int n = (baraja.cantidadCartasBaraja()-1);
        while (n > 0)
        {
            jugador.AgarrarCarta(baraja.robarCarta(n));
            n--;
        }
        
        jugador.mostrarCartas();
        Console.WriteLine(jugador.nombre);
    }

}

