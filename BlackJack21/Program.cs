using System.Text;


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

        private string ValoresEspeciales()
        {
            switch (Valor)
            {
                case 1: return "A";
                case 11: return "J";
                case 12: return "Q";
                case 13: return "K";
                default: return Valor.ToString();
            }
        }

        public int ConseguirValor()
        {
            return Valor;
        }

        public void mostrarCarta()
        {
            
            Console.WriteLine($"Carta: {ValoresEspeciales()} de {palosCartas}.");
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
        public int CantidadCartasBaraja()
        {
            return cartas.Count;
        }

        public void MostrarCartas()
        {
            foreach (Carta cartas in cartas)
            {
                cartas.mostrarCarta();
            }
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
            }
        }

        public Carta RobarCarta()
        {
            Carta cartaRobada = cartas[0];
            cartas.RemoveAt(0);
            return cartaRobada;
        }

    }

    public class JugadorCartas
    {
        protected List<Carta> manoJugador;

        public string Nombre { get; }


        public JugadorCartas(string nombre)
        {
            Nombre = nombre;
            manoJugador = new List<Carta>();
        }

        public void RobarCarta(Carta cartaRobada)
        {
            manoJugador.Add(cartaRobada);
        }

        public void MostrarCartas()
        {
            foreach (Carta cartas in manoJugador)
            {
                Console.WriteLine($"Mano del jugador: {Nombre}");
                cartas.mostrarCarta();
            }
        }

    }

    public class JugadorBlackJack : JugadorCartas
    {
        protected int puntuacion;
        public JugadorBlackJack(string nombre) : base(nombre)
        {
        }


        public bool ComprobarAs()
        {
            foreach (Carta carta in manoJugador) if (carta.Valor == 1) return true;
            return false;
        }

        public void CalcularPuntuación()
        {
            puntuacion = 0;
            foreach (Carta carta in manoJugador)
            {
                if (carta.Valor > 10) puntuacion += 10;
                else puntuacion += carta.Valor;
                if (ComprobarAs() && puntuacion + 10 <= 21) puntuacion += 10;
            }
        }

        public int Puntuacion
        {
            get { return puntuacion; }
            private set { puntuacion = value; }
        }

        public bool Bust()
        {
            return puntuacion > 21;
        }

        public int NumeroCartasBajara()
        {
            return manoJugador.Count;
        }
    }

    public class Croupier : JugadorBlackJack
    {
        public Croupier() : base("Croupier") { }

        public void SacarCartas(Baraja baraja)
        {
            while (puntuacion < 17)
            {
                RobarCarta(baraja.RobarCarta());
                CalcularPuntuación();
            }
        }
      
    }
        private static Baraja baraja = new Baraja();
        private static JugadorBlackJack? jugador;
        private static Croupier croupier = new Croupier();
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("+------------------------------------+");
        Console.WriteLine("| +-----+ ------------------         |");
        Console.WriteLine("| |K    | JUEGO: Blackjack 21        |");
        Console.WriteLine("| |  ♦  | LENGUAJE: C#               |");
        Console.WriteLine("| |    K| AUTOR: Alberto Trujillo    |");
        Console.WriteLine("| +-----+ ------------------         |");
        Console.WriteLine("|         PROGRAMACIÓN Y MOTORES     |");
        Console.WriteLine("+------------------------------------+");
        Console.WriteLine("Introduce tu nombre:");
        jugador = new JugadorBlackJack(Console.ReadLine() ?? "");

        Console.Clear();
        baraja.Barajar();
        croupier.SacarCartas(baraja);
        croupier.SacarCartas(baraja);
        croupier.CalcularPuntuación();
        jugador.RobarCarta(baraja.RobarCarta());
        jugador.RobarCarta(baraja.RobarCarta());
        jugador.CalcularPuntuación();
        croupier.MostrarCartas();
        Console.WriteLine();
        jugador.MostrarCartas();
        Console.WriteLine();

        bool jugando = false;
        while (!jugando)
        {
            Console.WriteLine($"Tienes {jugador.Puntuacion} puntos");
            if (jugador.Puntuacion == 21) break;

            string pregunta = OtraCarta();

            if (pregunta == "n") jugando = true;
            else
            {
                Console.Clear();
                jugador.RobarCarta(baraja.RobarCarta());
                croupier.MostrarCartas();
                Console.WriteLine();
                jugador.MostrarCartas();
                Console.WriteLine();
            }
            jugador.CalcularPuntuación();
            if (jugador.Bust() || jugador.Puntuacion == 21) jugando = true;
        }

        croupier.SacarCartas(baraja);
        Console.WriteLine();
        VerResultados();
    }

    public static string OtraCarta()
    {
        Console.WriteLine("¿Quieres otra carta? (s/n)");
        string pregunta = "";
        while (pregunta != "s" && pregunta != "n")
        {
            pregunta = Console.ReadLine() ?? "".ToLower();
            if (pregunta != "s" && pregunta != "n") Console.WriteLine("Escribe s o n");
        }
        return pregunta;
    }

    public static void VerResultados()
    {
        Console.Clear();
        croupier.MostrarCartas();
        Console.WriteLine();
        jugador.MostrarCartas();
        Console.WriteLine();
        Console.WriteLine($"Puntuación Croupier: {croupier.Puntuacion}");
        Console.WriteLine($"Puntuación {jugador.Nombre}: {jugador.Puntuacion}");
        if (jugador.Bust()) Console.WriteLine("Has sacado bust");
        if (croupier.Bust()) Console.WriteLine("El Croupier ha sacado bust");

        if (jugador.Bust() && croupier.Bust()) Console.WriteLine("Empataron");
        else if (!jugador.Bust() && croupier.Bust()) Console.WriteLine("Ganaste");
        else if (jugador.Bust() && !croupier.Bust()) Console.WriteLine("Perdiste");
        else
            if (croupier.Puntuacion < jugador.Puntuacion) Console.WriteLine("Ganaste");
        else if (jugador.Puntuacion < croupier.Puntuacion) Console.WriteLine("Perdiste");
        else
        {
            if (croupier.NumeroCartasBajara() < jugador.NumeroCartasBajara()) Console.WriteLine("Perdiste, el Crupier ganó gracias a la ventaja de Crupier");
            else Console.WriteLine("Empate");
        }
    }
}

    



