using CodingTimeLib.interfaces;
using CodingTimeLib.model;
using Microsoft.Data.Sqlite;

namespace CodingTimeLib.db;

public class SQLiteController : IDbActions
{
    private static string _conStr = "Data Source=codingTime.db";

    public SQLiteController()
    {
        InitDb();
    }

    public void AddSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var coding = connection.CreateCommand();
            coding.CommandText = @"
            INSERT INTO coding(session_id,start_time,end_time,duration)
            VALUES(
                @id, @start, @end, @duration
            )
            ";

            SqliteParameter idParam = new SqliteParameter("@id", SqliteType.Integer);
            idParam.Value = session.Id;
            coding.Parameters.Add(idParam);

            SqliteParameter startParam = new SqliteParameter("@start", SqliteType.Text);
            startParam.Value = session.StartTime;
            coding.Parameters.Add(startParam);

            SqliteParameter endParam = new SqliteParameter("@end", SqliteType.Text);
            endParam.Value = session.EndTime;
            coding.Parameters.Add(endParam);

            SqliteParameter durationParam = new SqliteParameter("@duration", SqliteType.Integer);
            durationParam.Value = session.Duration;
            coding.Parameters.Add(durationParam);

            coding.Prepare();
            coding.ExecuteNonQuery();
        }
    }

    public List<CodingDTO> GetAllSessions()
    {
        var sessions = new List<CodingDTO>();


        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var getOneSession = connection.CreateCommand();
            getOneSession.CommandText = @"
            SELECT session_id, start_time, end_time, duration coding FROM coding
            ";

            var reader = getOneSession.ExecuteReader();
            while (reader.Read())
            {
                var session = new CodingDTO();
                session.Id = Convert.ToInt32(reader[0]);
                session.StartTime = (string)reader[1];
                session.EndTime = (string)reader[2];
                session.Duration = Convert.ToInt32(reader[3]);
                sessions.Add(session);
            }

        }

        return sessions;
    }

    public CodingDTO GetOneSession(int id)
    {
        var session = new CodingDTO();


        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var getOneSession = connection.CreateCommand();
            getOneSession.CommandText = @"
            SELECT session_id, start_time, end_time, duration FROM coding
            WHERE session_id = @id
            ";

            SqliteParameter idParam = new SqliteParameter("@id", SqliteType.Integer);
            idParam.Value = id;
            getOneSession.Parameters.Add(idParam);

            getOneSession.Prepare();
            var reader = getOneSession.ExecuteReader();
            while (reader.Read())
            {
                session.Id = Convert.ToInt32(reader[0]);
                session.StartTime = (string)reader[1];
                session.EndTime = (string)reader[2];
                session.Duration = Convert.ToInt32(reader[3]);
            }

        }

        return session;
    }

    public void DeleteSession(int id)
    {
        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var delSession = connection.CreateCommand();
            delSession.CommandText = @"
            DELETE FROM coding
            WHERE session_id = @id
            ";

            SqliteParameter idParam = new SqliteParameter("@id", SqliteType.Integer);
            idParam.Value = id;
            delSession.Parameters.Add(idParam);

            delSession.Prepare();
            delSession.ExecuteNonQuery();
        }
    }

    public void UpdateSession(int id, CodingSession session)
    {
        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var updSession = connection.CreateCommand();
            updSession.CommandText = @"
            UPDATE coding
            SET start_time = @start, end_time = @end, duration = @duration
            WHERE session_id = @id
            ";

            SqliteParameter idParam = new SqliteParameter("@id", SqliteType.Integer);
            idParam.Value = id;
            updSession.Parameters.Add(idParam);

            SqliteParameter startParam = new SqliteParameter("@start", SqliteType.Text);
            startParam.Value = session.StartTime;
            updSession.Parameters.Add(startParam);

            SqliteParameter endParam = new SqliteParameter("@end", SqliteType.Text);
            endParam.Value = session.EndTime;
            updSession.Parameters.Add(endParam);

            SqliteParameter durationParam = new SqliteParameter("@duration", SqliteType.Integer);
            durationParam.Value = session.Duration;
            updSession.Parameters.Add(durationParam);

            updSession.Prepare();
            updSession.ExecuteNonQuery();
        }
    }

    private void InitDb()
    {
        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var createCodingTimeTable = connection.CreateCommand();
            createCodingTimeTable.CommandText = @"
            CREATE TABLE IF NOT EXISTS coding(
            session_id text PRIMARY KEY,
            start_time text ,
            end_time text ,
            duration integer
            )";

            createCodingTimeTable.ExecuteNonQuery();
        }
    }
}
