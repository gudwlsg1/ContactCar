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
        }

        private void SaleViewModel_ShowDetailViewEvent(Model.Sale selectionSale)
        {
           // this.DataContext = selectionSale;
        }
    }
}
