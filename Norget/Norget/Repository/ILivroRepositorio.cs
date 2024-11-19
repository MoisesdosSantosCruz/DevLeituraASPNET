using Norget.Models;

namespace Norget.Repository
{
    public interface ILivroRepositorio
    {
        public IEnumerable<Livro> ListarLivros();
        public Livro ObterLivro(int ISBN);
        
    }
}
