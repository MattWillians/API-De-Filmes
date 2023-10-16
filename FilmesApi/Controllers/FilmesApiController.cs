using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesApiController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _imapper;


    public FilmesApiController(FilmeContext context, IMapper mapper)
    {
       _context = context;
        _imapper = mapper;
    }


    /// <summary>
    /// Método usado para adicionar um novo filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto JSON que representa um filme, vindo diretamente do Body HTTP</param>
    /// <returns>Retorna o filme criado</returns>
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {

        Filme filme = _imapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarFilmePorID), new { id = filme.Id}, filme);
    }

    /// <summary>
    /// Método responsavel para recuperar todos os filmes da lista de filmes dentro do banco de dados.
    /// </summary>
    /// <param name="skip">Serve para pular posições, valor default caso não preencha.</param>
    /// <param name="take">Serve para dizer quantas posições ler, valor default caso não preencha.</param>
    /// <returns>Retorna uma lista de filmes</returns>
    [HttpGet]
    public IActionResult RecuperaFilmes([FromQuery] int skip = 0,
            [FromQuery] int take = 50)
    {
        return Ok(JsonSerializer.Serialize(_context.Filmes.Take(take).Skip(skip).ToArray()));
    }

    /// <summary>
    /// Busca um filme baseado na sua chave de acesso unica
    /// </summary>
    /// <param name="id">Identificador unico do filme</param>
    /// <returns>Retorna um filme caso exista.</returns>
    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorID(int id)
    {
       if(_context.Filmes.Count() > 0)
        {
            return Ok(JsonSerializer.Serialize(_context.Filmes.FirstOrDefault(fm => fm.Id == id)));
        }

        return NotFound("Não Temos Filmes Cadastrados");
    }

    /// <summary>
    /// Atualiza TODAS as informações de um filme, baseado no seu ID unico 
    /// </summary>
    /// <param name="id">Id unico do filme</param>
    /// <param name="newInfoFilme">Novas informações do filme, deve-se passar todos os parametros</param>
    /// <returns>Não há retornos</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto newInfoFilme)
    {
        Filme filme = _context.Filmes.FirstOrDefault(fm => fm.Id == id);
        
        if (filme == null)
        {
            return NotFound("Filme não encontrado");
        }

        _imapper.Map(newInfoFilme, filme);
        _context.SaveChanges();
        return NoContent();

    }

    /// <summary>
    /// Atualiza UMA INFORMAÇÃO ou mais do filme em questão
    /// </summary>
    /// <param name="id">identificador unico do filme que estamos ajusta</param>
    /// <param name="patchFilme">Campos que vamos atualizar</param>
    /// <returns>Não há retornos</returns>
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcialmente(int id, JsonPatchDocument<UpdateFilmeDto> patchFilme)
    {
        Filme filme = _context.Filmes.FirstOrDefault(fm => fm.Id == id);

        if (filme == null)
        {
            return NotFound("Filme não encontrado");
        }

        UpdateFilmeDto filmeVerificado = _imapper.Map<UpdateFilmeDto>(filme);

        patchFilme.ApplyTo(filmeVerificado, ModelState);

        if (!TryValidateModel(filmeVerificado))
        {
            return ValidationProblem();
        }

        _imapper.Map(filmeVerificado, filme);
        _context.SaveChanges();
        return NoContent();

    }

    /// <summary>
    /// Deleta um filme baseado no seu ID
    /// </summary>
    /// <param name="id">Identificador unico do filme</param>
    /// <returns>Não há retornos</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        Filme filme = _context.Filmes.FirstOrDefault(fm => fm.Id == id);

        if (filme == null)
        {
            return NotFound("Filme não encontrado");
        }

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

}
