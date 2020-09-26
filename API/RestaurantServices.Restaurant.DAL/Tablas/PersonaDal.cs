﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PersonaDal
    {
        private readonly IRepository _repository;

        public PersonaDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Persona>> GetAsync()
        {
            const string query = "";

            return await _repository.GetListAsync<Persona>(query);
        }

        public async Task<Persona> GetAsync(int id)
        {
            const string query = "";

            return await _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<Persona> GetByRutAsync(int rut)
        {
            const string query = "";

            return await _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public async Task<int> InsertAsync(Persona persona)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(query, new Dictionary<string, object>
            {
                {"@rut", persona.Rut},
                {"@digito_verificador", persona.DigitoVerificador},
                {"@nombre", persona.Nombre},
                {"@apellido", persona.Apellido},
                {"@email", persona.Email},
                {"@telefono", persona.Telefono},
                {"@persona_natural", persona.EsPersonaNatural},
            }, CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(query, new Dictionary<string, object>
            {
                {"@id", persona.Id},
                {"@rut", persona.Rut},
                {"@digito_verificador", persona.DigitoVerificador},
                {"@nombre", persona.Nombre},
                {"@apellido", persona.Apellido},
                {"@email", persona.Email},
                {"@telefono", persona.Telefono},
                {"@persona_natural", persona.EsPersonaNatural},
            }, CommandType.StoredProcedure);
        }
    }
}
