﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeoGarden.Application.Interfaces;
using TeoGarden.Data.EF;
using TeoGarden.Data.Models;
using TeoGarden.ShareModel.Requests.Vegetable;
using TeoGarden.ShareModel.ViewModels;

namespace TeoGarden.Application.Services
{
    public class VegetableService : IVegetableService
    {
        public readonly TeoGardenDbContext _context;

        public VegetableService(TeoGardenDbContext context)
        {
            _context = context;
        }

        public async Task<List<VegetableViewModel>> GetAllAsync()
        {
            return await _context.Vegetables.Select(vegetable => new VegetableViewModel()
            {
                Id = vegetable.Id,
                Name = vegetable.Name,
                Price = vegetable.Price,
                Weight = vegetable.Weight,
                Image = vegetable.Image,
                Location = vegetable.Location,
                CategoryId = vegetable.CategoryId
            }).ToListAsync();
        }

        public async Task<VegetableViewModel> GetByIdAsync(int Id)
        {
            var vegetable = await _context.Vegetables.Where(vegetable => vegetable.Id==Id).FirstOrDefaultAsync();
            return new VegetableViewModel()
            {
                Id = vegetable.Id,
                Name = vegetable.Name,
                Price  = vegetable.Price,
                Weight = vegetable.Weight,
                Image = vegetable.Image,
                Sale = vegetable.Sale,
                Location = vegetable.Location,
                CategoryId = vegetable.CategoryId
            };
        }

        public async Task<int> CreateAsync(VegetableCreateRequest request)
        {
            var vegetable = new Vegetable()
            {
                Name = request.Name,
                Price = request.Price,
                Weight = request.Weight,
                Image = request.Image,
                Location = request.Location,
                CategoryId = request.CategoryId
            };
            await _context.Vegetables.AddAsync(vegetable);
            await _context.SaveChangesAsync();
            return vegetable.Id;
        }
        public async Task<int> UpdateAsync(VegetableUpdateRequest request)
        {
            var vegetable = await _context.Vegetables.FindAsync(request.Id);
            vegetable.Name = request.Name;
            vegetable.Price = request.Price;
            vegetable.Weight = request.Weight;
            vegetable.Location = request.Location;
            vegetable.CategoryId = request.CategoryId;
            return await _context.SaveChangesAsync();
        }
        //public async Task<int> DeleteAsync(int Id)
        //{
        //    var vegetable = await _context.Vegetables.Where(vegetable => vegetable.Id == Id);
        //    await _context.Vegetables.Remove(vegetable);
        //    return await _context.SaveChangesAsync();
        //}
    }
}