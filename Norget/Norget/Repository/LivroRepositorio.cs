using MySql.Data.MySqlClient;
using Norget.Models;
using System.Data;

namespace Norget.Repository
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public LivroRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        public IEnumerable<Livro> ListarLivros()
        {
            List<Livro> ProdList = new List<Livro>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select ISBN, NomeLiv, PrecoLiv, ImgLiv from tbLivro", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdList.Add(
                        new Livro
                        {
                            ISBN = Convert.ToInt64(dr["ISBN"]),
                            NomeLiv = (string)(dr["NomeLiv"]),
                            PrecoLiv = (decimal)(dr["PrecoLiv"]),
                            ImgLiv = (string)(dr["ImgLiv"]),

                        }
                    );
                }
            }
            return ProdList;
        }
        public Livro ObterLivro(int ISBN)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from tbLivro ", conexao);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Livro livro = new Livro();
                // retorna conjunto de resultado ,  é funcionalmente equivalente a chamar ExecuteReader().
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    livro.ISBN = Convert.ToInt32(dr["ISBN"]);
                    livro.NomeLiv = (string)(dr["NomeLiv"]);
                    livro.PrecoLiv = (decimal)(dr["PrecoLiv"]);
                    livro.DescLiv = (string)(dr["DescLiv"]);
                    livro.EditoraId = (int)(dr["idEdi"]);
                }
                return livro;
            }
        }
    }
}
