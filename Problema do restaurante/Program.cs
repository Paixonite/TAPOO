using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

BlockingCollection<(int pedido, int prato)> pedidos = new BlockingCollection <(int pedido, int prato)>();
object lockConsole = new object();

int numGracons = 5;
int numChefs = 3;

void ConsoleLock(string msg, ConsoleColor color)
{
    lock(lockConsole)
    {
        var aux = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(msg);
        Console.ForegroundColor = aux;
    }
}

void Garcom(int numero)
{
    var rnd = new Random();
    int pedido = 1;
    ConsoleLock($"[Garçom {numero}] Estou pronto!", ConsoleColor.Blue);
    while(true)
    {
        int tempo = rnd.Next(1_000, 10_000);
        int prato = rnd.Next(1, 4);
        Thread.Sleep(tempo);

        ConsoleLock($"[Graçom {numero}] Enviei o prato {prato} do pedido {pedido}", ConsoleColor.Blue);
        pedidos.Add((pedido, prato));
        pedido++;
    }
}

void Chef(int numero)
{
    ConsoleLock("[Chef]Estou pronto!", ConsoleColor.Red);
    foreach(var item in pedidos.GetConsumingEnumerable())
    {
        var (pedido, prato) = item;
        ConsoleLock($"[Chef {numero}] Iniciando o prato {prato} do pedido {pedido}!", ConsoleColor.Red);

        if(prato is 1 or 2) Thread.Sleep(2_000);
        else Thread.Sleep(3_000);

        ConsoleLock($"[Chef {numero}] Finalizando o prato {prato} do pedido {pedido}!", ConsoleColor.Red);
    }
}

var g = new Task[numGracons];
var c = new Task[numChefs];

for(int i = 0; i < numGracons; i++){
    int id = i + 1;
    g[i] = Task.Run(() => Garcom(id));
}
for(int i = 0; i < numChefs; i++){
    int id = i + 1;
    c[i] = Task.Run(() => Chef(id));
}

Task.WaitAll(g);
Task.WaitAll(c);
