using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ReservaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public ReservaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Reserva>> ObtenerTodosAsync()
        {
            var reservas = await _unitOfWork.ReservaDal.GetAsync();
            return reservas.Select(reserva => new Reserva
            {
                CantidadComensales = reserva.CantidadComensalesReserva,
                FechaReserva = reserva.FechaReserva,
                IdCliente = reserva.IdCliente,
                Id = reserva.IdReserva,
                IdMesa = reserva.IdMesa,
                Mesa = new Mesa
                {
                    Id = reserva.IdMesa,
                    IdEstadoMesa = reserva.IdEstadoMesa,
                    Nombre = reserva.NombreMesa,
                    CantidadComensales = reserva.CantidadComensalesMesa
                },
                Cliente = new Cliente
                {
                    IdPersona = reserva.IdPersona,
                    Id = reserva.IdCliente,
                    Persona = new Persona
                    {
                        Nombre = reserva.Nombre,
                        Apellido = reserva.Apellido,
                        DigitoVerificador = reserva.DigitoVerificador,
                        Email = reserva.Email,
                        Telefono = reserva.Telefono,
                        EsPersonaNatural = reserva.EsPersonaNatural,
                        Id = reserva.IdPersona,
                        Rut = reserva.Rut
                    }
                }
            }).ToList();
        }

        public async Task<Reserva> ObtenerPorIdAsync(int id)
        {
            var reserva = await _unitOfWork.ReservaDal.GetAsync(id);
            if (reserva == null) return null;
            return new Reserva
            {
                CantidadComensales = reserva.CantidadComensalesReserva,
                FechaReserva = reserva.FechaReserva,
                IdCliente = reserva.IdCliente,
                Id = reserva.IdReserva,
                IdMesa = reserva.IdMesa,
                Mesa = new Mesa
                {
                    Id = reserva.IdMesa,
                    IdEstadoMesa = reserva.IdEstadoMesa,
                    Nombre = reserva.NombreMesa,
                    CantidadComensales = reserva.CantidadComensalesMesa
                },
                Cliente = new Cliente
                {
                    IdPersona = reserva.IdPersona,
                    Id = reserva.IdCliente,
                    Persona = new Persona
                    {
                        Nombre = reserva.Nombre,
                        Apellido = reserva.Apellido,
                        DigitoVerificador = reserva.DigitoVerificador,
                        Email = reserva.Email,
                        Telefono = reserva.Telefono,
                        EsPersonaNatural = reserva.EsPersonaNatural,
                        Id = reserva.IdPersona,
                        Rut = reserva.Rut
                    }
                }
            };
        }

        public async Task<int> GuardarAsync(Reserva reserva)
        {
            return await _unitOfWork.ReservaDal.InsertAsync(reserva);
        }

        public async Task<bool> ModificarAsync(Reserva reserva)
        {
            return await _unitOfWork.ReservaDal.UpdateAsync(reserva);
        }
    }
}
