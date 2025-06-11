using System.Text.RegularExpressions;
using System.IO;

var filePath = "prize.json";
var str = File.ReadAllText(filePath);
var regex = new Regex(@"""category"":""economics"",""laureates"":\[.+?""firstname"":(?<nome>""[^""]+"")[^\]]+\]");

// Imprime matches
foreach (Match match in regex.Matches(str))
{
    foreach (Group group in match.Groups)
    {        
        if (group.Name == "nome")
        {
            Console.WriteLine($"NOME: {group.Value}");
        }
    }
}