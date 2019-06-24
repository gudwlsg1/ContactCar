using System;
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
using ContactCar.Model;

namespace ContactCar.Controls
{
    /// <summary>
    /// Interaction logic for DetailViewControl.xaml
    /// </summary>
    public partial class DetailViewControl : UserControl
    {
        public DetailViewControl()
        {
            InitializeComponent();

            this.Loaded += DetailViewControl_Loaded;
        }

        private void DetailViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= DetailViewControl_Loaded;
            App.SaleViewModel.ShowDetailViewEvent += SaleViewModel_ShowDetailViewEvent;
            App.SaleViewModel.ResultEvent += SaleViewModel_ResultEvent; ;
           // this.DataContext = App.SaleViewModel.SelectedItem; //왜 안될까?
        }

        private void SaleViewModel_ResultEvent(bool isSuccess, string msg = null)
        {
            if (isSuccess)
            {
                this.Visibility = Visibility.Collapsed;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
            }
        }

        private void SaleViewModel_ShowDetailViewEvent(object sender, EventArgs e)
        {
            this.DataContext = App.SaleViewModel;
        }

        private void ReSetTextBox()
        {
            tbTitle.IsReadOnly = true;
            tbContent.IsReadOnly = true;
            tbPrice.IsReadOnly = true;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.IsReadOnly = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ReSetProperty();
            ReSetTextBox();
        }

        private void ReSetProperty()
        {
            App.SaleViewModel.SelectedItem = null;
            App.SaleViewModel.Comment = string.Empty;
        }
    }
}
