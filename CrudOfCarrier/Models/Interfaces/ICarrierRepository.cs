using CrudOfCarrier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOfCarrier.Models.Interfaces
{
    public interface ICarrierRepository
    {
        Task<IEnumerable<Carrier>> GetAll();
        Task<Carrier> GetByName(string carrierName);
        Task<Carrier> AddCarrier(Carrier newCarrier);
    }
}