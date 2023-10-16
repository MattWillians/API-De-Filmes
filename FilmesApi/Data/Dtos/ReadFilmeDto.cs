using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class ReadFilmeDto //data transfer object
{
    public string Titulo { get; set; }
    public int Duracao { get; set; }
    public string Genero { get; set; }
    public DateTime DataConsulta { get; set; } = DateTime.Now;   
}
