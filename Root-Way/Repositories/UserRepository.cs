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
        //throw new System.NotImplementedException();
    }

    public void Edit(UserModel userModel)
    {
        //throw new System.NotImplementedException();
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
            command.CommandText = "select * from `User` where username=@username and `password`=@password";
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
                        Name = reader[3].ToString(),
                        LastName = reader[4].ToString(),
                        Email = reader[5].ToString(),
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