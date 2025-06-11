using System.Text.RegularExpressions;

Console.WriteLine("Regras:");
Console.WriteLine("A senha deve ter entre 7 e 16 caracteres.");
Console.WriteLine("Deve conter pelo menos uma letra minúscula (a-z).");
Console.WriteLine("Deve conter pelo menos uma letra maiúscula (A-Z).");
Console.WriteLine("Deve conter pelo menos um dígito (0-9).");
Console.WriteLine("Deve conter pelo menos um caractere especial entre os seguintes:");
Console.WriteLine("! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; \\ \" ' ? < > , .");

while (true)
{
    Console.WriteLine("\nDigite uma senha para validação:");

    var str = Console.ReadLine() ?? string.Empty;
    var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()+=_\-{}\[\]:;""'?<>,.]).{7,16}$");
    
    if (regex.IsMatch(str))
    {
        Console.WriteLine("Senha válida.");
        break;
    }
    else
    {
        Console.WriteLine("Senha inválida!");
    }
}