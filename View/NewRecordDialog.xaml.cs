using HospitalServerManager.InterfacesAndEnums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Okno dialogowe zawartości jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace HospitalServerManager.View
{
	public sealed partial class NewRecordDialog : ContentDialog
	{
		private Dictionary<int, TextBlock> textBlocksDictionary = new Dictionary<int, TextBlock>();
		private Dictionary<int, Control> dataControlsDictionary = new Dictionary<int, Control>();
		private Dictionary<string, Type> enumTypes;
		private TextBlock AlertText = new TextBlock();
		private IEnumerable<string> neededValues;
		public List<string> ValuesOfNewObject { get; private set; }

		public NewRecordDialog(IEnumerable<string> namesOfColumn, IDictionary<int, string> typesOfColumn,
			IDictionary<string, Type> enumTypes)
		{
			this.InitializeComponent();
			this.neededValues = namesOfColumn;
			ValuesOfNewObject = new List<string>();
			this.enumTypes = enumTypes as Dictionary<string, Type>;
			CreateInterface(namesOfColumn, typesOfColumn);

		}
		private void CreateInterface(IEnumerable<string> namesOfColumn, IDictionary<int, string> typesOfColumn)
		{
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.RowSpacing = 10D;
			grid.ColumnSpacing = 10D;
			List<string> namesList = namesOfColumn.ToList();
			// List<string> typesList = typesOfColumn.ToList();
			foreach (string value in namesList)
			{
				int rowIndex = namesList.FindIndex(x => x == value);
				Control control = new TextBox();
				TextBlock descriptionTextBlock = new TextBlock();
				descriptionTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
				descriptionTextBlock.VerticalAlignment = VerticalAlignment.Center;
				descriptionTextBlock.HorizontalTextAlignment = TextAlignment.Center;
				Grid.SetColumn(descriptionTextBlock, 2);
				descriptionTextBlock.TextWrapping = TextWrapping.Wrap;
				descriptionTextBlock.FontSize = 12;
				grid.Children.Add(descriptionTextBlock);
				Grid.SetRow(descriptionTextBlock, rowIndex);
				// TODO: Dopisac pozostałe wyjatki dla pozostalych widokow
				if (typesOfColumn[rowIndex] == "date")
					descriptionTextBlock.Text = "Format: RRRR-MM-DD";
				else if (value == "PESEL")
					descriptionTextBlock.Text = "Format: 11 cyfr";
				else if (enumTypes.Keys.ToList().Contains(value)) // TODO: Dodac pozostale kolumny z enum
				{
					ComboBox cBox = new ComboBox();
					Type type = enumTypes[value];
					List<string> list = new List<string>(Enum.GetNames(enumTypes[value]).ToList());
					List<string> descriptionsList = new List<string>();
					foreach (string x in list)
					{
						var enumX = Enum.Parse(type, x);
						descriptionsList.Add((enumX as Enum).GetEnumDescription());

					}

					cBox.ItemsSource = descriptionsList;
					cBox.SelectedIndex = 0;
					control = cBox;
					descriptionTextBlock.Text = "Brak dodatkowych warunków";
				}
				else
					descriptionTextBlock.Text = "Brak dodatkowych warunków";

				grid.RowDefinitions.Add(new RowDefinition());
				TextBlock newTextBlock = new TextBlock();
				//        TextBox newTextBox = new TextBox();
				grid.Children.Add(newTextBlock);
				grid.Children.Add(control);
				Grid.SetColumn(control, 1);
				newTextBlock.Width = control.Width = 140;
				newTextBlock.Text = value;
				Grid.SetRow(control, rowIndex);
				Grid.SetRow(newTextBlock, rowIndex);
				textBlocksDictionary.Add(rowIndex, newTextBlock);
				dataControlsDictionary.Add(rowIndex, control);


			}
			grid.Children.Add(AlertText);
			grid.RowDefinitions.Add(new RowDefinition());
			Grid.SetRow(AlertText, grid.RowDefinitions.Count - 1);
			Grid.SetColumnSpan(AlertText, 3);
		}

		private bool CheckDialogIsFilled()
		{
			bool response = true;
			foreach (Control control in dataControlsDictionary.Values)
			{
				if (control is TextBox)
				{
					if (string.IsNullOrEmpty((control as TextBox).Text))
					{
						response = false;
						break;
					}
				}
			}
			if (textBlocksDictionary[0].Text == "PESEL")
			{
				TextBox textBoxToCheck = (dataControlsDictionary[0] as TextBox);
				int peselLength = textBoxToCheck.Text.Length;
				if (peselLength != 11)
				{
					response = false;
					textBoxToCheck.Text = string.Empty;
				}

			}

			return response;
		}
		private void StoreData()
		{
			foreach (Control control in dataControlsDictionary.Values)
			{
				if (control is TextBox)
					ValuesOfNewObject.Add((control as TextBox).Text);
				else
					ValuesOfNewObject.Add((control as ComboBox).SelectedItem as string);
			}

		}
		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
			bool isFilled = CheckDialogIsFilled();
			if (isFilled)
				StoreData();
			else
				AlertText.Text = "Wypełnij brakujące pola lub sprawdź poprawność danych!";
			args.Cancel = !isFilled;
		}

		private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{

		}
	}
}
