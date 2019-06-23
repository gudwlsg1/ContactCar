using ContactCar.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ContactCar.Converter
{
    class FindUserNameByUserIdxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(App.authViewModel.Items == null)
            {
                return null;
            }
            User user = App.authViewModel.Items.Where(w => w.Id == (int)value).FirstOrDefault();

            if(user == null)
            {
                return null;
            }

            return user.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
