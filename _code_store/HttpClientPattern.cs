//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Primitives;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace _code_store
//{
//    public interface IHttpLexClient
//    {
//        string HttpBaseAddress { get; }
//        string HttpClientName { get; }
//        string SecretKey { get; }
//        string SecretValue { get; }
//    }

//    public abstract class HttpClientPattern
//    {
//        private readonly HttpClient client;
//        private readonly ILogger logger;

//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
//        protected HttpClientPattern(IHttpContextAccessor contextAccessor, IHttpClientFactory httpClient, ILogger logger, IHttpLexClient lexClient)
//        {
//            try
//            {
//                this.logger = logger;

//                client = httpClient.CreateClient(lexClient.HttpClientName);
//                client.BaseAddress = new Uri(lexClient.HttpBaseAddress);

//                this.client.DefaultRequestHeaders.Add(lexClient.SecretKey, lexClient.SecretValue);

//                if (contextAccessor is null || contextAccessor.HttpContext is null)
//                {
//                    logger.LogWarning("No HttpContextAccessor or HttpContext for Client {httpClientName}.", lexClient.HttpClientName);
//                    return;
//                }

//                if (contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
//                {
//                    var token = headerValue.ToString().Substring("Bearer ".Length);
//                    this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//                    logger.LogInformation("HttpContextAccessor started. Token added in header of client {httpClientName}.", lexClient.HttpClientName);
//                }
//                else
//                {
//                    logger.LogWarning("The 'Authorization' not found in header for client {httpClientName}.", lexClient.HttpClientName);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, "[ HttpClientPattern ] [ IHttpLexClient: {@lexClient} ] ", lexClient);
//            }

//        }


//        protected async Task<HttpResponseMessage> PostAsync(string requestUri, object bodyData = null, Dictionary<string, string> headers = null)
//        {
//            try
//            {
//                using (var xpto = new HttpRequestMessage(HttpMethod.Post, requestUri))
//                {

//                }


//                ///[FromHeader]
//                if (headers != null)
//                {
//                    foreach (var header in headers)
//                    {
//                        IEnumerable<string> values = Enumerable.Empty<string>();

//                        if (client.DefaultRequestHeaders.TryGetValues(header.Key, out values))
//                        {
//                            values = values.Contains(header.Value) ? values : values.Append(header.Value);
//                            client.DefaultRequestHeaders.Remove(header.Key);
//                        }
//                        else
//                        {
//                            values = new[] { header.Value };
//                        }

//                        client.DefaultRequestHeaders.Add(header.Key, values);
//                    }
//                }

//                ///[FromBody]
//                HttpContent content = bodyData is null ? null : new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");

//                var response = await client.PostAsync(requestUri, content);
//                return response.EnsureSuccessStatusCode();
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, "[ PostAsync ] [ Uri: {Uri} ] [ Body: {@body} ] [ Headers: {headers} ]", requestUri, bodyData, headers);
//                throw;
//            }
//        }

//        protected async Task<TEntity> GetAsync<TEntity>(string requestUri, Dictionary<string, string> headers = null)
//        {
//            try
//            {
//                if (headers != null)
//                {
//                    foreach (var header in headers)
//                    {
//                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
//                    }
//                }

//                var response = await client.GetAsync(requestUri);
//                response.EnsureSuccessStatusCode();

//                return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, "[ PostOfficeHttpFactory: GetEntityAsync ] [ Uri: {Uri} ] [ {GetType} ] ", requestUri, ex.GetType().FullName);

//                throw;
//            }
//        }

//        protected async Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> headers = null)
//        {
//            try
//            {
//                if (headers != null)
//                {
//                    foreach (var header in headers)
//                    {
//                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
//                    }
//                }

//                var response = await client.GetAsync(requestUri);
//                response.EnsureSuccessStatusCode();
//                return response;
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, "[ PostOfficeHttpFactory: GetEntityAsync ] [ Uri: {Uri} ] [ {GetType} ] ",
//                requestUri,
//                ex.GetType().FullName);

//                throw;
//            }
//        }
//    }
//}
