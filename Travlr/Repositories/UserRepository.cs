using Travlr.Models;
using Travlr.Repositories;
using Travlr.Utils;

namespace Travlr.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    public List<User> GetAll()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
                                        id,
	                                    name,
	                                    dateCreated,
	                                    email,
	                                    firebaseId
                                    FROM [User]";

                var reader = cmd.ExecuteReader();
                var users = new List<User>();

                while (reader.Read())
                {
                    var user = new User()
                    {
                        Id = DbUtils.GetInt(reader, "id"),
                        Name = DbUtils.GetString(reader, "name"),
                        DateCreated = DbUtils.GetDateTime(reader, "dateCreated"),
                        Email = DbUtils.GetString(reader, "email"),
                        FirebaseId = DbUtils.GetString(reader, "firebaseId"),

                    };

                    users.Add(user);
                }

                reader.Close();
                return users;
            }
        }
    }

    public User GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
                                        id,
	                                    name,
	                                    dateCreated,
	                                    email,
	                                    firebaseId
                                    FROM [User]
                                    WHERE id = @id";
                DbUtils.AddParameter(cmd, "@id", id);

                var reader = cmd.ExecuteReader();

                User user = null;
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = DbUtils.GetInt(reader, "id"),
                        Name = DbUtils.GetString(reader, "name"),
                        DateCreated = DbUtils.GetDateTime(reader, "dateCreated"),
                        Email = DbUtils.GetString(reader, "email"),
                        FirebaseId = DbUtils.GetString(reader, "firebaseId")
                    };
                }

                reader.Close();
                return user;
            }
        }
    }

    public User GetByFirebaseId(string uid)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
                                        u.id,
	                                    u.name,
	                                    u.dateCreated,
	                                    u.email,
	                                    u.firebaseId,
                                        t.id as tripId,
                                        t.userId as tripUserId,
                                        t.tripName,
                                        t.pastTrip,
                                        t.description as tripDescription,
                                        t.budget
                                    FROM [User] u
                                    LEFT JOIN Trip t ON u.id = t.userId
                                    WHERE firebaseId = @uid";
                DbUtils.AddParameter(cmd, "@uid", uid);

                var reader = cmd.ExecuteReader();

                User user = null;
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = DbUtils.GetInt(reader, "id"),
                        Name = DbUtils.GetString(reader, "name"),
                        DateCreated = DbUtils.GetDateTime(reader, "dateCreated"),
                        Email = DbUtils.GetString(reader, "email"),
                        FirebaseId = DbUtils.GetString(reader, "firebaseId")
                    };

                    if (DbUtils.IsNotDbNull(reader, "tripId"))
                    {
                        user.Trip = new Trip()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "tripUserId"),
                            TripName = DbUtils.GetString(reader, "tripName"),
                            PastTrip = DbUtils.GetBoolean(reader, "pastTrip"),
                            Description = DbUtils.GetString(reader, "tripDescription"),
                            Budget = DbUtils.GetInt(reader, "budget")
                        };
                    }
                };

                reader.Close();
                return user;
            }
        }
    }

    public void Add(User user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO [User]
	                                    (name,
	                                    dateCreated,
	                                    email,
	                                    firebaseId)
                                    OUTPUT INSERTED.ID
                                    VALUES
	                                    (@name,
                                        @dateCreated,
                                        @email,
                                        @firebaseId)";

                DbUtils.AddParameter(cmd, "@name", user.Name);
                DbUtils.AddParameter(cmd, "@dateCreated", user.DateCreated);
                DbUtils.AddParameter(cmd, "@email", user.Email);
                DbUtils.AddParameter(cmd, "@firebaseId", user.FirebaseId);


                user.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public void Update(User user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE [User]
	                                    SET name = @name,
	                                        dateCreated = @dateCreated,
	                                        email = @email,
	                                        firebaseId = @firebaseId,
                                    WHERE id = @id";

                DbUtils.AddParameter(cmd, "@id", user.Id);
                DbUtils.AddParameter(cmd, "@name", user.Name);
                DbUtils.AddParameter(cmd, "@dateCreated", user.DateCreated);
                DbUtils.AddParameter(cmd, "@email", user.Email);
                DbUtils.AddParameter(cmd, "@firebaseId", user.FirebaseId);


                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM [User] WHERE id = @id";
                DbUtils.AddParameter(cmd, "@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}