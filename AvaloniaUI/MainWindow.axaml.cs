using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MeuProjetoAvalonia
{
	public partial class MainWindow : Window
	{
    	public MainWindow()
    	{
        	// Inicializa a interface a partir do XAML
        	InitializeComponent();
    	}

    	// Evento chamado quando o botão é clicado
    	private void BtnClique_Click(object sender, RoutedEventArgs e)
    	{
			// Recupera a referência ao TextBox
			var txtBox = this.FindControl<TextBox>("txtInput");
			var comboBox = this.FindControl<ComboBox>("conversionTypes");
			var txtOutput = this.FindControl<TextBox>("txtOutput");

			// Recupera e verifica o valor digitado no TextBox
		b	if (!double.TryParse(txtBox.Text, out double valor))
			{
				txtOutput.Text = "Valor inválido"; // Exibe erro se a conversão falhar
				return;
			}

			// Recupera a conversão selecionada
			string conversaoSelecionada  = "";
			if (comboBox.SelectedItem is ComboBoxItem selectedItem)
			{
				conversaoSelecionada = selectedItem.Content.ToString();
			}

			// Converte
			double resultado = 0;

			switch (conversaoSelecionada)
			{
				case "Celsius - Farenheit":
					resultado = (valor * 1.8) + 32;
					break;

				case "Farenheit - Celsius":
					resultado = (valor - 32) / 1.8;
					break;

				case "Celsius - Kelvin":
					resultado = valor + 273.15;
					break;

				case "Kelvin - Celsius":
					resultado = valor - 273.15;
					break;

				case "Metros - Pés":
					resultado = valor * 3.28084;
					break;

				case "Pés - Metros":
					resultado = valor * 0.3048;
					break;

				case "Quilômetros - Milhas":
					resultado = valor * 0.621371;
					break;

				case "Milhas - Quilômetros":
					resultado = valor * 1.60934;
					break;

				case "Gramas - Onças":
					resultado = valor * 0.035274;
					break;

				case "Onças - Gramas":
					resultado = valor * 28.3495;
					break;

				case "Quilogramas - Libras":
					resultado = valor * 2.20462;
					break;

				case "Libras - Quilogramas":
					resultado = valor * 0.453592;
					break;

				case "Litros - Galões":
					resultado = valor * 0.264172;
					break;

				case "Galões - Litros":
					resultado = valor * 3.78541;
					break;

				case "Mililitros - Onças Fluidas":
					resultado = valor * 0.033814;
					break;

				case "Onças Fluidas - Mililitros":
					resultado = valor * 29.5735;
					break;

				default:
					resultado = 666;
					break;
			}

			// Escreve valor na caixa output
			txtOutput.Text = resultado.ToString();
			//txtOutput.Text = conversaoSelecionada;

    	}
	}
}