using cwiczenia5.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia5.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        Task<bool> RemoveClient(int id);
        Task<bool> RegisterClientForTrip(SomeSortOfRegistration registration);
    }
}
