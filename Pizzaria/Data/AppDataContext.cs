using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Data;

public class AppDataContext : DbContext
{
	public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

	//Classes que vão virar tabelas no banco de dados
   
	public DbSet<Atendente> Atendentes { get; set; }

	public DbSet<Cliente> Clientes { get; set; }

	public DbSet<Cardapio> Cardapios { get; set; }
	
	public DbSet<Pedido> Pedidos { get; set; }

	public DbSet<Carrinho> Carrinhos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//Como popular uma base de dados utilizando EF no método
		//OnModelCreating, quero dados reais de produto, com os seguintes
		//atributos


		base.OnModelCreating(modelBuilder);
	}
}

