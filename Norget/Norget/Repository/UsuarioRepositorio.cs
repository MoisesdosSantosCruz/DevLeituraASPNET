using MySql.Data.MySqlClient;
using Norget.Models;
using System.Data;

// RECEBA (TROCAR O CÓDIGO)

namespace Norget.Repository
{
    
    // Chamar a interface com herança
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        //declarando a varival de da string de conexão

        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public UsuarioRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        //Login Cliente(metodo )

        public Usuario Login(string Email, string Senha)
        {
            //usando a variavel conexao 
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                //abre a conexão com o banco de dados
                conexao.Open();

                // variavel cmd que receb o select do banco de dados buscando email e senha
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where email = @Email and senha = @Senha", conexao);

                //os paramentros do email e da senha 
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                // Lê os dados que foi pego do email e senha do banco de dados
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Guarda os dados que foi pego do email e senha do banco de dados
                MySqlDataReader dr;

                // Instanciando a model cliente
                Usuario usuario = new Usuario();
                // Executando os comandos do mysql e passsando paa a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // Verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {
                    usuario.Email = Convert.ToString(dr["email"]);
                    usuario.Senha = Convert.ToString(dr["senha"]);
                }
                return usuario;
            }
        }
        // MÉTODO CADASTRAR CLIENTE
        public void Cadastro(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Insert into tbCliente (Nome,Tel,Email) values (@Nome, @Telefone, @Email)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = usuario.Tel;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = usuario.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }
        // Listar todos os clientes

        public IEnumerable<Usuario> TodosUsuarios()
        {
            List<Usuario> Usuariolist = new List<Usuario>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbCliente", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Usuariolist.Add(
                            new Usuario
                            {
                                Nome = ((string)dr["nome"]),
                                Tel = ((int)dr["tel"]),
                                Email = ((string)dr["email"]),
                            });
                }
                return Usuariolist;

            }
        }

        // Buscar todos os clientes por id
        public Usuario ObterUsuario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from tbCliente ", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                // retorna conjunto de resultado ,  é funcionalmente equivalente a chamar ExecuteReader().
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    usuario.Id = Convert.ToInt32(dr["id"]);
                    usuario.Nome = (string)(dr["nome"]);
                    usuario.Tel = (int)(dr["tel"]);
                    usuario.Email = (string)(dr["email"]);
                }
                return usuario;
            }
        }

        //Alterar Cliente
        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbCliente set nome=@nome, tel=@telefone, email=@email " +
                                                    " where cod=@codigo ", conexao);

                cmd.Parameters.Add("@codigo", MySqlDbType.VarChar).Value = usuario.Id;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = usuario.Tel;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        // Excluir
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCliente where cod=@codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

    }

}