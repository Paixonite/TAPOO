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
			var inputTextBox = this.FindControl<TextBox>("txtInput");
			var conversionComboBox = this.FindControl<ComboBox>("conversionTypes");
			var outputTextBox = this.FindControl<TextBox>("txtOutput");

			// Recupera e verifica o valor digitado no TextBox
			if (!double.TryParse(inputTextBox.Text, out double value))
			{
				outputTextBox.Text = "Valor inválido"; // Exibe erro se a conversão falhar
				return;
			}

			// Recupera a conversão selecionada
			string selectedConversion = "";
			if (conversionComboBox.SelectedItem is ComboBoxItem selectedItem)
			{
				selectedConversion = selectedItem.Content.ToString();
			}

			// Converte
			double result = 0;

			switch (selectedConversion)
			{
				case "Celsius - Farenheit":
					result = (value * 1.8) + 32;
					break;

				case "Farenheit - Celsius":
					result = (value - 32) / 1.8;
					break;

				case "Celsius - Kelvin":
					result = value + 273.15;
					break;

				case "Kelvin - Celsius":
					result = value - 273.15;
					break;

				case "Metros - Pés":
					result = value * 3.28084;
					break;

				case "Pés - Metros":
					result = value * 0.3048;
					break;

				case "Quilômetros - Milhas":
					result = value * 0.621371;
					break;

				case "Milhas - Quilômetros":
					result = value * 1.60934;
					break;

				case "Gramas - Onças":
					result = value * 0.035274;
					break;

				case "Onças - Gramas":
					result = value * 28.3495;
					break;

				case "Quilogramas - Libras":
					result = value * 2.20462;
					break;

				case "Libras - Quilogramas":
					result = value * 0.453592;
					break;

				case "Litros - Galões":
					result = value * 0.264172;
					break;

				case "Galões - Litros":
					result = value * 3.78541;
					break;

				case "Mililitros - Onças Fluidas":
					result = value * 0.033814;
					break;

				case "Onças Fluidas - Mililitros":
					result = value * 29.5735;
					break;

				default:
					result = 666;
					break;
			}

			// Escreve valor na caixa output
			outputTextBox.Text = result.ToString();
			//outputTextBox.Text = selectedConversion;

    	}
	}
}