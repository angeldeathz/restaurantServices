using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;
using RestaurantServices.Restaurant.Shared.Mail;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ReservaBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EstadoMesaBl _estadoMesaBl;
        private readonly EmailClient _emailClient;
        private readonly ClienteBl _clienteBl;

        public ReservaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _estadoMesaBl = new EstadoMesaBl();
            _emailClient = new EmailClient();
            _clienteBl = new ClienteBl();
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
                var estados = await _unitOfWork.EstadoReservaDal.GetByReservaAsync(x.Id);
                x.EstadosReserva = (List<EstadoReserva>)estados;
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
            var estados = await _unitOfWork.EstadoReservaDal.GetByReservaAsync(reservaResult.Id);
            reservaResult.EstadosReserva = (List<EstadoReserva>)estados;

            return reservaResult;
        }

        public async Task<int> GuardarAsync(Reserva reserva)
        {
            var mesa = await _unitOfWork.MesaDal.GetAsync(reserva.IdMesa);

            // Validación OK
            if (reserva.CantidadComensales > mesa.CantidadComensales)
                throw new Exception($"La mesa solo acepta {mesa.CantidadComensales} comensales");

            var reservas = await ObtenerTodosAsync();
            var reservaOcupada = false;

            foreach (var x in reservas)
            {
                if (x.IdMesa != reserva.IdMesa) continue;

                if (reserva.FechaReserva == x.FechaReserva)
                    reservaOcupada = true;

                if (reserva.FechaReserva < x.FechaReserva)
                {
                    if (reserva.FechaReserva >= x.FechaReserva.AddHours(-1))
                    {
                        reservaOcupada = true;
                    }
                }

                if (reserva.FechaReserva > x.FechaReserva)
                {
                    if (reserva.FechaReserva <= x.FechaReserva.AddHours(1))
                    {
                        reservaOcupada = true;
                    }
                }
            }

            if (reservaOcupada)
                throw new Exception($"Ya hay una reserva para la {mesa.NombreMesa}. Debe haber 1 hora de diferencia entre las reservas.");

            var idReserva = await _unitOfWork.ReservaDal.InsertAsync(reserva);

            if (idReserva > 0)
            {
                var cliente = await _clienteBl.ObtenerPorIdAsync(reserva.IdCliente);
                var cabeceraNombre = cliente.Persona.Nombre == null ? "Estimado Cliente:" : $"Estimado {cliente.Persona.Nombre} {cliente.Persona.Apellido}:";

                _emailClient.Send(new Email
                {
                    ReceptorEmail = cliente.Persona.Email,
                    ReceptorNombre = cabeceraNombre,
                    Asunto = $"Reserva {idReserva} creada",
                    Contenido =
                        $@"{cabeceraNombre} <br/><br/>
                           Gracias por reservar en Restaurante Siglo XXI, <br/><br/>
                           Su reserva es la N° {idReserva} <br/>
                           Cantidad Comensales: {reserva.CantidadComensales} <br/>
                           {mesa.NombreMesa} <br/>
                           Fecha de la Reserva: {reserva.FechaReserva:dd-MM-yyyy hh:mm} <br/><br/>

                           Atte, <br/>
                           Restaurante Siglo XXI. <br/>"
                });
            }

            return idReserva;
        }

        public async Task<int> ModificarAsync(Reserva reserva)
        {
            var mesa = await _unitOfWork.MesaDal.GetAsync(reserva.IdMesa);

            // Validación OK
            if (reserva.CantidadComensales > mesa.CantidadComensales)
                throw new Exception($"La mesa solo acepta {mesa.CantidadComensales} comensales");

            var reservas = await ObtenerTodosAsync();
            var reservaOcupada = false;

            foreach (var x in reservas)
            {
                if (x.IdMesa != reserva.IdMesa) continue;

                if (reserva.FechaReserva == x.FechaReserva)
                    reservaOcupada = true;

                if (reserva.FechaReserva < x.FechaReserva)
                {
                    if (reserva.FechaReserva >= x.FechaReserva.AddHours(-1))
                    {
                        reservaOcupada = true;
                    }
                }

                if (reserva.FechaReserva > x.FechaReserva)
                {
                    if (reserva.FechaReserva <= x.FechaReserva.AddHours(1))
                    {
                        reservaOcupada = true;
                    }
                }
            }

            if (reservaOcupada)
                throw new Exception($"Ya hay una reserva para la {mesa.NombreMesa}. Debe haber 1 hora de diferencia entre las reservas.");

            var modificado = await _unitOfWork.ReservaDal.UpdateAsync(reserva);

            var cliente = await _clienteBl.ObtenerPorIdAsync(reserva.IdCliente);
            var cabeceraNombre = cliente.Persona.Nombre == null ? "Estimado Cliente:" : $"Estimado {cliente.Persona.Nombre} {cliente.Persona.Apellido}:";
            var estadoReserva = await _unitOfWork.EstadoReservaDal.GetAsync(reserva.IdEstadoReserva);

            _emailClient.Send(new Email
            {
                ReceptorEmail = cliente.Persona.Email,
                ReceptorNombre = cabeceraNombre,
                Asunto = $"Reserva {reserva.Id} ha sido Modificada",
                Contenido =
                    $@"{cabeceraNombre} <br/><br/>
                           Su reserva N° {reserva.Id} ha sido modificada, <br/>
                           El estado de la reserva ahora es {estadoReserva.Nombre} <br/>
                           Cantidad Comensales: {reserva.CantidadComensales} <br/>
                           {mesa.NombreMesa} <br/>
                           Fecha de la Reserva: {reserva.FechaReserva:dd-MM-yyyy hh:mm} <br/><br/>

                           Atte, <br/>
                           Restaurante Siglo XXI. <br/>"
            });

            return modificado;
        }

        public async Task<int> AgregarEstadoAsync(ReservaEstado estado)
        {
            var idEstado = await _unitOfWork.ReservaDal.InsertEstadoAsync(estado);
            var estadoReserva = await _unitOfWork.EstadoReservaDal.GetAsync(estado.IdEstadoReserva);
            var reserva = await ObtenerPorIdAsync(estado.IdReserva);

            var cabeceraNombre = reserva.Cliente.Persona.Nombre == null ? "Estimado Cliente:" : $"Estimado {reserva.Cliente.Persona.Nombre} {reserva.Cliente.Persona.Apellido}:";

            _emailClient.Send(new Email
            {
                ReceptorEmail = reserva.Cliente.Persona.Email,
                ReceptorNombre = cabeceraNombre,
                Asunto = $"Reserva {estado.IdReserva} {estadoReserva.Nombre}",
                Contenido =
                    $@"{cabeceraNombre} <br/><br/>
                           Su reserva N° {estado.IdReserva} con fecha {reserva.FechaReserva:dd-MM-yyyy hh:mm},
                           cambió de estado a {estadoReserva.Nombre}. <br/><br/>

                           Atte, <br/>
                           Restaurante Siglo XXI. <br/>"
            });

            return idEstado;
        }
    }
}
