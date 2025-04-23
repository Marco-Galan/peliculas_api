using peliculas_api.entidades;

namespace peliculas_api
{
    public class RepositorioEnMemoria
    {
        private List<Genero> _generos;

        public RepositorioEnMemoria()
        {
            _generos = new List<Genero>

            {
                new Genero{Id = 1, Nombre = "Comedia"},
                new Genero{Id = 2, Nombre = "Accion"},
                new Genero{Id = 3, Nombre = "Animación"}
            };
        }

        public List<Genero> obtenerTodosLosDatos()
        {
            return _generos;
        }

        public async Task<Genero?> ObtenerPorId(int id)
        //public Genero? ObtenerPorId(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return _generos.FirstOrDefault(g => g.Id == id);
        }

        public bool Existe(string nombre)
        {
            return _generos.Any(g => g.Nombre == nombre);
        }


        // Los metodos void solo se pone Task y el await igual
        //private async Task Loguear()
        //{
        //    //logueo
        //}

    }
}
