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
            List<Livro> LivroList = new List<Livro>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select ISBN, NomeLiv, PrecoLiv, DescLiv, ImgLiv, Categoria, Autor, DataPubli from tbLivro", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    LivroList.Add(
                        new Livro
                        {
                            ISBN = (decimal)(dr["ISBN"]),
                            NomeLiv = (string)(dr["NomeLiv"]),
                            PrecoLiv = (decimal)(dr["PrecoLiv"]),
                            DescLiv = (string)(dr["DescLiv"]),
                            ImgLiv = (string)(dr["ImgLiv"]),
                            Categoria = (string)(dr["Categoria"]),
                           // EditoraId = (int)(dr["idEdi"]),
                            Autor = (string)(dr["Autor"]),
                            DataPubli = (string)(dr["DataPubli"])

                        }
                    );
                }
            }
            return LivroList;
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
                    livro.ISBN = (decimal)(dr["ISBN"]);
                    livro.NomeLiv = (string)(dr["NomeLiv"]);
                    livro.PrecoLiv = (decimal)(dr["PrecoLiv"]);
                    livro.DescLiv = (string)(dr["DescLiv"]);
                    livro.ImgLiv = (string)(dr["ImgLiv"]);
                    livro.Categoria = (string)(dr["Categoria"]);
                   // livro.EditoraId = (int)(dr["idEdi"]);
                    livro.Autor = (string)(dr["Autor"]);
                    livro.DataPubli = (string)(dr["DataPubli"]);


                }
                return livro;
            }
        }

        public void CadastroLivro(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                string query = "CALL spInsertLivro(@ISBN, @NomeLiv, @PrecoLiv, @DescLiv, @ImgLiv, @Categoria, " +
                        " @Autor, @DataPubli)"; // @: PARAMETRO
                using (var cmd = new MySqlCommand(query, conexao))
                {

                    cmd.Parameters.Add("@ISBN", MySqlDbType.Decimal).Value = livro.ISBN;
                    cmd.Parameters.Add("@NomeLiv", MySqlDbType.VarChar).Value = livro.NomeLiv;
                    cmd.Parameters.Add("@PrecoLiv", MySqlDbType.Decimal).Value = livro.PrecoLiv;
                    cmd.Parameters.Add("@DescLiv", MySqlDbType.VarChar).Value = livro.DescLiv;
                    cmd.Parameters.Add("@ImgLiv", MySqlDbType.VarChar).Value = livro.ImgLiv;
                    cmd.Parameters.Add("@Categoria", MySqlDbType.VarChar).Value = livro.Categoria;
                    //cmd.Parameters.Add("@NomeEdi", MySqlDbType.VarChar).Value = livro.EditoraId;
                    cmd.Parameters.Add("@Autor", MySqlDbType.VarChar).Value = livro.Autor;
                    cmd.Parameters.Add("@DataPubli", MySqlDbType.VarChar).Value = livro.DataPubli;


                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }
            }

        }
    }
}
