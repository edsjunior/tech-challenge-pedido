using AutoMapper;
using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Models;
using G64.ProdutoApi.Repositories;

namespace G64.ProdutoApi.Services
{
	public class ProdutoService : IProdutoService
	{
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;

        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produtosEntity = await _produtoRepository.GetAll();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtosEntity);

        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosIngredientes()
        {
            var produtosEntity = await _produtoRepository.GetProdutosIngredientes();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtosEntity);
        }

        public async Task<ProdutoDTO> GetProdutoById(Guid id)
        {
            var produtoEntity = await _produtoRepository.GetById(id);
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        public async Task AddProdutoDTO(ProdutoDTO produtoDTO)
		{
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepository.Create(produtoEntity);
            produtoDTO.Id = produtoEntity.Id;


		}
		
		public async Task UpdateProdutoDTO(ProdutoDTO produtoDTO)
		{
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepository.Update(produtoEntity);
        }

        public async Task RemoveProduto(Guid id)
        {
            var produtoEntity = _produtoRepository.GetById(id).Result;
            await _produtoRepository.Delete(produtoEntity.Id);
        }

    }
}
