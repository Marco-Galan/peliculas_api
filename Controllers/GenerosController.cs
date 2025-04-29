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
        private readonly ServicioTrasient trasient1;
        private readonly ServicioTrasient trasient2;
        private readonly ServicioScoped scoped1;
        private readonly ServicioScoped scoped2;
        private readonly ServicioSingleton singleton;

        public GenerosController(IRepositorio repositorio,
            ServicioTrasient trasient1,
            ServicioTrasient trasient2,
            ServicioScoped scoped1,
            ServicioScoped scoped2,
            ServicioSingleton singleton
            )
        {
            this.repositorio = repositorio;
            this.trasient1 = trasient1;
            this.trasient2 = trasient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton = singleton;
        }

        [HttpGet("ServiciosTiemposVida")]
        public IActionResult ObtenerTiemposVidaServicios()
        {
            return Ok(new
            {

                Trasients = new
                {
                    Trasient1 = trasient1.ObtenerId,
                    Trasient2 = trasient2.ObtenerId
                },
                Scopeds = new
                {
                    Scoped1 = scoped1.ObtenerId,
                    Scoped2 = scoped2.ObtenerId
                },
                Singleton = singleton.ObtenerId
            });
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
