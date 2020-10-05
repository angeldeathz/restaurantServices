using System;
using System.Collections.Generic;
using System.Linq;
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
            var usuarioCompleto = await _unitOfWork.UsuarioDal.GetAsync();
            return usuarioCompleto.Select(x => new Usuario
            {
                Id = x.IdUsuario,
                IdPersona = x.IdPersona,
                IdTipoUsuario = x.IdTipoUsuario,
                Persona = new Persona
                {
                    DigitoVerificador = x.DigitoVerificador,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Id = x.IdPersona,
                    Email = x.Email,
                    EsPersonaNatural = x.EsPersonaNatural,
                    Rut = x.Rut,
                    Telefono = x.Telefono
                },
                TipoUsuario = new TipoUsuario
                {
                    Id = x.IdTipoUsuario,
                    Nombre = x.NombreTipoUsuario
                }
            }).ToList();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            var usuarioCompleto = await _unitOfWork.UsuarioDal.GetAsync(id);
            if (usuarioCompleto == null) return null;
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
            if (usuarioCompleto == null) return null;

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

        public async Task<int> InsertarAsync(Usuario usuario)
        {
            var usuarioExistente = await this.ObtenerPorRutAsync(usuario.Persona.ObtenerRutCompleto());
            if (usuarioExistente !=  null) throw new Exception("Usuario ya existe");
            return await _unitOfWork.UsuarioDal.InsertAsync(usuario);
        }

        public Task<int> ActualizarAsync(Usuario usuario)
        {
            return _unitOfWork.UsuarioDal.UpdateAsync(usuario);
        }
    }
}
