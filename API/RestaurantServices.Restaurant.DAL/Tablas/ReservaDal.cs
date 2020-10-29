using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ReservaDal
    {
        private readonly IRepository _repository;

        public ReservaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ReservaJoin>> GetAsync()
        {
            const string query = @"SELECT
                    r.id as idReserva,
                    r.fecha_hora as fechaReserva,
                    r.cantidad_comensales as cantidadComensalesReserva,
                    r.cliente_id as idCliente,
                    r.mesa_id as idMesa,
                    c.persona_id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as esPersonaNatural,
                    m.nombre as nombreMesa,
                    m.cantidad_comensales as cantidadComensalesMesa,
                    m.estado_mesa_id as idEstadoMesa
                FROM RESERVA r
                JOIN CLIENTE c on r.cliente_id = c.id
                JOIN PERSONA p on c.persona_id = p.id
                JOIN MESA m on m.id = r.mesa_id";

            return _repository.GetListAsync<ReservaJoin>(query);
        }

        public Task<ReservaJoin> GetAsync(int id)
        {
            const string query = @"SELECT
                    r.id as idReserva,
                    r.fecha_hora as fechaReserva,
                    r.cantidad_comensales as cantidadComensalesReserva,
                    r.cliente_id as idCliente,
                    r.mesa_id as idMesa,
                    c.persona_id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as esPersonaNatural,
                    m.nombre as nombreMesa,
                    m.cantidad_comensales as cantidadComensalesMesa,
                    m.estado_mesa_id as idEstadoMesa
                FROM RESERVA r
                JOIN CLIENTE c on r.cliente_id = c.id
                JOIN PERSONA p on c.persona_id = p.id
                JOIN MESA m on m.id = r.mesa_id
                where r.id = :id";

            return _repository.GetAsync<ReservaJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Reserva reserva)
        {
            const string spName = "sp_insertReserva";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_fechaHora", reserva.FechaReserva},
                {"@p_cantidad_comensales", reserva.CantidadComensales},
                {"@p_clienteId", reserva.IdCliente},
                {"@p_mesaId", reserva.IdMesa},
                {"@p_estado_reserva_id", reserva.IdEstadoReserva},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Reserva reserva)
        {
            const string spName = "sp_updateReserva";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", reserva.Id},
                {"@p_fechaHora", reserva.FechaReserva},
                {"@p_cantidad_comensales", reserva.CantidadComensales},
                {"@p_clienteId", reserva.IdCliente},
                {"@p_mesaId", reserva.IdMesa},
                {"@p_estado_reserva_id", reserva.IdEstadoReserva},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> InsertEstadoAsync(ReservaEstado estado)
        {
            const string spName =
                @"insert into cambio_estado_reserva (estado_reserva_id, reserva_id, fecha)
                  values (:EstadoReservaId, :ReservaId, :Fecha)";

            return _repository.InsertAsync(spName, new Dictionary<string, object>
            {
                {"@EstadoReservaId", estado.IdEstadoReserva},
                {"@ReservaId", estado.IdReserva},
                {"@Fecha", DateTime.Now}
            });
        }
    }
}