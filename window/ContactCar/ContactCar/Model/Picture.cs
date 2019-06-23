using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Model
{
    public class Picture
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("postId")]
        public int PostId { get; set; }

        private string _path;
        [JsonProperty("path")]
        public string Path
        {
            get => _path;
            set
            {
                _path = "http://localhost:8000/images/" + value;
            }
        }
    }
}
