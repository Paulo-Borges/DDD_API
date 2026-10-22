using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;

        public RepositorioUsuario()
        {
            _optionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var data = new Contexto(_optionsBuilder))
                {
                    await data.ApplicationUser.AddAsync(new ApplicationUser
                    {
                        Email = email,
                        PasswordHash = senha,
                        Idade = idade,
                        Celular = celular,
                        Tipo = TipoUsuario.Comum
                    });
                   
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;   
            }
            return true;
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var data = new Contexto(_optionsBuilder))
                {
                    return await data.ApplicationUser.Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(senha))
                        .AsNoTracking()
                        .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            try
            {
                using (var data = new Contexto(_optionsBuilder))
                {
                    var usuario = await data.ApplicationUser.Where(u => u.Email.Equals(email))
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    return usuario.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
