﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MesaSuite.Common.Data
{
    public class PutData : DataAccess
    {
        private object _objectToPut;
        public PutData(APIs api, string resource, object objectToPut) : base(api, resource)
        {
            _objectToPut = objectToPut;
        }

        private async Task<string> InternalExecute()
        {
            string uri = GetResourceURI();

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Method = WebRequestMethods.Http.Put;
            if (RequireAuthentication)
            {
                request.Headers.Add("Authorization", "Bearer " + Authentication.GetAuthToken());
            }
            foreach(KeyValuePair<string, string> kvp in Headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(_objectToPut));
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                string json;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    json = await reader.ReadToEndAsync();
                }

                RequestSuccessful = true;
                return json;
            }
            catch(WebException ex)
            {
                await HandleResponseWebException(ex, new ResponseWebExceptionRetryCallback(InternalExecute));
            }

            return null;
        }

        public async Task<T> Execute<T>()
        {
            string json = await InternalExecute();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task ExecuteNoResult()
        {
            await InternalExecute();
        }
    }
}
