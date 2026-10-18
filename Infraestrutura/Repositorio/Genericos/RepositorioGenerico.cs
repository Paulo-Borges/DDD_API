using Dominio.Interfaces.Genericos;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infraestrutura.Repositorio.Genericos
{
    public class RepositorioGenerico<T> : IGenericos<T>, IDisposable where T : class
    {
        public Task Adicionar(T Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(T Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<T> BuscarPorId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(T Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Listar()
        {
            throw new NotImplementedException();
        }


        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose

        bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        #endregion
    }
}
 