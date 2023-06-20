
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
                                t.budget as tripBudget
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
                    Id = DbUtils.GetInt(tripReader, "tripId"),
                    UserId = DbUtils.GetInt(tripReader, "tripUserId"),
                    TripName = DbUtils.GetString(tripReader, "tripName"),
                    PastTrip = DbUtils.GetBoolean(tripReader, "pastTrip"),
                    Description = DbUtils.GetString(tripReader, "tripDisciption"),
                    Budget = DbUtils.GetInt(tripReader, "tripBudget")

                };


                trip.DineOptions = GetDineOptions(dineReader, trip.Id);
                trip.StayOptions = GetStayOptions(stayReader, trip.Id);
                trip.ActvitityOptions = GetActivities(activityReader, trip.Id);

                trips.Add(trip);



            }

            return trips;

        }

        private static List<Stay> GetStayOptions(SqlDataReader reader, int tripId)
        {
            var stayOptions = new List<Stay>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(reader.GetOrdinal("soTripId")) && DbUtils.GetInt(reader, "soTripId") == tripId)
                {
                    var stayOption = new Stay()
                    {
                        Id = DbUtils.GetInt(reader, "stayId"),
                        StayName = DbUtils.GetString(reader, "stayName"),
                        StayAddress = DbUtils.GetString(reader, "stayAddress"),
                        StayImage = DbUtils.GetString(reader, "stayImage"),
                        StayDescription = DbUtils.GetString(reader, "stayDescription"),
                        StayNotes = DbUtils.GetString(reader, "stayNotes")
                    };

                    stayOptions.Add(stayOption);
                }
            }

            return stayOptions;
        }

        private static List<Activity> GetActivities(SqlDataReader reader, int tripId)
        {
            var activities = new List<Activity>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(reader.GetOrdinal("aoTripId")) && DbUtils.GetInt(reader, "aoTripId") == tripId)
                {
                    var activity = new Activity()
                    {
                        Id = DbUtils.GetInt(reader, "activityId"),
                        ActivityName = DbUtils.GetString(reader, "activityName"),
                        ActivityAddress = DbUtils.GetString(reader, "activityAddress"),
                        ActivityImage = DbUtils.GetString(reader, "activityImage"),
                        ActivityDescription = DbUtils.GetString(reader, "activityDescription"),
                        ActivityNotes = DbUtils.GetString(reader, "activityNotes")
                    };

                    activities.Add(activity);
                }
            }

            return activities;
        }

        private static List<Dine> GetDineOptions(SqlDataReader reader, int tripId)
        {
            var dineOptions = new List<Dine>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(reader.GetOrdinal("doTripId")) && DbUtils.GetInt(reader, "doTripId") == tripId)
                {
                    var dineOption = new Dine()
                    {
                        Id = DbUtils.GetInt(reader, "dineId"),
                        DineName = DbUtils.GetString(reader, "dineName"),
                        DineAddress = DbUtils.GetString(reader, "dineAddress"),
                        DineImage = DbUtils.GetString(reader, "dineImage"),
                        DineDescription = DbUtils.GetString(reader, "dineDescription"),
                        DineNotes = DbUtils.GetString(reader, "dineNotes")
                    };

                    dineOptions.Add(dineOption);
                }
            }

            return dineOptions;
        }

        // private List<Dine> getDineOption(SqlDataReader reader, int tripId)
        // {
        //    while (reader.Read())
        //    {
        //        var dine =
        //    }
        //trip 
        //}

        public List<Trip> GetByUserId(int tripUserId)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT 
                                        t.id as tripId,
                                        t.userId as tripUserId,
                                        t.tripName,
                                        t.pastTrip,
                                        t.description as tripDescription,
                                        t.budget as tripBudget,
                                        ao.id as aoId,
                                        ao.tripId as aoTripId,
                                        ao.activityId as aoActivityId,
                                        a.id as activityId,
                                        a.activityName,
                                        a.activityAddress,
                                        a.activityImage,
                                        a.activityDescription,
                                        a.activityNotes,
                                        so.id as soId,
                                        so.tripId as soTripId,
                                        so.stayId as soStayId,
                                        s.id as stayId,
                                        s.stayName,
                                        s.stayAddress,
                                        s.stayImage,
                                        s.stayDescription,
                                        s.stayNotes,
                                        do.id as doId,
                                        do.tripId as doTripId,
                                        do.dineId as doDineId,
                                        d.id as dineId,
                                        d.dineName,
                                        d.dineAddress,
                                        d.dineImage,
                                        d.dineDescription,
                                        d.dineNotes
                                    FROM [Trip] t
                                    LEFT JOIN [ActivityOptions] ao ON t.id = ao.tripId
                                    LEFT JOIN [Activity] a ON ao.activityId = a.id
                                    LEFT JOIN [StayOptions] so ON t.id = so.tripId
                                    LEFT JOIN [Stay] s ON so.stayId = s.id
                                    LEFT JOIN [DineOptions] do ON t.id = do.tripId
                                    LEFT JOIN [Dine] d ON do.dineId = d.id
                                    WHERE t.userId = @userId";

                    DbUtils.AddParameter(command, "@userId", tripUserId);

                    var reader = command.ExecuteReader();
                    var trips = new List<Trip>();

                    while (reader.Read())
                    {
                        var tripId = DbUtils.GetInt(reader, "tripId");
                        var existingTrip = trips.FirstOrDefault(t => t.Id == tripId);

                        if (existingTrip == null)
                        {
                            var trip = new Trip()
                            {
                                Id = tripId,
                                UserId = DbUtils.GetInt(reader, "tripUserId"),
                                TripName = DbUtils.GetString(reader, "tripName"),
                                PastTrip = DbUtils.GetBoolean(reader, "pastTrip"),
                                Description = DbUtils.GetString(reader, "tripDescription"),
                                Budget = DbUtils.GetInt(reader, "tripBudget"),
                                ActvitityOptions = new List<Activity>(),
                                StayOptions = new List<Stay>(),
                                DineOptions = new List<Dine>()
                            };

                            trips.Add(trip);
                            existingTrip = trip;
                        }

                        var activityId = DbUtils.GetInt(reader, "activityId");
                        if (activityId != 0)
                        {
                            var activity = new Activity()
                            {
                                Id = activityId,
                                ActivityName = DbUtils.GetString(reader, "activityName"),
                                ActivityAddress = DbUtils.GetString(reader, "activityAddress"),
                                ActivityImage = DbUtils.GetString(reader, "activityImage"),
                                ActivityDescription = DbUtils.GetString(reader, "activityDescription"),
                                ActivityNotes = DbUtils.GetString(reader, "activityNotes")
                            };

                            existingTrip.ActvitityOptions.Add(activity);
                        }

                        var stayId = DbUtils.GetInt(reader, "stayId");
                        if (stayId != 0)
                        {
                            var stay = new Stay()
                            {
                                Id = stayId,
                                StayName = DbUtils.GetString(reader, "stayName"),
                                StayAddress = DbUtils.GetString(reader, "stayAddress"),
                                StayImage = DbUtils.GetString(reader, "stayImage"),
                                StayDescription = DbUtils.GetString(reader, "stayDescription"),
                                StayNotes = DbUtils.GetString(reader, "stayNotes")
                            };

                            existingTrip.StayOptions.Add(stay);
                        }

                        var dineId = DbUtils.GetInt(reader, "dineId");
                        if (dineId != 0)
                        {
                            var dine = new Dine()
                            {
                                Id = dineId,
                                DineName = DbUtils.GetString(reader, "dineName"),
                                DineAddress = DbUtils.GetString(reader, "dineAddress"),
                                DineImage = DbUtils.GetString(reader, "dineImage"),
                                DineDescription = DbUtils.GetString(reader, "dineDescription"),
                                DineNotes = DbUtils.GetString(reader, "dineNotes")
                            };

                            existingTrip.DineOptions.Add(dine);
                        }
                    }

                    reader.Close();
                    return trips;
                }
            }
        }

        public List<Dine> GetDineOptionsByTripId(int tripId)
        {
            using var connection = Connection;
            connection.Open();

            var dineOptions = new List<Dine>();

            var sql = @"SELECT DISTINCT
                    d.id as dineId,
                    d.dineName,
                    d.dineAddress,
                    d.dineImage,
                    d.dineDescription,
                    d.dineNotes
                FROM [DineOptions] do
                JOIN [Dine] d ON do.dineId = d.id
                WHERE do.tripId = @tripId";

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@tripId", tripId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var dineOption = new Dine()
                {
                    Id = DbUtils.GetInt(reader, "dineId"),
                    DineName = DbUtils.GetString(reader, "dineName"),
                    DineAddress = DbUtils.GetString(reader, "dineAddress"),
                    DineImage = DbUtils.GetString(reader, "dineImage"),
                    DineDescription = DbUtils.GetString(reader, "dineDescription"),
                    DineNotes = DbUtils.GetString(reader, "dineNotes")
                };

                dineOptions.Add(dineOption);
            }

            return dineOptions;
        }

        public List<Stay> GetStayOptionsByTripId(int tripId)
        {
            using var connection = Connection;
            connection.Open();

            var stayOptions = new List<Stay>();

            var sql = @"SELECT DISTINCT
                    s.id as stayId,
                    s.stayName,
                    s.stayAddress,
                    s.stayImage,
                    s.stayDescription,
                    s.stayNotes
                FROM [StayOptions] so
                JOIN [Stay] s ON so.stayId = s.id
                WHERE so.tripId = @tripId";

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@tripId", tripId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var stayOption = new Stay()
                {
                    Id = DbUtils.GetInt(reader, "stayId"),
                    StayName = DbUtils.GetString(reader, "stayName"),
                    StayAddress = DbUtils.GetString(reader, "stayAddress"),
                    StayImage = DbUtils.GetString(reader, "stayImage"),
                    StayDescription = DbUtils.GetString(reader, "stayDescription"),
                    StayNotes = DbUtils.GetString(reader, "stayNotes")
                };

                stayOptions.Add(stayOption);
            }

            return stayOptions;
        }

        public List<Activity> GetActivityOptionsByTripId(int tripId)
        {
            using var connection = Connection;
            connection.Open();

            var activityOptions = new List<Activity>();

            var sql = @"SELECT DISTINCT
                    a.id as activityId,
                    a.activityName,
                    a.activityAddress,
                    a.activityImage,
                    a.activityDescription,
                    a.activityNotes
                FROM [ActivityOptions] ao
                JOIN [Activity] a ON ao.activityId = a.id
                WHERE ao.tripId = @tripId";

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@tripId", tripId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var activityOption = new Activity()
                {
                    Id = DbUtils.GetInt(reader, "activityId"),
                    ActivityName = DbUtils.GetString(reader, "activityName"),
                    ActivityAddress = DbUtils.GetString(reader, "activityAddress"),
                    ActivityImage = DbUtils.GetString(reader, "activityImage"),
                    ActivityDescription = DbUtils.GetString(reader, "activityDescription"),
                    ActivityNotes = DbUtils.GetString(reader, "activityNotes")
                };

                activityOptions.Add(activityOption);
            }

            return activityOptions;
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
