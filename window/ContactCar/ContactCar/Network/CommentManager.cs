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
        private const string COMMENT = "comment/";
        public async Task<CResponse<List<Comment>>> GetCommentData()
        {
            var resp = await GetResponseAsync<List<Comment>>(COMMENT, RestSharp.Method.GET);
            return resp;
        }

        public async Task<CResponse<List<Comment>>> GetCommentData(int id)
        {
            var resp = await GetResponseAsync<List<Comment>>(COMMENT + id, RestSharp.Method.GET);
            return resp;
        }

        public async Task<CResponse<Comment>> PostComment(JObject json)
        {
            var resp = await GetResponseAsync<Comment>(COMMENT, RestSharp.Method.POST, json);
            return resp;
        }

        public async Task<CResponse<Comment>> EditComment(int id, JObject json)
        {
            var resp = await GetResponseAsync<Comment>(COMMENT + id, RestSharp.Method.PUT, json);
            return resp;
        }

        public async Task<CResponse<Boolean>> DeleteComment(int id)
        {
            var resp = await GetResponseAsync<Boolean>(COMMENT + id, RestSharp.Method.DELETE);
            return resp;
        }
    }
}
