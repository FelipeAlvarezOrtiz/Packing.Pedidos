using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Packing.Pedidos.Core;
using Packing.Pedidos.Core.Pedidos;
using Packing.Pedidos.Core.Productos;

namespace Packing.Pedidos.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var cascadeFKs = builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        base.OnModelCreating(builder);
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Contacto> Contactos { get; set; }

    public DbSet<Formato> Formatos { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Presentacion> Presentaciones { get; set; }

    public DbSet<DetallePedido> DetallesPedido { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<EstadoPedido> EstadosPedidos { get; set; }
}
