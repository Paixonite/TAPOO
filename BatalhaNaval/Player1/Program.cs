using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


class ServidorTcp
{
    const int NUM_SHIPS = 10;

    private static TcpListener listener;

    static async Task Main()
    {
        Board board = new Board();

        Console.WriteLine("Bem-vindo à batalha naval!");
        SetupBoard(board);
        
        StartServer(9000);
        while (true)
        {
            var cliente = await listener.AcceptTcpClientAsync();
            _ = TratarCliente(cliente, board); 
        }
    }

    public static void SetupBoard(Board board)
    {
        string opcao;  
        string opcao2;

        do
        {
            Console.WriteLine("[1] Colocar navios aleatoriamente");
            Console.WriteLine("[2] Colocar navios manualmente");
            opcao = Console.ReadLine();

            if (opcao == "1")
            {
                Console.WriteLine("Colocando navios aleatoriamente...");
                board.PlaceShipsRandomly(NUM_SHIPS); 
            }
            else if (opcao == "2")
            {
                Console.WriteLine("Colocando navios manualmente...");
                for (int i = 0; i < NUM_SHIPS; i++)
                {
                    Console.WriteLine($"Colocando navio {i + 1} (exemplo: A5): ");
                    string coords = Console.ReadLine();

                    try
                    {
                        var (x, y) = board.ParseInput(coords);

                        board.PlaceShipManually(x, y);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}. Tente novamente.");
                        i--;
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }

            board.PrintBoard();

            Console.WriteLine("[1] Confirmar");
            Console.WriteLine("[2] Refazer");
            opcao2 = Console.ReadLine();

            if (opcao2 == "1")
            {
                Console.WriteLine("Navios colocados com sucesso!");
            }
            else if (opcao2 == "2")
            {
                board = new Board();
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }

        } while (opcao != "1" && opcao != "2" || opcao2 != "1");
    }

    public static void StartServer(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Servidor ouvindo na porta {port}...");
    }

    static async Task TratarCliente(TcpClient cliente, Board board)
    {
        Console.WriteLine("Cliente conectado!");
        using var stream = cliente.GetStream();

        try
        {
            while (cliente.Connected)
            {
                string mensagem = await Task.Run(() => Receber(stream));

                Console.WriteLine($"Recebido: {mensagem}");

                string resposta;
                try
                {
                    // Unpack the tuple returned by ParseInput
                    var (x, y) = board.ParseInput(mensagem);

                    // Pass the unpacked values to the Hit method
                    if (board.Hit(x, y))
                    {
                        resposta = "HIT";
                    }
                    else
                    {
                        resposta = "MISS";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar a mensagem: {ex.Message}");
                    resposta = "ERRO";
                }

                board.PrintBoard();

                if (board.IsGameOver())
                {
                    resposta = "GAME OVER";
                    Console.WriteLine("Fim de jogo!");
                    await Task.Run(() => Enviar(stream, resposta));
                    break; 
                    //falta desconectar o cliente de forma elegante
                }
                await Task.Run(() => Enviar(stream, resposta));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        Console.WriteLine("Cliente desconectado.");
        cliente.Close();
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
