
using G64.ProdutoApi.Context;
using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace G64.ProdutoApi.Repositories
{
	public class ComboRepository : IComboRepository
	{
        private readonly AppDbContext _context;
        public ComboRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Combo>> GetAll()
        {
            return await _context.Combos.ToListAsync();
        }
        public async Task<IEnumerable<Combo>> GetComboProdutos()
        {
            return await _context.Combos.Include(c => c.Produtos).ToListAsync();
        }

        public async Task<Combo> GetById(Guid id)
        {
            return await _context.Combos.Where(c => c.Id == id).FirstOrDefaultAsync();

        }

        public async Task<Combo> Create(Combo combo)
        {
            _context.Combos.Add(combo);
            await _context.SaveChangesAsync();
            return combo;
        }

        public async Task<Combo> Update(Combo combo)
        {
            _context.Entry(combo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return combo;
        }

        public async Task<Combo> Delete(Guid id)
        {
            var combo = await GetById(id);
            _context.Combos.Remove(combo);
            await _context.SaveChangesAsync();
            return combo;
        }
    }
}
