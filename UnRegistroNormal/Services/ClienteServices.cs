using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UnRegistroNormal.DAL;
using UnRegistroNormal.Models;

namespace UnRegistroNormal.Services;

public class ClienteServices
{
	private readonly Contexto _contexto;
	public ClienteServices (Contexto contexto)
	{
		_contexto = contexto;
	}
	public async Task<bool> Extiste (int ClienteId)
	{
		return await _contexto.clientes
			.AnyAsync(c => c.ClienteId == ClienteId);
	}
	public async Task<bool> Guardar (Cliente cliente)
	{
		if (!await Extiste(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}
	public async Task<bool> Insertar(Cliente cliente)
	{
		_contexto.Add(cliente);
		return await _contexto
			.SaveChangesAsync() > 0;
	}
	public async Task<bool> Modificar(Cliente cliente)
	{
		_contexto.Update(cliente);
		return await _contexto
			.SaveChangesAsync() > 0;
	}
	public async Task<bool> Eliminar (Cliente cliente)
	{
		var cantidad = await _contexto.clientes
			.Where(c => c.ClienteId == cliente.ClienteId)
			.ExecuteDeleteAsync();
		return cantidad > 0;
	}
    public async Task<Cliente?> BuscarNombre(string nombre)
    {
        return await _contexto.clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombre.ToLower() == nombre.ToLower());
    }
    public async Task<Cliente?> BuscarRNC(string RNC)
    {
        return await _contexto.clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.RNC == RNC);
    }
    public async Task<Cliente?> Buscar (int ClienteId)
	{
		return await _contexto.clientes.AsNoTracking().FirstOrDefaultAsync(c => c.ClienteId == ClienteId);
	}
	public async Task<List<Cliente>> Listar (Expression<Func<Cliente, bool>> criterio)
	{
		return await _contexto.clientes
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
 
}
