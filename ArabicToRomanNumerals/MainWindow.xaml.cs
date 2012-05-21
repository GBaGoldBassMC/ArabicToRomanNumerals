using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace ArabicToRomanNumerals
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Arabic/Roman numeral data array. This provides the "digits" that make up a roman numeral.
		/// </summary>
		private readonly Dictionary<int, string> _numeralDictionary = new Dictionary<int, string>
		                                        	{
		                                        		//ones
		                                        		{1, "I"},{2, "II"},{3, "III"},{4, "IV"},{5, "V"},{6, "VI"},{7, "VII"},{8, "VIII"},{9, "IX"},
		                                        		//tens
		                                        		{10, "X"},{20, "XX"},{30, "XXX"},{40, "XL"},{50, "L"},{60, "LX"},{70, "LXX"},{80, "LXX"},{90, "XC"},
		                                        		//hundreds
		                                        		{100, "C"},{200, "CC"},{300, "CCC"},{400, "CD"},{490, "XD"},{500, "D"},{600, "DC"},{700, "DCC"},{800, "DCCC"},{900, "CM"},{990, "XM"},
		                                        		//thousands
		                                        		{1000, "M"},{2000, "MM"},{3000, "MMM"},
		                                        	};

		public MainWindow()
		{
			InitializeComponent();
		}

		private void cmdConvert_Click(object sender, RoutedEventArgs e)
		{
			lblRoman.Content = ArabicToRomanNumeralConversion(txtArabic.Text);
			txtArabic.Focus();
			txtArabic.SelectAll();
		}

		/// <summary>
		/// Receive a valid arabic numeral (integer)
		/// </summary>
		/// <param name="arabicNumeral">Integer between 1 and 3999.</param>
		/// <returns></returns>
		private string ArabicToRomanNumeralConversion(string arabicNumeral)
		{
			var errorMessage = string.Empty;
			var romanNumeral = string.Empty;
			if (!string.IsNullOrWhiteSpace(arabicNumeral))
			{
				int arabicNumber;
				if (Int32.TryParse(arabicNumeral, out arabicNumber))
				{
					if (arabicNumber >= 1 && arabicNumber <= 3999)
					{
						var digits = arabicNumeral.ToCharArray();
						for (var digit = 0; digit < digits.Length; digit++)
						{
							var fullDigitValue =
								Convert.ToInt16(digits[digit].ToString(CultureInfo.InvariantCulture).PadRight(digits.Length - digit, '0'));
							string romanDigit;
							if (_numeralDictionary.TryGetValue(fullDigitValue, out romanDigit))
								romanNumeral += romanDigit;
						}
						if (string.IsNullOrWhiteSpace(romanNumeral)) errorMessage = "Unable to convert to a Roman Numeral :(";
					}
					else
					{
						errorMessage = "Please enter an Arabic Numeral between and 1 and 3999";
					}
				}
				else
				{
					errorMessage = "Please enter a valid Arabic Numeral";
				}
			}
			else
			{
				errorMessage = "Please enter an Arabic Numeral";
			}
			if (!String.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentException(errorMessage, "arabicNumeral");
			return romanNumeral;
		}
	}
}