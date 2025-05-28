using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ClienteTcp
{
    static async Task Main()
    {
        const string host = "127.0.0.1";
        const int porta = 9000;
        using var cliente = new TcpClient();
        await cliente.ConnectAsync(host, porta);
        using var stream = cliente.GetStream();

        Console.WriteLine("Conectado ao servidor. Digite mensagens para enviar:");

        while (true)
        {
            Console.Write("> ");
            string entrada = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entrada))
                continue;

            Enviar(stream, entrada);

            string resposta = Receber(stream);
            Console.WriteLine($"Servidor: {resposta}");
        }
    }

    static void Enviar(NetworkStream stream, string msg)
    {
        var data = Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }

    static string Receber(NetworkStream stream)
    {
        var buf = new byte[32];
        int len = stream.Read(buf, 0, buf.Length);
        return Encoding.ASCII.GetString(buf, 0, len);
    }
}
