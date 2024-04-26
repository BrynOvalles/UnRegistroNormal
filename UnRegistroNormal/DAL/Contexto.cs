using Microsoft.EntityFrameworkCore;
using UnRegistroNormal.Models;

namespace UnRegistroNormal.DAL;

public class Contexto : DbContext
{
	public Contexto(DbContextOptions<Contexto> options) : base(options) {}
	public DbSet<Cliente> clientes { get; set; }
}
