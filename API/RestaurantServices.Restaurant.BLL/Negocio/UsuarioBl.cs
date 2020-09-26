using System;
using System.Collections.Generic;
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
            var a = await _unitOfWork.UsuarioDal.GetAsync();
            return (List<Usuario>) await _unitOfWork.UsuarioDal.GetAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            var usuarioCompleto = await _unitOfWork.UsuarioDal.GetAsync(id);
            return new Usuario
            {
                Id = usuarioCompleto.IdUsuario,
                IdPersona = usuarioCompleto.IdPersona,
                IdTipoUsuario = usuarioCompleto.IdTipoUsuario,
                Persona = new Persona
                {
                    DigitoVerificador = usuarioCompleto.DigitoVerificador,
                    Nombre = usuarioCompleto.Nombre,
                    Apellido = usuarioCompleto.Apellido,
                    Id = usuarioCompleto.IdPersona,
                    Email = usuarioCompleto.Email,
                    EsPersonaNatural = usuarioCompleto.EsPersonaNatural,
                    Rut = usuarioCompleto.Rut,
                    Telefono = usuarioCompleto.Telefono
                },
                TipoUsuario = new TipoUsuario
                {
                    Id = usuarioCompleto.IdTipoUsuario,
                    Nombre = usuarioCompleto.NombreTipoUsuario
                }
            };
        }

        public async Task<Usuario> ObtenerPorRutAsync(string rut)
        {
            var personaHelper = new Persona();
            if (!personaHelper.ValidaRut(rut))
            {
                throw new Exception("Rut es inválido");
            }

            var usuarioCompleto = await _unitOfWork.UsuarioDal.GetByRutAsync(personaHelper.Rut);
            return new Usuario
            {
                Id = usuarioCompleto.IdUsuario,
                IdPersona = usuarioCompleto.IdPersona,
                IdTipoUsuario = usuarioCompleto.IdTipoUsuario,
                Persona = new Persona
                {
                    DigitoVerificador = usuarioCompleto.DigitoVerificador,
                    Nombre = usuarioCompleto.Nombre,
                    Apellido = usuarioCompleto.Apellido,
                    Id = usuarioCompleto.IdPersona,
                    Email = usuarioCompleto.Email,
                    EsPersonaNatural = usuarioCompleto.EsPersonaNatural,
                    Rut = usuarioCompleto.Rut,
                    Telefono = usuarioCompleto.Telefono
                },
                TipoUsuario = new TipoUsuario
                {
                    Id = usuarioCompleto.IdTipoUsuario,
                    Nombre = usuarioCompleto.NombreTipoUsuario
                }
            };
        }

        public async Task<Usuario> ValidaLoginAsync(UsuarioLogin usuarioLogin)
        {
            // codificar/decodificar contrasena encriptada?
            var personaHelper = new Persona();
            if (!personaHelper.ValidaRut(usuarioLogin.Rut))
            {
                return null;
            }

            return await _unitOfWork.UsuarioDal.ValidaLoginAsync(personaHelper.Rut, usuarioLogin.Contrasena);
        }
    }
}
