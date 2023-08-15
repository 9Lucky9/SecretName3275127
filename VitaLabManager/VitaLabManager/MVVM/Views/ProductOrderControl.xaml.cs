using System.Windows;
using System.Windows.Controls;

namespace VitaLabManager.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductOrderControl.xaml
    /// </summary>
    public partial class ProductOrderControl : UserControl
    {
        public ProductOrderControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Count_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "1";
            }
        }
    }
}
