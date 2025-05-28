using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    static BlockingCollection<(int pedido, int prato)> pedidos = new BlockingCollection<(int pedido, int prato)>();

    static object lockConsole = new object();
    public static string[] nomesPratos = { "executivo", "italiano", "especial" };

    static int numGarcons = 3;
    static int numChefs = 3;

    static void Main()
    {
        var garcons = new Task[numGarcons];
        var chefs = new Task[numChefs];

        for (int i = 0; i < numGarcons; i++)
        {
            int id = i + 1;
            garcons[i] = Task.Run(() =>
            {
                var g = new Garcom(id, pedidos);
                g.Trabalhar();
            });
        }

        for (int i = 0; i < numChefs; i++)
        {
            int id = i + 1;
            chefs[i] = Task.Run(() =>
            {
                var c = new Chef(id, pedidos);
                c.Trabalhar();
            });
        }

        Task.WaitAll(garcons);
        Task.WaitAll(chefs);
    }

    public static void ConsoleLock(string msg, ConsoleColor color)
    {
        lock (lockConsole)
        {
            var aux = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = aux;
        }
    }
}
