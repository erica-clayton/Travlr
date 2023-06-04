using System;
using Travlr.Utils;
using Travlr.Models;

namespace Travlr.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(IConfiguration configuration) : base(configuration) { }

        public List<Activity> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT
                                                id,
                                                activityName,
                                                activityAddress,
                                                activityImage,
                                                activityDescription,
                                                activityNotes
                                            FROM [Activity]";

                    var activities = new List<Activity>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var activity = new Activity()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ActivityName = DbUtils.GetString(reader, "activityName"),
                            ActivityAddress = DbUtils.GetString(reader, "activityAddress"),
                            ActivityImage = DbUtils.GetString(reader, "activityImage"),
                            ActivityDescription = DbUtils.GetString(reader, "activityDescription"),
                            ActivityNotes = DbUtils.GetString(reader, "activityNotes")
                        };

                        activities.Add(activity);
                    }

                    reader.Close();
                    return activities;
                }
            }
        }

        public Activity GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"id,
                                                activityName,
                                                activityAddress,
                                                activityImage,
                                                activityDescription,
                                                activityNotes
                                            FROM [Activity]
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", id);

                    var reader = command.ExecuteReader();

                    Activity activity = null;
                    if (reader.Read())
                    {
                        activity = new Activity()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ActivityName = DbUtils.GetString(reader, "activityName"),
                            ActivityAddress = DbUtils.GetString(reader, "activityAddress"),
                            ActivityImage = DbUtils.GetString(reader, "activityImage"),
                            ActivityDescription = DbUtils.GetString(reader, "activityDescription"),
                            ActivityNotes = DbUtils.GetString(reader, "activityNotes")
                        };
                    }

                    reader.Close();
                    return activity;
                }
            }
        }

        public void Add(Activity category)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [Activity]
                                                (activityName,
                                                activityAddress,
                                                activityImage,
                                                activityDescription,
                                                activityNotes)
                                            OUTPUT INSERTED.ID
                                            VALUES
                                                (@activityName,
                                                 @activityAddress,
                                                 @activityImage,
                                                 @activityDescription,
                                                 @activityNotes)";

                    DbUtils.AddParameter(command, "@activityName", category.ActivityName);
                    DbUtils.AddParameter(command, "@activityAddress", category.ActivityAddress);
                    DbUtils.AddParameter(command, "@activityImage", category.ActivityImage);
                    DbUtils.AddParameter(command, "@activityDescription", category.ActivityDescription);
                    DbUtils.AddParameter(command, "@activityNotes", category.ActivityNotes);

                    category.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Activity activity)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Activity]
                                                SET activityName = @activityName,
                                                    activityAddress = @activityAddress,
                                                    activityImage = @activityImage,
                                                    activityDescription = @activityDescription,
                                            WHERE id = @id";

                    DbUtils.AddParameter(command, "@id", activity.Id);
                    DbUtils.AddParameter(command, "@userId", activity.ActivityName);
                    DbUtils.AddParameter(command, "@tripName", activity.ActivityAddress);
                    DbUtils.AddParameter(command, "@pastTrip", activity.ActivityImage);
                    DbUtils.AddParameter(command, "@description", activity.ActivityDescription);
                    DbUtils.AddParameter(command, "@budget", activity.ActivityNotes);


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
                    command.CommandText = "DELETE FROM [Activity] WHERE id = @id";
                    DbUtils.AddParameter(command, "@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}


