using Senai.People.WebApi.Domain;
using Senai.People.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.People.WebApi.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private string stringConexao = "Data Source=DEV12\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public List<PeopleDomain> Listar()
        {
            List<PeopleDomain> Listar = new List<PeopleDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryListarAll = "SELECT  IdFuncionario, Nome, Sobrenome FROM Funcionario";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryListarAll, con))
                {
                    con.Open();

                    rdr = cmd.ExecuteReader();

                    while(rdr.Read()) //while = enquanto
                    {
                        PeopleDomain People = new PeopleDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),

                            Nome = rdr["Nome"].ToString(),

                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        Listar.Add(People);
                    }
                }
                return Listar;
            }
        }

        public void AtualizarIdUrl(int id, PeopleDomain People)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE People SET Nome = @Nome WHERE IdFuncionario = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", People.Nome);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdCorpo(PeopleDomain people)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Funcionario SET  Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Nome", people.Nome);

                    cmd.Parameters.AddWithValue("@Sobrenome", people.Sobrenome);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public PeopleDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdFuncionario, Nome FROM Funcionario WHERE IdFuncionario = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader fazer a leitura no banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Caso a o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Cria um objeto genero
                        PeopleDomain people = new PeopleDomain
                        {
                            // Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            Nome = rdr["Nome"].ToString()
                        };

                        // Retorna o genero com os dados obtidos
                        return people;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(PeopleDomain people)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryInsert = "INSERT INTO Generos(Nome) VALUES (@Nome)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@Nome", people.Nome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM People WHERE IdPeople = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(PeopleDomain people)
        {
            throw new NotImplementedException();
        }
    }
}
