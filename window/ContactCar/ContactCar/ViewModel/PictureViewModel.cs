using ContactCar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.ViewModel
{
    public class PictureViewModel
    {
        private object _lock = new object();
        private List<Picture> lstPicture = new List<Picture>(); 

        public async Task GetDataAsync()
        {
            var resp = await App.networkManager.GetPictureData();

            if (resp == null || resp.Status != 200)
            {
                return;
            }
            else if(resp != null && resp.Data != null && resp.Status == (int)HttpStatusCode.OK)
            {
                lstPicture = resp.Data;
                SetImageData();
            }
        }

        private void SetImageData()
        {
            foreach(Picture picture in lstPicture)
            {
                Sale sale = App.SaleViewModel.Items.Where(s => s.Id == picture.PostId).FirstOrDefault();

                if(sale == null)
                {
                    continue;
                }
                lock(_lock) {
                    sale.lstPicture.Add(picture);
                }
            }
        }
    }
}
