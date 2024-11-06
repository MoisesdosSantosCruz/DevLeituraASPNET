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
                Usuario cliente = new Usuario();
                // Executando os comandos do mysql e passsando paa a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // Verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {
                    cliente.Email = Convert.ToString(dr["email"]);
                    cliente.Senha = Convert.ToString(dr["senha"]);
                }
                return cliente;
            }
        }
        // MÉTODO CADASTRAR CLIENTE
        public void Cadastrar(Usuario cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Insert into tbCliente (Nome,Tel,Email) values (@Nome, @Telefone, @Email)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Tel;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }
        // Listar todos os clientes

        public IEnumerable<Usuario> TodosClientes()
        {
            List<Usuario> Clientlist = new List<Usuario>();

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
                    Clientlist.Add(
                            new Usuario
                            {
                                Nome = ((string)dr["nome"]),
                                Tel = ((string)dr["tel"]),
                                Email = ((string)dr["email"]),
                            });
                }
                return Clientlist;

            }
        }

        // Buscar todos os clientes por id
        public Usuario ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from tbCliente ", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario cliente = new Usuario();
                // retorna conjunto de resultado ,  é funcionalmente equivalente a chamar ExecuteReader().
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["id"]);
                    cliente.Nome = (string)(dr["nome"]);
                    cliente.Tel = (string)(dr["tel"]);
                    cliente.Email = (string)(dr["email"]);
                }
                return cliente;
            }
        }

        //Alterar Cliente
        public void Atualizar(Usuario cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbCliente set nome=@nome, tel=@telefone, email=@email " +
                                                    " where cod=@codigo ", conexao);

                cmd.Parameters.Add("@codigo", MySqlDbType.VarChar).Value = cliente.Id;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Tel;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;

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