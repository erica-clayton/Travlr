using Travlr.Utils;
using Travlr.Models;

namespace Travlr.Repositories
{
    public class StayRepository : BaseRepository, IStayRepository
    {
        public StayRepository(IConfiguration configuration) : base(configuration) { }

        public List<Stay> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT
                                                id,
                                                stayName,
                                                stayAddress,
                                                stayImage,
                                                stayDescription,
                                                stayNotes
                                            FROM [Stay]";

                    var stays = new List<Stay>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var stay = new Stay()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            StayName = DbUtils.GetString(reader, "stayName"),
                            StayAddress = DbUtils.GetString(reader, "stayAddress"),
                            StayImage = DbUtils.GetString(reader, "stayImage"),
                            StayDescription = DbUtils.GetString(reader, "stayDescription"),
                            StayNotes = DbUtils.GetString(reader, "stayNotes")
                        };

                        stays.Add(stay);
                    }

                    reader.Close();
                    return stays;
                }
            }
        }

        public Stay GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT
                                                id,
                                                stayName,
                                                stayAddress,
                                                stayImage,
                                                stayDescription,
                                                stayNotes
                                            FROM [Stay]
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", id);

                    var reader = command.ExecuteReader();

                    Stay stay = null;
                    if (reader.Read())
                    {
                        stay = new Stay()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            StayName = DbUtils.GetString(reader, "stayName"),
                            StayAddress = DbUtils.GetString(reader, "stayAddress"),
                            StayImage = DbUtils.GetString(reader, "stayImage"),
                            StayDescription = DbUtils.GetString(reader, "stayDescription"),
                            StayNotes = DbUtils.GetString(reader, "stayNotes")
                        };
                    }

                    reader.Close();
                    return stay;
                }
            }
        }

        public void Add(Stay category)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [Stay]
                                                (stayName,
                                                stayAddress,
                                                stayImage,
                                                stayDescription,
                                                stayNotes)
                                            OUTPUT INSERTED.ID
                                            VALUES
                                                (@stayName,
                                                 @stayAddress,
                                                 @stayImage,
                                                 @stayDescription,
                                                 @stayNotes)";

                    DbUtils.AddParameter(command, "@stayName", category.StayName);
                    DbUtils.AddParameter(command, "@stayAddress", category.StayAddress);
                    DbUtils.AddParameter(command, "@stayImage", category.StayImage);
                    DbUtils.AddParameter(command, "@stayDescription", category.StayDescription);
                    DbUtils.AddParameter(command, "@stayNotes", category.StayNotes);

                    category.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Stay stay)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Stay]
                                                SET stayName = @stayName,
                                                    stayAddress = @stayAddress,
                                                    stayImage = @stayImage,
                                                    stayDescription = @stayDescription,
                                                    stayNotes = @stayNotes
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", stay.Id);
                    DbUtils.AddParameter(command, "@stayName", stay.StayName);
                    DbUtils.AddParameter(command, "@stayAddress", stay.StayAddress);
                    DbUtils.AddParameter(command, "@stayImage", stay.StayImage);
                    DbUtils.AddParameter(command, "@stayDescription", stay.StayDescription);
                    DbUtils.AddParameter(command, "@stayNotes", stay.StayNotes);


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
                    command.CommandText = "DELETE FROM [Stay] WHERE id = @id";
                    DbUtils.AddParameter(command, "@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}


