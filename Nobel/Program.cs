using System.Text.RegularExpressions;
using System.IO;

var filePath = "prize.json";
var str = File.ReadAllText(filePath);
var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()+=_\-{}\[\]:;""'?<>,.]).{7,16}$");



// Imprime matches
foreach (Match match in regex.Matches(str))
{
    foreach (Group group in match.Groups)
    {
        if (group.Success)
        {
            Console.WriteLine(group.Value);
        }
    }
}
