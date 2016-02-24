using CrudOfCarrier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOfCarrier.Models.Interfaces
{
    public interface ICarrierRatingRepository
    {
        Task RateCarrier(CarrierRating carrierRating);
        Task<CarrierRating> GetByCarrierAndUser(string carrierName, string userName);
        Task<IEnumerable<CarrierRating>> GetAll();
    }
}