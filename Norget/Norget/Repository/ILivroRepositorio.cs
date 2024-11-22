using Norget.Models;

namespace Norget.Repository
{
    public interface ILivroRepositorio
    {
         IEnumerable<Livro> ListarLivros();
        
        public Livro ObterLivro(int ISBN);

        void CadastroLivro(Livro livro);




    }
}
