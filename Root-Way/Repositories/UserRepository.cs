using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using Root_Way.Models;
using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public string HashPassword(string password, string salt)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }
    public bool AuthenticateUser(NetworkCredential credential)
    {
        string salt = GetSaltByUsername(credential.UserName);
        if (string.IsNullOrEmpty(salt))
        {
            return false;
        }
        string hashedPassword = HashPassword(credential.Password, salt);
        
        bool validUser;
        using (var connection = GetConnectionDB())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from `User` where username=@username and password=@password";
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credential.UserName;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = hashedPassword;
            validUser = command.ExecuteScalar() != null;
        }
        return validUser;
    }

    public void Add(UserModel userModel)
    {
        string salt = GenerateSalt();
        string hashedPassword = HashPassword(userModel.Password, salt);
        using (var connection = GetConnectionDB())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "INSERT INTO `User` (username, password, salt) VALUES (@username, @password, @salt)";
            command.Parameters.AddWithValue("@username", userModel.Username);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.Parameters.AddWithValue("@salt", salt);
            command.ExecuteNonQuery();
        }
    }

    public void Edit(UserModel userModel)
    {
        string salt = GenerateSalt();
        string hashedPassword = HashPassword(userModel.Password, salt);
        using (var connection = GetConnectionDB())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "UPDATE `User` SET password = @password, salt = @salt WHERE username = @username";
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.Parameters.AddWithValue("@salt", salt);
            command.Parameters.AddWithValue("@username", userModel.Username);
            command.ExecuteNonQuery();
        }
    }

    public void Remove(int id)
    {
        // throw new System.NotImplementedException();
    }

    public UserModel GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    public UserModel GetByUsername(string username)
    {
        UserModel? user = null; 
        using (var connection = GetConnectionDB())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select Id, Username from `User` where username=@username";
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
    
    public string GetSaltByUsername(string username)
    {
        if (GetByUsername(username) == null)
        {
            return null;
        }
        
        using (var connection = GetConnectionDB())
        using (var command = new MySqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select Salt from `User` where username=@username";
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (reader[0] as string);
                }
            }
        }

        return null;
    }

    public IEnumerable<UserModel> GetByAll()
    {
        throw new System.NotImplementedException();
    }
}
