using Travlr.Utils;
using Travlr.Models;

namespace Travlr.Repositories
{
    public class DineRepository : BaseRepository, IDineRepository
    {
        public DineRepository(IConfiguration configuration) : base(configuration) { }

        public List<Dine> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT
                                                id,
                                                dineName,
                                                dineAddress,
                                                dineImage,
                                                dineDescription,
                                                dineNotes
                                            FROM [Dine]";

                    var dines = new List<Dine>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dine = new Dine()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            DineName = DbUtils.GetString(reader, "dineName"),
                            DineAddress = DbUtils.GetString(reader, "dineAddress"),
                            DineImage = DbUtils.GetString(reader, "dineImage"),
                            DineDescription = DbUtils.GetString(reader, "dineDescription"),
                            DineNotes = DbUtils.GetString(reader, "dineNotes")
                        };

                        dines.Add(dine);
                    }

                    reader.Close();
                    return dines;
                }
            }
        }

        public Dine GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT
                                                id,
                                                dineName,
                                                dineAddress,
                                                dineImage,
                                                dineDescription,
                                                dineNotes
                                            FROM [Dine]
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", id);

                    var reader = command.ExecuteReader();

                    Dine dine = null;
                    if (reader.Read())
                    {
                        dine = new Dine()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            DineName = DbUtils.GetString(reader, "dineName"),
                            DineAddress = DbUtils.GetString(reader, "dineAddress"),
                            DineImage = DbUtils.GetString(reader, "dineImage"),
                            DineDescription = DbUtils.GetString(reader, "dineDescription"),
                            DineNotes = DbUtils.GetString(reader, "dineNotes")
                        };
                    }

                    reader.Close();
                    return dine;
                }
            }
        }

        
        public void Add(Dine category)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [Dine]
                                                (dineName,
                                                dineAddress,
                                                dineImage,
                                                dineDescription,
                                                dineNotes)
                                            OUTPUT INSERTED.ID
                                            VALUES
                                                (@dineName,
                                                 @dineAddress,
                                                 @dineImage,
                                                 @dineDescription,
                                                 @dineNotes)";

                    DbUtils.AddParameter(command, "@dineName", category.DineName);
                    DbUtils.AddParameter(command, "@dineAddress", category.DineAddress);
                    DbUtils.AddParameter(command, "@dineImage", category.DineImage);
                    DbUtils.AddParameter(command, "@dineDescription", category.DineDescription);
                    DbUtils.AddParameter(command, "@dineNotes", category.DineNotes);

                    category.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Dine dine)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Dine]
                                                SET dineName = @dineName,
                                                    dineAddress = @dineAddress,
                                                    dineImage = @dineImage,
                                                    dineDescription = @dineDescription,
                                                    dineNotes = @dineNotes
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", dine.Id);
                    DbUtils.AddParameter(command, "@dineName", dine.DineName);
                    DbUtils.AddParameter(command, "@dineAddress", dine.DineAddress);
                    DbUtils.AddParameter(command, "@dineImage", dine.DineImage);
                    DbUtils.AddParameter(command, "@dineDescription", dine.DineDescription);
                    DbUtils.AddParameter(command, "@dineNotes", dine.DineNotes);


                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Dine] WHERE id = @id";
                    DbUtils.AddParameter(command, "@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

