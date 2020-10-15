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
        private readonly EstadoMesaBl _estadoMesaBl;

        public ReservaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _estadoMesaBl = new EstadoMesaBl();
        }

        public async Task<List<Reserva>> ObtenerTodosAsync()
        {
            var reservas = await _unitOfWork.ReservaDal.GetAsync();
            var reservasList = reservas.Select(reserva => new Reserva
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

            foreach (var x in reservasList)
            {
                x.Mesa.EstadoMesa = await _estadoMesaBl.ObtenerPorIdAsync(x.Mesa.IdEstadoMesa);
            }

            return reservasList;
        }

        public async Task<Reserva> ObtenerPorIdAsync(int id)
        {
            var reserva = await _unitOfWork.ReservaDal.GetAsync(id);
            if (reserva == null) return null;
            var reservaResult = new Reserva
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

            reservaResult.Mesa.EstadoMesa = await _estadoMesaBl.ObtenerPorIdAsync(reservaResult.Mesa.IdEstadoMesa);
            return reservaResult;
        }

        public Task<int> GuardarAsync(Reserva reserva)
        {
            return _unitOfWork.ReservaDal.InsertAsync(reserva);
        }

        public Task<int> ModificarAsync(Reserva reserva)
        {
            return _unitOfWork.ReservaDal.UpdateAsync(reserva);
        }
    }
}
