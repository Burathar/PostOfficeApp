using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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

namespace PostOffice
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

        private void BtnAddPost_Click(object sender, RoutedEventArgs e)
        {
            if (TbRecipiantName.Text.Trim() == "" && NumUdRecipianNumber.Number == 0)
            {
                MessageBox.Show("Please enter a name and/or select a number higher than 0", "No recipiant info", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }
            Recipiant newRecipiant = new Recipiant(TbRecipiantName.Text.Trim(), NumUdRecipianNumber.Number > 0 ? NumUdRecipianNumber.Number : (int?)null, null);
            throw new NotImplementedException("Implement AddPost");
            RefreshListBox();
        }

        private void BtnRemovePost_Click(object sender, RoutedEventArgs e)
        {
            if (!(LbRecipiants.SelectedItem is Recipiant selectedRecipiant))
            {
                MessageBox.Show("Please select a recipiant to remove", "No recipiant selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            throw new NotImplementedException("Implement RemovePost");
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            LbRecipiants.Items.Clear();
            throw new NotImplementedException("Implement GetPosts");
        }
    }
}