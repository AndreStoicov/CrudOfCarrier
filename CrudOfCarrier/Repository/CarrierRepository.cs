using CrudOfCarrier.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudOfCarrier.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrudOfCarrier.Repository
{
    public class CarrierRepository : ICarrierRepository
    {
        public async Task<Carrier> AddCarrier(Carrier newCarrier)
        {
            using (var carriers = new HttpClient())
            {
                carriers.BaseAddress = new Uri("https://api.mongolab.com/");
                carriers.DefaultRequestHeaders.Accept.Clear();
                carriers.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var carrier = new Carrier() { Name = newCarrier.Name };
                HttpResponseMessage response = response = await carriers.PostAsJsonAsync("api/1/databases/appharbor_k82fwxcl/collections/Carrier?apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp", carrier);
                if (response.IsSuccessStatusCode)
                {
                    return await GetByName(newCarrier.Name);
                }
            }

            return null;
        }

        public async Task<IEnumerable<Carrier>> GetAll()
        {
            using (var carriers = new HttpClient())
            {
                carriers.BaseAddress = new Uri("https://api.mongolab.com/");
                carriers.DefaultRequestHeaders.Accept.Clear();
                carriers.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await carriers.GetAsync("api/1/databases/appharbor_k82fwxcl/collections/Carrier?apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp");
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Carrier> carrier = await response.Content.ReadAsAsync<IEnumerable<Carrier>>();
                    return carrier;
                }
            }
            return null;
        }

        public async Task<Carrier> GetByName(string carrierName)
        {
            using (var carriers = new HttpClient())
            {
                carriers.BaseAddress = new Uri("https://api.mongolab.com/");
                carriers.DefaultRequestHeaders.Accept.Clear();
                carriers.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = "api/1/databases/appharbor_k82fwxcl/collections/Carrier?q={\"Name\": \"" + carrierName + "\"}&apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp";
                HttpResponseMessage response = await carriers.GetAsync(apiUrl).ConfigureAwait(continueOnCapturedContext: false); ;
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Carrier> carrier = await response.Content.ReadAsAsync<IEnumerable<Carrier>>();
                    return carrier.FirstOrDefault();
                }
            }
            return null;
        }
    }
}