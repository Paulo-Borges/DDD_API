namespace Dominio.Interfaces
{
    public interface IUsuario
    {
        Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular);
    }
}
