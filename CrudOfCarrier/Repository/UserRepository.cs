using CrudOfCarrier.Models.Entities;
using CrudOfCarrier.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CrudOfCarrier.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetByName(string userName)
        {
            IEnumerable<User> currentUser = null;

            using (var user = new HttpClient())
            {
                user.BaseAddress = new Uri("https://api.mongolab.com/");
                user.DefaultRequestHeaders.Accept.Clear();
                user.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = "api/1/databases/appharbor_k82fwxcl/collections/Users?q={\"UserName\": \"" + userName + "\"}&apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp";
                HttpResponseMessage response = await user.GetAsync(apiUrl).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    currentUser = await response.Content.ReadAsAsync<IEnumerable<User>>();                    
                }
            }

            return currentUser.FirstOrDefault();
        }

        public async Task<bool> IsValid(string userName, string pass)
        {
            IEnumerable<User> users = null;

            using (var user = new HttpClient())
            {
                user.BaseAddress = new Uri("https://api.mongolab.com/");
                user.DefaultRequestHeaders.Accept.Clear();
                user.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var apiUrl = "api/1/databases/appharbor_k82fwxcl/collections/Users?q={\"UserName\": \"" + userName + "\", \"Password\": \"" + Helpers.SHA1.Encode(pass) + "\"}&apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp";
                HttpResponseMessage response = await user.GetAsync(apiUrl).ConfigureAwait(continueOnCapturedContext:false);
                if (response.IsSuccessStatusCode)
                {
                     users = await response.Content.ReadAsAsync<IEnumerable<User>>();                    
                }
            }

            return users.Any();
        }

        public async Task<User> Register(User user)
        {
            User registeredUser = null;
            using (var register = new HttpClient())
            {
                register.BaseAddress = new Uri("https://api.mongolab.com/");
                register.DefaultRequestHeaders.Accept.Clear();
                register.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                user.Password = Helpers.SHA1.Encode(user.Password);
                HttpResponseMessage response = response = await register.PostAsJsonAsync("api/1/databases/appharbor_k82fwxcl/collections/Users?apiKey=iQun6DS8l6aQ_QRZTVRxHu39z49ZnZUp", user);
                if (response.IsSuccessStatusCode)
                {
                    registeredUser = GetByName(user.UserName).Result;
                }
            }

            return registeredUser;
        }
    }
}