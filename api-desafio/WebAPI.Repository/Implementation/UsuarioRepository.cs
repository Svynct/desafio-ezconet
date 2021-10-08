using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using WebAPI.Repository.Interfaces;
using WebAPI.Domain.Models;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public List<UsuarioViewModel> GetAllUsuarios()
        {
            var usuarios = new List<UsuarioViewModel>();

            var query = $"SELECT Usuarios.UsuarioId, Sexos.Descricao AS Sexo, Usuarios.Nome, Usuarios.Ativo, Usuarios.DataNascimento, Usuarios.Email," +
                        $" Usuarios.Senha, Usuarios.SexoId FROM Usuarios INNER JOIN Sexos ON Sexos.SexoId = Usuarios.SexoId";

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    usuarios.Add(new UsuarioViewModel
                    {
                        UsuarioId = (int)sqlDataReader["UsuarioId"],
                        Nome = (string)sqlDataReader["Nome"],
                        DataNascimento = (DateTime)sqlDataReader["DataNascimento"],
                        Email = (string)sqlDataReader["Email"],
                        Ativo = (bool)sqlDataReader["Ativo"],
                        Senha = (string)sqlDataReader["Senha"],
                        Sexo = (string)sqlDataReader["Sexo"],
                        SexoId = (int)sqlDataReader["SexoId"]
                    });
                }

                DatabaseContext.DatabaseConnection.Close();
            }
            catch(Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return usuarios;
        }

        public List<UsuarioViewModel> GetUsuarioByFiltro(FiltroUsuarioModel filtro)
        {
            var usuarios = new List<UsuarioViewModel>();

            string query;

            if (filtro == null || (filtro.Ativo == null && string.IsNullOrWhiteSpace(filtro.Nome)))
                query = $"SELECT Usuarios.UsuarioId, Sexos.Descricao AS Sexo, Usuarios.Nome, Usuarios.Ativo, Usuarios.DataNascimento, Usuarios.Email," +
                        $" Usuarios.Senha, Usuarios.SexoId FROM Usuarios INNER JOIN Sexos ON Sexos.SexoId = Usuarios.SexoId";
            else
                query = $"SELECT Usuarios.UsuarioId, Sexos.Descricao AS Sexo, Usuarios.Nome, Usuarios.Ativo, Usuarios.DataNascimento, Usuarios.Email," +
                        $" Usuarios.Senha, Usuarios.SexoId FROM Usuarios INNER JOIN Sexos ON Sexos.SexoId = Usuarios.SexoId WHERE ";

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                query += $"Nome LIKE '%{filtro.Nome}%' ";
            
            if (filtro.Ativo.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(filtro.Nome))
                    query += $"AND Ativo = {ToBit(filtro.Ativo)}";
                else
                    query += $"Ativo = {ToBit(filtro.Ativo)}";
            }

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    usuarios.Add(new UsuarioViewModel
                    {
                        UsuarioId = (int)sqlDataReader["UsuarioId"],
                        Nome = (string)sqlDataReader["Nome"],
                        DataNascimento = (DateTime)sqlDataReader["DataNascimento"],
                        Email = (string)sqlDataReader["Email"],
                        Ativo = (bool)sqlDataReader["Ativo"],
                        Senha = (string)sqlDataReader["Senha"],
                        Sexo = (string)sqlDataReader["Sexo"],
                        SexoId = (int)sqlDataReader["SexoId"]
                    });
                }

                DatabaseContext.DatabaseConnection.Close();
            }
            catch(Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return usuarios;
        }

        public UsuarioViewModel GetUsuarioById(int id)
        {
            var UsuarioViewModel = new UsuarioViewModel();

            var query = $"SELECT * FROM Usuarios WHERE UsuarioId = {id}";

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    UsuarioViewModel = new UsuarioViewModel
                    {
                        UsuarioId = (int)sqlDataReader["UsuarioId"],
                        Nome = (string)sqlDataReader["Nome"],
                        DataNascimento = (DateTime)sqlDataReader["DataNascimento"],
                        Email = (string)sqlDataReader["Email"],
                        Ativo = (bool)sqlDataReader["Ativo"],
                        Senha = (string)sqlDataReader["Senha"],
                        SexoId = (int)sqlDataReader["SexoId"]
                    };
                }

                DatabaseContext.DatabaseConnection.Close();
            }
            catch (Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return UsuarioViewModel;
        }
        
        public int CadastrarUsuario(Usuario usuario)
        {
            var query =
                $"INSERT Usuarios (Nome, DataNascimento, Email, Senha, Ativo, SexoId)" +
                $" VALUES ('{usuario.Nome}', '{usuario.DataNascimento}', '{usuario.Email}', '{usuario.Senha}', {ToBit(usuario.Ativo)}, {usuario.SexoId})";

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                DatabaseContext.DatabaseConnection.Close();

                return rowsAffected;
            }
            catch(Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return 0;
        }

        public int RemoverUsuario(int id)
        {
            var query = $"DELETE FROM Usuarios WHERE UsuarioId = {id}";

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                DatabaseContext.DatabaseConnection.Close();

                return rowsAffected;
            }
            catch (Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return 0;
        }

        public int AtualizarUsuario(Usuario usuario)
        {
            var query = $"UPDATE Usuarios SET Nome = '{usuario.Nome}', DataNascimento = '{usuario.DataNascimento}', Email = '{usuario.Email}', " +
                        $"Senha = '{usuario.Senha}', Ativo = {ToBit(usuario.Ativo)}, SexoId = {usuario.SexoId} WHERE UsuarioId = {usuario.UsuarioId}";

            try
            {
                DatabaseContext.DatabaseConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, DatabaseContext.DatabaseConnection);

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                DatabaseContext.DatabaseConnection.Close();

                return rowsAffected;
            }
            catch (Exception)
            {
                DatabaseContext.DatabaseConnection.Close();
            }

            return 0;
        }

        public static int? ToBit(bool? B)
        {
            if (B == true)
                return 1;
            else if (B == false)
                return 0;

            return null;
        }
    }
}
