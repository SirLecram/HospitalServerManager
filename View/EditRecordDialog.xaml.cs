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
	public sealed partial class EditRecordDialog : ContentDialog
	{
		public string Result { get; private set; }
		public string FieldToUpdate { get; private set; }
		private Dictionary<int, ComboBox> comboBoxes = new Dictionary<int, ComboBox>();
		private Dictionary<int, Control> controlsDictionary = new Dictionary<int, Control>();
		private Dictionary<int, string> _TypesDictionary { get; set; }
		private Dictionary<string, Type> enumTypes;
		public EditRecordDialog(IEnumerable<string> comboboxSource, IDictionary<int, string> typesDictionary, string editedFieldToTitle,
			IDictionary<string, Type> enumTypesDictionary)
		{
			this.InitializeComponent();
			var valuesToCombobox = new List<string>( comboboxSource);
			_TypesDictionary = new Dictionary<int, string>( typesDictionary as Dictionary<int, string>);
			valuesToCombobox.RemoveAt(0);
			_TypesDictionary.Remove(0);
			enumTypes = enumTypesDictionary as Dictionary<string, Type>;
			CreateBasicInterface(valuesToCombobox, editedFieldToTitle);
			fieldToEdit.SelectedIndex = 0;

		}
		private void CreateBasicInterface(IEnumerable<string> comboboxSource, string editedFieldToTitle)
		{
			Title = editedFieldToTitle;

			TextBox textBox = new TextBox();
			textBox.VerticalAlignment = VerticalAlignment.Center;
			//textBox.Width = 170;
			textBox.Margin = new Thickness(20D);
			
			grid.Children.Add(textBox);
			Grid.SetRow(textBox, 2);
			/*Button btn = new Button();
			btn.Content = "+";
			grid.Children.Add(btn);
			comboBoxes.Add(0, fieldToEdit);*/
			controlsDictionary.Add(0, textBox);
			fieldToEdit.ItemsSource = comboboxSource;
		}

		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
			if (controlsDictionary[0] is ComboBox)
				Result = (controlsDictionary[0] as ComboBox).SelectedItem.ToString();
			else
				Result = (controlsDictionary[0] as TextBox).Text;
			FieldToUpdate = fieldToEdit.SelectedItem.ToString();

			// return Result;
		}

		private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//AddNextField();
		}

		private void FieldToEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int selectedIndex = fieldToEdit.SelectedIndex;
			string selectedText = fieldToEdit.SelectedItem.ToString();
			string typeOfField = _TypesDictionary[selectedIndex + 1];
			int changeInRow = Grid.GetRow(sender as ComboBox);

			// TODO: Dodać warunki co do pozostałych tabel, COMBOBOX JUZ JEST GENEROWANY!
			if (typeOfField.ToLower() == "date")
				additionalFormatInfo.Text = "Format: RRRR-MM-DD";
			else
				additionalFormatInfo.Text = "Format: brak wymagań";

			if (enumTypes.Keys.ToList().Contains(selectedText))
			{
				// TODO: Dodać moze datapicker dla wartosci datowych ?
				ComboBox cBox = new ComboBox();
				Type type = enumTypes[selectedText];
				List<string> list = new List<string>(Enum.GetNames(enumTypes[selectedText]).ToList());
				List<string> descriptionsList = new List<string>();
				foreach (string x in list)
				{
					var enumX = Enum.Parse(type, x);
					descriptionsList.Add((enumX as Enum).GetEnumDescription());

				}
				cBox.VerticalAlignment = VerticalAlignment.Center;
				cBox.HorizontalAlignment = HorizontalAlignment.Stretch;
				cBox.Margin = new Thickness(20D);
				cBox.ItemsSource = descriptionsList;
				cBox.SelectedIndex = 0;
				grid.Children.Remove(controlsDictionary[changeInRow]);
				controlsDictionary[changeInRow] = cBox;
				grid.Children.Insert(0, controlsDictionary[changeInRow]);
				Grid.SetRow(cBox, 2);
				additionalFilterInfo.Text = "Wybierz z dostępnych wartości";
				//firstValueStackPanel.Children.Add(controlsDictionary[changeInRow]);
				//additionalFilterInfo.Text = "Możliwości: 'KRYTYCZNY', 'STABILNY', 'ZAGROŻONY', 'NULL'";
			}
			else
			{
				TextBox tBox = new TextBox();
				tBox.VerticalAlignment = VerticalAlignment.Center;
				tBox.HorizontalAlignment = HorizontalAlignment.Stretch;
				tBox.Margin = new Thickness(20D);
				grid.Children.Remove(controlsDictionary[changeInRow]);
				Grid.SetRow(tBox, 2);
				controlsDictionary[changeInRow] = tBox;
				grid.Children.Insert(0, controlsDictionary[changeInRow]);
				additionalFilterInfo.Text = "Brak dodatkowych wymagań";
			}


			if (typeOfField == "varchar")
				additionalTypeInfo.Text = "Typ: " + "text";
			else
				additionalTypeInfo.Text = "Typ: " + typeOfField;
		}
	}
}
