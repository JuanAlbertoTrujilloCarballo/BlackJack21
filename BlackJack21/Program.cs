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

        public int CantidadCartasBaraja()
        {
            return cartas.Count;
        }

        public void Barajar()
        {
            int n = CantidadCartasBaraja();
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

        public Carta RobarCarta(int contador)
        {
            Carta cartaRobada = cartas[contador];
            Console.WriteLine(cartaRobada.Valor);
            return cartaRobada;
        }

    }

    static void Main(string[] args)
    {
        Baraja baraja = new Baraja();
        baraja.Barajar();
        Carta carta1 = new Carta(palosCartas.picas, 2);
        //carta1.mostrarCarta();
        Console.WriteLine(baraja.RobarCarta(1)+ " Esto es aqui");
    }

}

