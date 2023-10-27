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

    public void Barajar() 
    {
        int n = cartas.Count;
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

    public int CantidadCartasBaraja()
        {
            Console.WriteLine($"{cartas.Count}");
            return cartas.Count;
        }
    }

    static void Main(string[] args)
    {
        Baraja baraja = new Baraja();
        baraja.Barajar();
        baraja.CantidadCartasBaraja();
        Carta carta1 = new Carta(palosCartas.picas, 2);
        carta1.mostrarCarta();
    }

}

