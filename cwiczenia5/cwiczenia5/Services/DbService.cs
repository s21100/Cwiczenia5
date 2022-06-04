using cwiczenia5.Models;
using cwiczenia5.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia5.Services
{
    public class DbService : IDbService
    {
        private readonly MasterContext _dbContext;
        public DbService(MasterContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Select(e => new SomeSortOfTrip 
            { 
                Name = e.Name,
                Description = e.Description,
                MaxPeople = e.MaxPeople,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                Countries = e.CountryTrips.Select(e => new SomeSortOfCountry {Name = e.IdCountryNavigation.Name}).ToList(),
                Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName}).ToList()
            }).OrderByDescending(e => e.DateFrom).ToListAsync();
        }

        public async Task<bool> RegisterClientForTrip(SomeSortOfRegistration form)
        {
            var client = await _dbContext.Clients.Select(e => e).Where(e => e.Pesel.Equals(form.Pesel)).FirstOrDefaultAsync();

            if (client == null) {
                var newClient = new Client {FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, Telephone = form.Telephone, Pesel = form.Pesel};
                _dbContext.Add(newClient);
                await _dbContext.SaveChangesAsync();
            }

            var trip = await _dbContext.Trips.Select(e => e).Where(e => e.IdTrip == form.IdTrip).FirstOrDefaultAsync();

            if (trip == null) {
                return false;
            }

            var clientTrip = await _dbContext.ClientTrips.Select(e => e).Where(e => e.IdClient == client.IdClient && e.IdTrip == form.IdTrip).FirstOrDefaultAsync();

            if (clientTrip != null) {
                return false;
            }

            var newClientTrip = new ClientTrip { 
                IdClient = client.IdClient, 
                IdTrip = form.IdTrip, 
                RegisteredAt = DateTime.Now, 
                PaymentDate = DateTime.ParseExact(form.PaymentDate, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None) 
            };
            _dbContext.Add(newClientTrip);
            await _dbContext.SaveChangesAsync();

            return true;
        
        }

        public async Task<bool> RemoveClient(int id)
        {
            var client = await _dbContext.Clients.Select(e => e).Where(e => e.IdClient == id).FirstOrDefaultAsync();

            if (client == null) {
                return false;
            }

            var trips = await _dbContext.ClientTrips
                .Select(e => e)
                .Where(e => e.IdClient == id).ToListAsync();

            if (trips.Count > 0) {
                return false;
            }

            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
