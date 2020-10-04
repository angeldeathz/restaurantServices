using System;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private readonly UsuarioBl _usuarioBl;

        public UsuariosController()
        {
            _usuarioBl = new UsuarioBl();
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> ValidarSesion([FromBody] UsuarioLogin usuarioLogin)
        {
            var usuario = await _usuarioBl.ValidaLoginAsync(usuarioLogin);

            if (usuario == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(usuario);
        }

        [Authorize, HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var usuarios = await _usuarioBl.ObtenerTodosAsync();

            if (usuarios.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(usuarios);
        }

        [Authorize, HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var usuario = await _usuarioBl.ObtenerPorIdAsync(id);

            if (usuario == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(usuario);
        }

        [Authorize, HttpGet, Route("")]
        public async Task<IHttpActionResult> Get([FromUri] string rut)
        {
            var usuario = await _usuarioBl.ObtenerPorRutAsync(rut);

            if (usuario == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(usuario);
        }

        [Authorize, HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Usuario usuario)
        {
            var idUsuario = await _usuarioBl.InsertarAsync(usuario);

            if (idUsuario == 0) throw new Exception("No se pudo crear el usuario");
            return Ok(idUsuario);
        }

        [Authorize, HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Usuario usuario, int id)
        {
            if (id == 0) throw new Exception("El id del usuario debe ser mayor a cero");
            usuario.Id = id;
            var esActualizado = await _usuarioBl.ActualizarAsync(usuario);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el usuario");
            return Ok(esActualizado);
        }
    }
}
