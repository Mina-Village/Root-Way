using System.Collections.Generic;
using System.Net;
using System.Security;
using Root_Way.Models;
using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public bool AuthenticateUser(NetworkCredential credential)
    {
        bool validUser;
        using (var connection = GetConnection())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select *from `User` where username=@username and password=@password";
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credential.UserName;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = credential.Password;
            validUser = command.ExecuteScalar() != null;
        }
        return validUser;
    }

    public void Add(UserModel userModel)
    {
        using (var connection = GetConnection())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "INSERT INTO `User` (username, password) VALUES (@username, @password)";
            command.Parameters.AddWithValue("@username", userModel.Username);
            command.Parameters.AddWithValue("@password", userModel.Password);
            command.ExecuteNonQuery();
        }
    }

    public void Edit(UserModel userModel)
    {
        using (var connection = GetConnection())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "UPDATE `User` SET password = @password WHERE username = @username";
            command.Parameters.AddWithValue("@password", userModel.Password);
            command.Parameters.AddWithValue("@username", userModel.Username);
            command.ExecuteNonQuery();
        }
    }

    public void Remove(int id)
    {
        //throw new System.NotImplementedException();
    }

    public UserModel GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    public UserModel GetByUsername(string username)
    {
        UserModel? user = null; 
        using (var connection = GetConnection())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from `User` where username=@username";
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new UserModel()
                    {
                        Id = reader[0].ToString(),
                        Username = reader[1].ToString(),
                        Password = string.Empty,
                    };
                }
            }
        }
        return user;
    }

    public IEnumerable<UserModel> GetByAll()
    {
        throw new System.NotImplementedException();
    }
}