using AutoMapper;
using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Models;
using G64.ProdutoApi.Repositories;

namespace G64.ProdutoApi.Services
{
	public class ComboService : IComboService
	{
        private readonly IComboRepository _comboRepository;
        private readonly IMapper _mapper;
        public ComboService(IComboRepository comboRepository, IMapper mapper)
        {
            _comboRepository = comboRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ComboDTO>> GetCombos()
        {
            var combosEntity = await _comboRepository.GetAll();
            return _mapper.Map<IEnumerable<ComboDTO>>(combosEntity);
        }

        public async Task<IEnumerable<ComboDTO>> GetComboProdutos()
        {
            var combosEntity = await _comboRepository.GetComboProdutos();
            return _mapper.Map<IEnumerable<ComboDTO>>(combosEntity);
        }

        public async Task<ComboDTO> GetComboById(Guid id)
        {
            var comboEntity = await _comboRepository.GetById(id);
            return _mapper.Map<ComboDTO>(comboEntity);
        }

        public async Task AddComboTO(ComboDTO comboDTO)
        {
            var comboEntity = _mapper.Map<Combo>(comboDTO);
            await _comboRepository.Create(comboEntity);
            comboDTO.Id = comboEntity.Id;


        }

        public async Task UpdateComboDTO(ComboDTO comboDTO)
        {
            var comboEntity = _mapper.Map<Combo>(comboDTO);
            await _comboRepository.Update(comboEntity);
        }

        public async Task RemoveComboDTO(Guid id)
        {
            var comboEntity = _comboRepository.GetById(id).Result;
            await _comboRepository.Delete(comboEntity.Id);
        }

	}
}
