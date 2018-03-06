using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba2WPF  //2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59(6), ... , 3997859(22), ... , 27644437(25)  (log2(n))
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultFerma.Text = "";
            ResulRabinMiller.Text = "";

            if (BigInteger.TryParse(Number.Text, out BigInteger number))
            {
                string asd = number.ToString();
                if (Decimal.TryParse(asd, out decimal asds))
                {
                    ResultFerma.Text = TestFerma.IsPrime(Convert.ToDecimal(asds)).ToString();
                }
 
                ResulRabinMiller.Text = RabinMillerBig.IsPrime(number, 10).ToString();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Number.Text, out double num))
            {
                num = Math.Log(num, 2);

                Round.Text = Math.Round(num).ToString();

                Round.IsReadOnly = true;
            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Round.IsReadOnly = false;
        }
    }
}
