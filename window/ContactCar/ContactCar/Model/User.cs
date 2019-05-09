using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Model
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

#if false //NOTUSED
        [JsonProperty("email_verified_at")]
        public string Email_Verified_At { get; set; }

        [JsonProperty("remember_token")]
        public object Remember_Token { get; set; }

        [JsonProperty("created_at")]
        public object Created_At { get; set; }

        [JsonProperty("updated_at")]
        public object Updated_At { get; set; }
#endif
    }
}
