using System;
using Travlr.Utils;
using Travlr.Models;

namespace Travlr.Repositories
{
    public class TripRepository : BaseRepository, ITripRepository
    {
        public TripRepository(IConfiguration configuration) : base(configuration) { }

        public List<Trip> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT
                                                id,
                                                userId as tripUserId,
                                                tripName,
                                                pastTrip,
                                                description,
                                                budget
                                            FROM [Trip]";

                    var trips = new List<Trip>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var trip = new Trip()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "tripUserId"),
                            TripName = DbUtils.GetString(reader, "tripName"),
                            PastTrip = DbUtils.GetBoolean(reader, "pastTrip"),
                            Description = DbUtils.GetString(reader, "description"),
                            Budget = DbUtils.GetInt(reader, "budget")
                        };

                        trips.Add(trip);
                    }

                    reader.Close();
                    return trips;
                }
            }
        }

        public Trip GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT
                                                id,
                                                userId as tripUserId,
                                                tripName,
                                                pastTrip,
                                                description,
                                                budget
                                            FROM [Trip]
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", id);

                    var reader = command.ExecuteReader();

                    Trip trip = null;
                    if (reader.Read())
                    {
                        trip = new Trip()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "tripUserId"),
                            TripName = DbUtils.GetString(reader, "tripName"),
                            PastTrip = DbUtils.GetBoolean(reader, "pastTrip"),
                            Description = DbUtils.GetString(reader, "description"),
                            Budget = DbUtils.GetInt(reader, "budget")
                        };
                    }

                    reader.Close();
                    return trip;
                }
            }
        }

        public void Add(Trip category)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [Trip]
                                                (userId,
                                                tripName,
                                                pastTrip,
                                                description,
                                                budget)
                                            OUTPUT INSERTED.ID
                                            VALUES
                                                (@userId,
                                                 @tripName,
                                                 @pastTrip,
                                                 @description,
                                                 @budget)";

                    DbUtils.AddParameter(command, "@userId", category.UserId);
                    DbUtils.AddParameter(command, "@tripName", category.TripName);
                    DbUtils.AddParameter(command, "@pastTrip", category.PastTrip);
                    DbUtils.AddParameter(command, "@description", category.Description);
                    DbUtils.AddParameter(command, "@budget", category.Budget);

                    category.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Trip trip)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Trip]
                                                SET userId = @userId,
                                                    tripName = @tripName,
                                                    pastTrip = @pastTrip,
                                                    description = @description,
                                                    budget = @budget
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", trip.Id);
                    DbUtils.AddParameter(command, "@userId", trip.UserId);
                    DbUtils.AddParameter(command, "@tripName", trip.TripName);
                    DbUtils.AddParameter(command, "@pastTrip", trip.PastTrip);
                    DbUtils.AddParameter(command, "@description", trip.Description);
                    DbUtils.AddParameter(command, "@budget", trip.Budget);


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
                    command.CommandText = "DELETE FROM [Trip] WHERE id = @id";
                    DbUtils.AddParameter(command, "@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
