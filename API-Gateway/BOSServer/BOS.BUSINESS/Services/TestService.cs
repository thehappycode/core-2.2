using BOS.BUSINESS.Infrastructures;
using BOS.BUSINESS.Sessions;
using BOS.DB.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BOS.BUSINESS.Services
{
    public class TestService : TemplateService
    {
        
        List<_Entities> boss = new List<_Entities> {
                new _Entities { Group = 1, Id = 1, Name = "BOS1" },
                new _Entities{ Group = 1, Id = 2, Name = "BOS2" },
                new  _Entities{ Group = 2, Id = 3, Name = "BOS3" },
                new _Entities { Group = 2, Id = 4, Name = "BOS4" },
            }.ToList();

        public dynamic GetAll()
        {
            try
            {
                return new { status = 200, message = "success", data = boss };
            }
            catch (Exception ex)
            {

                return new { status = 200, message = ex.Message, data = new { } };
            }
            
        }
        public async Task<dynamic> Get(HttpClient _client, string _requestUrl, string _accessToken)
        {
            dynamic results = null;
            var ecms = await HttpClientFactoryApi.GetAsync<List<_Entities>>(_client, _requestUrl, _accessToken);
            results = boss.Join(
                    ecms,
                    bos => bos.Group,
                    ecm => ecm.Group,
                    (bos, ecm) => new
                    {
                        bos.Group,
                        bosId = bos.Id,
                        bosName = bos.Name,
                        ecmId = ecm.Id,
                        ecmName = ecm.Name
                    }
                )
                .ToList();

            return new { status = 200, message = "success", data = results }; ;
        }
        public async Task<dynamic> Post(HttpClient _client, string _requestUrl, string _accessToken, _Entities data)
        {
            dynamic results = null;
            results = await HttpClientFactoryApi.PostAsync<_Entities>(_client, _requestUrl, _accessToken, data);
            return new { status = 200, message = "success", data = results }; ;
        }
        public async Task<dynamic> Put(HttpClient _client, string _requestUrl, string _accessToken, _Entities data)
        {
            dynamic results = null;
            results = await HttpClientFactoryApi.PutAsync<_Entities>(_client, _requestUrl, _accessToken, data);
            return new { status = 200, message = "success", data = results }; ;
        }
        public async Task<dynamic> Delete(HttpClient _client, string _requestUrl, string _accessToken)
        {
            dynamic results = null;
            results = await HttpClientFactoryApi.DeleteAsync(_client, _requestUrl, _accessToken);
            return new { status = 200, message = "success", data = results }; ;
        }

    }
}
