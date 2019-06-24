using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Model
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("postId")]
        public int PostId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
