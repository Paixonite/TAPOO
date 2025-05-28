using System;
using System.Collections.Concurrent;
using System.Threading;

public class Garcom
{
    private int idGarcom;
    private BlockingCollection<(int pedido, int prato)> pedidos;
    private Random rnd = new Random();

    static int pedido = 1;
    static object pedidoLock = new object();

    public Garcom(int idGarcom, BlockingCollection<(int pedido, int prato)> pedidos)
    {
        this.idGarcom = idGarcom;
        this.pedidos = pedidos;
    }

    public void Trabalhar()
    {
        Program.ConsoleLock($"[Garçom {idGarcom}] Estou pronto!", ConsoleColor.Blue);

        while (true)
        {
            int tempo = rnd.Next(1_000, 10_000);
            int prato = rnd.Next(1, 4);
            Thread.Sleep(tempo);

            lock(pedidoLock)
            {
                pedidos.Add((pedido, prato));
                pedido++;
            }

            Program.ConsoleLock($"[Garçom {idGarcom}] Enviei o prato {Program.nomesPratos[prato-1]} do pedido {pedido}!", ConsoleColor.Blue);
        }
    }
}