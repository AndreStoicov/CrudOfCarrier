using CrudOfCarrier.Models.Entities;
using CrudOfCarrier.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading.Tasks;

namespace CrudOfCarrier.Repository
{
    public class CarrierRatingRepository : ICarrierRatingRepository
    {
        public async Task<IEnumerable<CarrierRating>> GetAll()
        {
            IEnumerable<CarrierRating> carrierRated = null;

            using (var rating = new HttpClient())
            {
                rating.BaseAddress = new Uri("https://api.mongolab.com/");
                rating.DefaultRequestHeaders.Accept.Clear();
                rating.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await rating.GetAsync("api/1/databases/appharbor_k82fwxcl/collections/RatedCarrier?apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp");
                if (response.IsSuccessStatusCode)
                {
                    carrierRated = await response.Content.ReadAsAsync<IEnumerable<CarrierRating>>();
                }
            }

            return carrierRated;
        }

        public async Task<CarrierRating> GetByCarrierAndUser(string carrierName, string userName)
        {
            IEnumerable<CarrierRating> carrierRated = null;

            using (var carriers = new HttpClient())
            {
                carriers.BaseAddress = new Uri("https://api.mongolab.com/");
                carriers.DefaultRequestHeaders.Accept.Clear();
                carriers.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = "api/1/databases/appharbor_k82fwxcl/collections/RatedCarrier?q={\"Carrier.Name\": \"" + carrierName + "\", \"User.UserName\": \"" + userName + "\"}&apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp";
                HttpResponseMessage response = await carriers.GetAsync(apiUrl).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    carrierRated = await response.Content.ReadAsAsync<IEnumerable<CarrierRating>>();                    
                }
            }

            return carrierRated.FirstOrDefault();
        }

        public async Task RateCarrier(CarrierRating carrierRating)
        {
            if (GetByCarrierAndUser(carrierRating.Carrier.Name, carrierRating.User.UserName).Result != null)
            {
                throw new Exception("This carrier already has been rated by you, please select another carrier.");
            }

            using (var rating = new HttpClient())
            {
                rating.BaseAddress = new Uri("https://api.mongolab.com/");
                rating.DefaultRequestHeaders.Accept.Clear();
                rating.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = response = await rating.PostAsJsonAsync("api/1/databases/appharbor_k82fwxcl/collections/RatedCarrier?apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp", carrierRating);

            }
        }
    }
}