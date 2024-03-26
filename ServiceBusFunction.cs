using System;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationWebHook.Models;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace OperationWebHook
{
    public class ServiceBusFunction
    {
        private readonly HttpClient _httpClient;
        public ServiceBusFunction(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [FunctionName("OperationWebHook")]
        public async Task Run([ServiceBusTrigger("OperationWebHook", Connection = "ServiceBusConnection")] Message message, MessageReceiver messageReceiver)
        {
            var operationWebHookModel = JsonConvert.DeserializeObject<OperationWebHookModel>(Encoding.UTF8.GetString(message.Body));
            if(operationWebHookModel != null)
            {
                var json = JsonConvert.SerializeObject(operationWebHookModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(operationWebHookModel.UrlWebHook, content);
                await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
            }
            else
            {
                await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
            }
        }
    }
}
