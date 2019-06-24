using ContactCar.Model;
using ContactCar.Network.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Network
{
    public partial class NetworkManager
    {
        private const string SALE = "sale/";
        public async Task<CResponse<List<Sale>>> GetSaleData()
        {
            var resp = await GetResponseAsync<List<Sale>>(SALE, RestSharp.Method.GET);
            return resp;
        }

        public async Task<CResponse<Sale>> PostSale(JObject json)
        {
            var resp = await GetResponseAsync<Sale>(SALE, RestSharp.Method.POST, json);
            return resp;
        }

        public async Task<CResponse<Sale>> EditSale(int id, JObject json)
        {
            var resp = await GetResponseAsync<Sale>(SALE + id, RestSharp.Method.PUT, json);
            return resp;
        }

        public async Task<CResponse<Boolean>> DeleteSale(int id)
        {
            var resp = await GetResponseAsync<Boolean>(SALE + id, RestSharp.Method.DELETE);
            return resp;
        }
    }
}
