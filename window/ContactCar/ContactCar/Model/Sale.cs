using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Model
{
    public class Sale : BindableBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
     
        [JsonProperty("userId")]
        public int UserId { get; set; }

        private string _title;
        [JsonProperty("title")]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _content;

        [JsonProperty("content")]
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private int _price;
        [JsonProperty("price")]
        public int Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        [JsonIgnore]
        public List<Picture> lstPicture { get; set; } = new List<Picture>();

        [JsonIgnore]
        public ObservableCollection<Comment> lstComment { get; set; } = new ObservableCollection<Comment>();

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        private DateTime _updated;
        [JsonProperty("updated")]
        public DateTime Updated
        {
            get => _updated;
            set => SetProperty(ref _updated, value);
        }
    }
}
