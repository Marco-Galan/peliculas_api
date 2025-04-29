using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using peliculas_api.entidades;

namespace peliculas_api.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase // :ControllerBase -> Acceso a controlres auxiliares
    {   //Acopalmiento fuerte
        // Configuracion de dependencia
        private readonly IRepositorio repositorio;
        public GenerosController(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet] //api/generos
        [HttpGet("listado")] //url-> api/generos/listado
        [HttpGet("/listado/generos")] //Url-> /listado/generos
        [OutputCache]
        public List<Genero> Get()
        {
            var generos = repositorio.ObtenerTodosLosDatos();
            return generos;
        }

        [HttpGet("{id:int}")] // api/generos/500
        [OutputCache] // //Se agrega cache 1.3
        public async Task<ActionResult<Genero>> Get(int id)
        //Fucnion ActionResult<Genero> : Clase base que define el resultado que una acción en un controlador puede devolver
        {
            var genero = await repositorio.ObtenerPorId(id);

            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpGet("{nombre}")] // api/generos/{nombre}
        public async Task<Genero?> Get(string nombre)
        {
            var genero = await repositorio.ObtenerPorId(1);
            return genero;
        }

        [HttpPost()]
        //IActionResult para retornar valores booleanos
        public IActionResult Post([FromBody] Genero genero)
        {
            var generoExistente = repositorio.Existe(genero.Nombre);

            if (generoExistente)
            {
                return BadRequest($"El genero {genero.Nombre} ya esta registrado");
            }

            return Ok();
            //var g = new Genero() { Nombre = "Drama" };


        }
        [HttpPut]
        public void Put()
        {

        }

        [HttpDelete]
        public void Delete()
        {

        }
    }
}
