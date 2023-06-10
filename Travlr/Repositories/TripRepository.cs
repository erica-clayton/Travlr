
using Travlr.Utils;
using Travlr.Models;
using Microsoft.Data.SqlClient;

namespace Travlr.Repositories
{
    public class TripRepository : BaseRepository, ITripRepository
    {
        public TripRepository(IConfiguration configuration) : base(configuration) { }

        public List<Trip> GetAll()
        {
            using var connection = Connection;

            connection.Open();
            //var trans = connection.BeginTransaction();

            var tripSQL = @"SELECT 
                                t.id as tripId,
                                t.userId as tripUserId,
                                t.tripName,
                                t.pastTrip,
                                t.description as tripDisciption,
                                t.budget tripBudget
                                FROM [Trip] t";

            var dineOptionSQL = @" SELECT         
                                       do.id as dineOptionsId,
                                       do.tripId as doTripId,
                                       do.dineId as doDineId,
                                       d.id as dineId,
                                       d.dineName,
                                       d.dineAddress,
                                       d.dineImage,
                                       d.dineDescription,
                                       d.dineNotes
                                      FROM [Dine] d           
                                      LEFT JOIN [DineOptions] do
                                      ON dineId = d.id";

            var stayOptionSQL = @"SELECT 
                                    so.id as stayOptionId,
                                    so.tripId as soTripId,
                                    so.stayId as soStayId,
                                    s.id as stayId,
                                    s.stayName,
                                    s.stayAddress,
                                    s.stayImage,
                                    s.stayDescription,
                                    s.stayNotes
                                    FROM [Stay] s           
                                    LEFT JOIN [StayOptions] so
                                    ON stayId = s.id";

            var activtiesOptionSQL = @"SELECT 
                                        ao.id as aoId,
                                        ao.tripId as aoTripId,
                                        ao.activityId as aoActivityId,
                                        a.id as activityId,
                                        a.activityName,
                                        a.activityAddress,
                                        a.activityImage,
                                        a.activityDescription,
                                        a.activityNotes
                                        FROM [Activity] a           
                                        LEFT JOIN [ActivityOptions] ao
                                        ON activityId = a.id";

            using var tripCommand = connection.CreateCommand();
            tripCommand.CommandText = tripSQL;
            using var tripReader = tripCommand.ExecuteReader();
           

            using var dineCommand = connection.CreateCommand();
            dineCommand.CommandText = dineOptionSQL;
            using var dineReader = dineCommand.ExecuteReader();

            using var stayCommand = connection.CreateCommand();
            stayCommand.CommandText = stayOptionSQL;
            using var stayReader = stayCommand.ExecuteReader();

            using var activityCommand = connection.CreateCommand();
            activityCommand.CommandText = activtiesOptionSQL;
            using var activityReader = activityCommand.ExecuteReader();

            List<Trip> trips = new();

            while (tripReader.Read())
            {

                var trip = new Trip()

                {
                    Id = DbUtils.GetInt(tripReader, "id"),
                    UserId = DbUtils.GetInt(tripReader, "tripUserId"),
                    TripName = DbUtils.GetString(tripReader, "tripName"),
                    PastTrip = DbUtils.GetBoolean(tripReader, "pastTrip"),
                    Description = DbUtils.GetString(tripReader, "description"),
                    Budget = DbUtils.GetInt(tripReader, "budget")

                };


                //trip.DineOptions = getDineOption(dineReader);
                //while loop
                // if trip Id

                //trip.StayOptions = getDineOption();
                //trip.Activity =
                trips.Add(trip);



            }
            
            return trips;


            //            using (var command = connection.CreateCommand())
            //            {
            //                command.CommandText = @" SELECT
            //                                                t.id as tripId,
            //                                                t.userId as tripUserId,
            //                                                t.tripName,
            //                                                t.pastTrip,
            //                                                t.description as tripDisciption,
            //                                                t.budget tripBudget,
            //                                                do.id as dineOptionsId,
            //                                                do.tripId as doTripId,
            //                                                do.dineId as doDineId,
            //                                                d.id as dineId,
            //                                                d.dineName,
            //                                                d.dineAddress,
            //                                                d.dineImage,
            //                                                d.dineDescription,
            //                                                d.dineNotes,
            //                                                so.id as stayOptionId,
            //                                                so.tripId as soTripId,
            //                                                so.stayId as soStayId,
            //                                                s.id as stayId,
            //                                                s.stayName,
            //                                                s.stayAddress,
            //                                                s.stayImage,
            //                                                s.stayDescription,
            //                                                s.stayNotes,
            //                                                ao.id as aoId,
            //                                                ao.tripId as aoTripId,
            //                                                ao.activityId as aoActivityId,
            //                                                a.id as activityId,
            //                                                a.activityName,
            //                                                a.activityAddress,
            //                                                a.activityImage,
            //                                                a.activityDescription,
            //                                                a.activityNotes
            //                                            FROM [Trip] t
            //                                            LEFT JOIN [DineOptions] do
            //                                            ON t.id = do.tripId
            //                                            LEFT JOIN [Dine] d
            //                                            ON dineId = d.id
            //                                            LEFT JOIN [StayOptions] so
            //                                            ON t.id = so.tripId
            //                                            LEFT JOIN [Stay] s 
            //                                            ON s.id = stayId
            //                                            LEFT JOIN [ActivityOptions] ao 
            //                                            ON t.id = ao.tripId
            //                                            LEFT JOIN [Activity] a 
            //                                            ON a.id = activityId";

            //                var reader = command.ExecuteReader();
            //                var trips = new List<Trip>();


            //                    while (reader.Read())
            //                    {

            //                    var trip = new Trip()
            //                    {
            //                        Id = DbUtils.GetInt(reader, "id"),
            //                        UserId = DbUtils.GetInt(reader, "tripUserId"),
            //                        TripName = DbUtils.GetString(reader, "tripName"),
            //                        PastTrip = DbUtils.GetBoolean(reader, "pastTrip"),
            //                        Description = DbUtils.GetString(reader, "description"),
            //                        Budget = DbUtils.GetInt(reader, "budget"),


            //                    };

            //                      //  DineOptions = new List<Dine>(),

            //                       // if (DbUtils.IsNotDbNull(reader, "dineId"))

            ////trips.Add(trip);
            //                    }

            //                reader.Close();
            //                return trips;
            //            }

        }

        // private List<Dine> getDineOption(SqlDataReader reader, int tripId)
        // {
        //    while (reader.Read())
        //    {
        //        var dine =
        //    }
        //trip 
        //}

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
