﻿using GoingTo_API.Domain.Models;
using GoingTo_API.Domain.Persistence.Context;
using GoingTo_API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoingTo_API.Persistence.Repositories
{
    public class PlaceRepository : BaseRepository, IPlaceRepository
    {
        public PlaceRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Place place)
        {
            await _context.Places.AddAsync(place);
        }
        public async Task<Place> FindById(int id)
        {
            return await _context.Places
                .Where(p => p.Id == id)
                .Include(p => p.Locatable)
                .Include(p => p.City.Locatable)
                .Include(p => p.City.Country.Locatable)
                .FirstAsync();
        }

        public async Task<IEnumerable<Place>> ListAsync()
        {
            return await _context.Places
                .Include(p=>p.Locatable)
                .Include(p=>p.City)
                .Include(p=>p.City.Locatable)//Descubrimiento
                .ToListAsync();
        }

      

        public void Remove(Place place)
        {
            _context.Places.Remove(place);
        }

        public void Update(Place place)
        {
            _context.Places.Update(place);
        }

        public async Task<IEnumerable<Place>> ListByCityIdAsync(int cityId) =>
            await _context.Places
                .Where(p => p.CityId == cityId)
                .Include(p=>p.Locatable)
                .Include(p => p.City)
                .ToListAsync();

        //Under development
        //public async Task AssignCityLocatable(int cityId, int locatableId,int id)
        //{
        //    Place place = await FindById(id);
        //    if(place != null)
        //    {
        //        place.LocatableId = locatableId;
        //        place.CityId = cityId;

        //        _context.Places.Update(place);
        //    }


        //}
       
    }
}
