using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

Console.WriteLine("Digite a unidade de temperatura de desejada:");
Console.WriteLine("[1] Celsius");
Console.WriteLine("[2] Fahrenheit");
Console.WriteLine("[3] Kelvin");

var unitOption = Console.ReadLine();

switch (unitOption)
{
    case "1":
        unitOption = "celsius";
        break;
    case "2":
        unitOption = "fahrenheit";
        break;
    case "3":
        unitOption = "kelvin";
        break;
    default:
        Console.WriteLine("Opção inválida. Usando Celsius como padrão.");
        unitOption = "celsius";
        break;
}

Console.WriteLine("Digite o intervalo em segundos entre as requisições:");
var intervalInput = Console.ReadLine();

int intervalSeconds;
while (!int.TryParse(intervalInput, out intervalSeconds) || intervalSeconds <= 0)
{
    Console.WriteLine("Digite um número inteiro positivo para o intervalo:");
    intervalInput = Console.ReadLine();
}

HttpClient httpClient = new HttpClient();

Console.WriteLine("Iniciando leitura periódica de temperatura. Pressione Ctrl+C para parar.");

double? lastTemperature = null;

while (true)
{
    try
    {
        // Faz a requisição para obter a temperatura
        var response = await httpClient.GetAsync($"http://localhost:5000/temperatura/{unitOption}");
        response.EnsureSuccessStatusCode();

        // Lê o conteúdo da resposta como string
        var content = await response.Content.ReadAsStringAsync();

        // Desserializa o JSON para um objeto dinâmico
        var temperatureData = JsonSerializer.Deserialize<JsonElement>(content);

        // Captura horário local
        var localTime = DateTime.Now.ToString("HH:mm:ss");

        Console.WriteLine($"[{localTime}] Temperatura: {temperatureData.GetProperty("valor")}° {temperatureData.GetProperty("unidade")}");

        // Caso haja uma temperatura anterior, compara com a atual
        double currentTemperature = temperatureData.GetProperty("valor").GetDouble();
        if (lastTemperature != null)
        {
            var variation =
                currentTemperature > lastTemperature ? "SUBIU" :
                currentTemperature < lastTemperature ? "DESCEU" : "SEM ALTERAÇÃO";

            // Define a cor do console com base na variação
            Console.ForegroundColor =
                variation == "SUBIU" ? ConsoleColor.Red :
                variation == "DESCEU" ? ConsoleColor.Blue :
                ConsoleColor.White;
            Console.WriteLine($">> {variation}");
        }
        // Restaura a cor do console
        Console.ResetColor();

        // Guarda a última temperatura lida
        lastTemperature = currentTemperature;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao obter temperatura: {ex.Message}");
    }

    // Aguarda o intervalo especificado
    await Task.Delay(intervalSeconds * 1000);
}