using G64.ProdutoApi.Context;
using G64.ProdutoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace G64.ProdutoApi.Repositories
{
	public class IngredienteRepository : IIngredienteRepository
	{
        private readonly AppDbContext _context;
        public IngredienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingrediente>> GetAll()
        {
            return await _context.Ingredientes.ToListAsync();
        }

        public async Task<Ingrediente> GetById(Guid id)
        {
            return await _context.Ingredientes.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Ingrediente> Create(Ingrediente ingrediente)
		{
            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();
            return ingrediente;
        }

        public async Task<Ingrediente> Update(Ingrediente ingrediente)
        {
            _context.Entry(ingrediente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ingrediente;
        }

        public async Task<Ingrediente> Delete(Guid id)
		{
            var ingrediente = await GetById(id);
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return ingrediente;
        }
	}
}
