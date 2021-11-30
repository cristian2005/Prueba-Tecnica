using Newtonsoft.Json;
using Prueba_Tecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Prueba_Tecnica.Controllers
{
    public class BooksController : ApiController
    {
    
        private const string BaseUrl = "https://fakerestapi.azurewebsites.net/";

        public async Task<HttpResponseMessage> Get()
        {
            List<Books> BookInfo = new List<Books>();

            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    HttpResponseMessage Res = await client.GetAsync("api/v1/Books");
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                       
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        
                        BookInfo = JsonConvert.DeserializeObject<List<Books>>(EmpResponse);
                    }
                    else
                    {
                        Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ocurrio un error en obtener los libros");
                    }

                    return Request.CreateResponse<List<Books>>(HttpStatusCode.OK, BookInfo);
                }
            }
            catch (Exception ex)
            {
               return  Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);  
            }
        }
        public async Task<HttpResponseMessage> Get(int id)
        {
            Books BookInfo = new Books();

            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("api/v1/Books/"+id);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {

                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        BookInfo = JsonConvert.DeserializeObject<Books>(EmpResponse);
                    }
                    else
                    {
                        Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ocurrio un error al encontrar el libro "+id);
                    }

                    return Request.CreateResponse<Books>(HttpStatusCode.OK, BookInfo);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public async Task<HttpStatusCode> Post(Books books)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.PostAsJsonAsync("api/v1/Books/",books);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.StatusCode;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }

                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        public async Task<HttpStatusCode> Put(int id, Books books)
        {
            Books BookInfo = new Books();

            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("api/v1/Books/" + id);

                    //Checking the response is successful or not which is sent using HttpClient
                    if (!Res.IsSuccessStatusCode)
                    {
                        return HttpStatusCode.NotFound;
                    }
                   
                    Res = await client.PutAsJsonAsync("api/v1/Books/"+id, books);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.StatusCode;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }

                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        public async Task<HttpStatusCode> Delete(int id)
        {
            
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("api/v1/Books/" + id);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (!Res.IsSuccessStatusCode)
                    {
                        return HttpStatusCode.NotFound;


                    }
                    else
                    {
                      await  client.DeleteAsync("api/v1/Books/" + id);
                      return Res.StatusCode;
                    }

                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
