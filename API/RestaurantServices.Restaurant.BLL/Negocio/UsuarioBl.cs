﻿using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class UsuarioBl
    {
        private readonly UnitOfWork _unitOfWork;

        public UsuarioBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return (List<Usuario>) await _unitOfWork.UsuarioDal.GetAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.UsuarioDal.GetAsync(id);
        }

        public async Task<Usuario> ObtenerPorRutAsync(string rut)
        {
            var rutSinDv = 1;
            return await _unitOfWork.UsuarioDal.GetByRutAsync(rutSinDv);
        }

        public async Task<Usuario> ValidaLoginAsync(UsuarioLogin usuarioLogin)
        {
            // codificar/decodificar contrasena encriptada?
            return await _unitOfWork.UsuarioDal.ValidaLoginAsync(usuarioLogin.Rut, usuarioLogin.Contrasena);
        }
    }
}