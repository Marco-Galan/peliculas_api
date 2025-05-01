namespace peliculas_api.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        public int resultadosPorPagina = 10;
        public readonly int maximoResultadoPorPagina = 50;
        public int ResultadosPorPagina
        {
            get { return resultadosPorPagina; }
            set { resultadosPorPagina = (value > maximoResultadoPorPagina) ? maximoResultadoPorPagina : value; }
        }

    }
}
