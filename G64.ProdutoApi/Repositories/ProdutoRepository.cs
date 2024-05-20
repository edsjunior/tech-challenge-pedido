using G64.ProdutoApi.Context;
using G64.ProdutoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace G64.ProdutoApi.Repositories
{
	public class ProdutoRepository : IProdutoRepository
	{
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context) {  
            _context = context; 
        }


        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }
        public async Task<IEnumerable<Produto>> GetProdutosIngredientes()
        {
            return await _context.Produtos.Include(c => c.Ingredientes).ToListAsync();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.Where(c => c.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task<Produto> Create(Produto produto)
		{
			_context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
		}

        public async Task<Produto> Update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Delete(Guid id)
		{
            var produto = await GetById(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
		}

	}
}
