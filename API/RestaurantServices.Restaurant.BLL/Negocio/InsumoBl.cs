﻿using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class InsumoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public InsumoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Insumo>> ObtenerTodosAsync()
        {
            return (List<Insumo>)await _unitOfWork.InsumoDal.GetAsync();
        }

        public async Task<Insumo> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.InsumoDal.GetAsync(id);
        }

        public async Task<int> GuardarAsync(Insumo insumo)
        {
            return await _unitOfWork.InsumoDal.InsertAsync(insumo);
        }

        public async Task<bool> ModificarAsync(Insumo insumo)
        {
            return await _unitOfWork.InsumoDal.UpdateAsync(insumo);
        }
    }
}