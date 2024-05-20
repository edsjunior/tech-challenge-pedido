using AutoMapper;
using Microsoft.EntityFrameworkCore;
using G64.PedidoAPI.Context;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Repositories;

public class CarrinhoPedidoRepository : ICarrinhoPedidoRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public CarrinhoPedidoRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

	public async Task<bool> CleanCarrinhoPedidoAsync(Guid Id)
    {
        var headerPedido = await _context.HeaderPedidos.FirstOrDefaultAsync(c => c.Id == Id);

        if (headerPedido is not null)
        {
            _context.ItemPedidos.RemoveRange(_context.ItemPedidos.Where(c => c.HeaderPedidoId == headerPedido.Id));
            _context.HeaderPedidos.Remove(headerPedido);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CarrinhoPedidoDTO> GetCarrinhoPedidoByUserIdAsync(string userId)
    {

        CarrinhoPedido carrinhoPedido = new()
        {
            HeaderPedido = await _context.HeaderPedidos.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        carrinhoPedido.ItemPedidos = _context.ItemPedidos.Where(c => c.HeaderPedidoId == carrinhoPedido.HeaderPedido.Id)
            .Include(c => c.Produto);

        return _mapper.Map<CarrinhoPedidoDTO>(carrinhoPedido);
    }

    public async Task<bool> DeleteItemCarrinhoAsync(Guid itemPedidoId)
    {
        try
        {
            ItemPedido itemPedido = await _context.ItemPedidos
                               .FirstOrDefaultAsync(c => c.Id == itemPedidoId);

            int total = _context.ItemPedidos.Where(c => c.HeaderPedidoId == itemPedido.HeaderPedidoId).Count();

            _context.ItemPedidos.Remove(itemPedido);
            await _context.SaveChangesAsync();

            if (total == 1)
            {
                var headerPedido = await _context.HeaderPedidos.FirstOrDefaultAsync(
                                             c => c.Id == itemPedido.HeaderPedidoId);

                _context.HeaderPedidos.Remove(headerPedido);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<CarrinhoPedidoDTO> UpdateCarrinhoPedidoAsync(CarrinhoPedidoDTO carrinhoPedidoDto)
    {
        CarrinhoPedido carrinhoPedido = _mapper.Map<CarrinhoPedido>(carrinhoPedidoDto);

        //salva o produto no banco 
        await SaveProdutoInDataBase(carrinhoPedidoDto, carrinhoPedido);

        //Verifica se o CartHeader é null
        var headerPedido = await _context.HeaderPedidos.AsNoTracking().FirstOrDefaultAsync(
                               c => c.UserId == carrinhoPedido.HeaderPedido.UserId);

        if (headerPedido is null)
        {
            //criar o Header e os itens
            await CreateHeaderPedidoAndItems(carrinhoPedido);
        }
        else
        {
            //atualiza a quantidade e os itens
            await UpdateQuantityAndItems(carrinhoPedidoDto, carrinhoPedido, headerPedido);
        }
        return _mapper.Map<CarrinhoPedidoDTO>(carrinhoPedido);
	}

    private async Task UpdateQuantityAndItems(CarrinhoPedidoDTO carrinhoPedidoDto, CarrinhoPedido carrinhoPedido, HeaderPedido? headerPedido)
    {
        var itemPedido = await _context.ItemPedidos.AsNoTracking().FirstOrDefaultAsync(
                               p => p.ProdutoId == carrinhoPedidoDto.ItemPedidos.FirstOrDefault()
                               .ProdutoId && p.HeaderPedidoId == headerPedido.Id);

        if (itemPedido is null)
        {
            carrinhoPedido.ItemPedidos.FirstOrDefault().HeaderPedidoId = headerPedido.Id;
            carrinhoPedido.ItemPedidos.FirstOrDefault().Produto = null;
            _context.ItemPedidos.Add(carrinhoPedido.ItemPedidos.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        else
        {
            carrinhoPedido.ItemPedidos.FirstOrDefault().Produto = null;
            carrinhoPedido.ItemPedidos.FirstOrDefault().Quantidade += itemPedido.Quantidade;
            carrinhoPedido.ItemPedidos.FirstOrDefault().Id = itemPedido.Id;
            carrinhoPedido.ItemPedidos.FirstOrDefault().HeaderPedidoId = itemPedido.HeaderPedidoId;
            _context.ItemPedidos.Update(carrinhoPedido.ItemPedidos.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
    }

    private async Task CreateHeaderPedidoAndItems(CarrinhoPedido carrinhoPedido)
    {
        _context.HeaderPedidos.Add(carrinhoPedido.HeaderPedido);
        await _context.SaveChangesAsync();

        carrinhoPedido.ItemPedidos.FirstOrDefault().HeaderPedidoId = carrinhoPedido.HeaderPedido.Id;
        carrinhoPedido.ItemPedidos.FirstOrDefault().Produto = null;

        _context.ItemPedidos.Add(carrinhoPedido.ItemPedidos.FirstOrDefault());

        await _context.SaveChangesAsync();
    }

    private async Task SaveProdutoInDataBase(CarrinhoPedidoDTO carrinhoPedidoDTO, CarrinhoPedido carrinhoPedido)
    {
        //Verifica se o produto ja foi salvo senão salva
        var product = await _context.Produtos.FirstOrDefaultAsync(p => p.Id ==
                            carrinhoPedidoDTO.ItemPedidos.FirstOrDefault().ProdutoId);

        //salva o produto senão existe no bd
        if (product is null)
        {
            _context.Produtos.Add(carrinhoPedido.ItemPedidos.FirstOrDefault().Produto);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<IEnumerable<CarrinhoPedidoDTO>> GetAll()
    {
        return await _context.CarrinhoPedido.ToListAsync();
    }

    public async Task<CarrinhoPedidoDTO> GetCarrinhoPedidoById(Guid Id)
    {
        CarrinhoPedido carrinhoPedido = new()
        {
            HeaderPedido = await _context.HeaderPedidos.FirstOrDefaultAsync(c => c.Id == Id)
        };

        carrinhoPedido.ItemPedidos = _context.ItemPedidos.Where(c => c.HeaderPedidoId == carrinhoPedido.HeaderPedido.Id)
            .Include(c => c.Produto);

        return _mapper.Map<CarrinhoPedidoDTO>(carrinhoPedido);
    }


}
