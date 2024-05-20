using AutoMapper;
using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Models;
using G64.ProdutoApi.Repositories;

namespace G64.ProdutoApi.Services;

public class IngredienteService : IIngredienteService
{
    private readonly IIngredienteRepository _ingredienteRepository;
    private readonly IMapper _mapper;

    public IngredienteService(IIngredienteRepository ingredienteRepository, IMapper mapper)
    {
        _ingredienteRepository = ingredienteRepository;
        _mapper = mapper;

    }

    public async Task<IEnumerable<IngredienteDTO>> GetIngredientes()
    {
        var ingredienteEntity = await _ingredienteRepository.GetAll();
        return _mapper.Map<IEnumerable<IngredienteDTO>>(ingredienteEntity);
    }

    public async Task<IngredienteDTO> GetIngredienteById(Guid id)
    {
        var ingredienteEntity = await _ingredienteRepository.GetById(id);
        return _mapper.Map<IngredienteDTO>(ingredienteEntity);
    }

    public async Task AddIngredienteDTO(IngredienteDTO produtoDTO)
    {
        var ingredienteEntity = _mapper.Map<Ingrediente>(produtoDTO);
        await _ingredienteRepository.Create(ingredienteEntity);
        produtoDTO.Id = ingredienteEntity.Id;


    }

    public async Task UpdateIngredienteDTO(IngredienteDTO produtoDTO)
    {
        var ingredienteEntity = _mapper.Map<Ingrediente>(produtoDTO);
        await _ingredienteRepository.Update(ingredienteEntity);
    }

    public async Task RemoveIngrediente(Guid id)
    {
        var ingredienteEntity = _ingredienteRepository.GetById(id).Result;
        await _ingredienteRepository.Delete(ingredienteEntity.Id);
    }
}
