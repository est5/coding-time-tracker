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
            coding.Prepare();

            SqliteParameter startParam = new SqliteParameter("@start", SqliteType.Text);
            idParam.Value = session.Id;
            coding.Parameters.Add(startParam);
            coding.Prepare();

            SqliteParameter endParam = new SqliteParameter("@end", SqliteType.Text);
            idParam.Value = session.Id;
            coding.Parameters.Add(endParam);
            coding.Prepare();

            SqliteParameter durationParam = new SqliteParameter("@duration", SqliteType.Integer);
            idParam.Value = session.Id;
            coding.Parameters.Add(durationParam);
            coding.Prepare();


            coding.ExecuteNonQuery();
        }
    }

    public List<CodingSession> GetAllSessions()
    {
        throw new NotImplementedException();
    }

    public CodingSession GetOneSession(int id)
    {
        throw new NotImplementedException();
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
            )";

            SqliteParameter idParam = new SqliteParameter("@id", SqliteType.Integer);
            idParam.Value = id;
            delSession.Parameters.Add(idParam);
            delSession.Prepare();

            delSession.ExecuteNonQuery();
        }
    }

    public void UpdateSession(int id)
    {
        throw new NotImplementedException();
    }

    private void InitDb()
    {
        using (var connection = new SqliteConnection(_conStr))
        {
            connection.Open();

            var createCodingTimeTable = connection.CreateCommand();
            createCodingTimeTable.CommandText = @"
            CREATE TABLE IF NOT EXISTS coding(
            session_id integer PRIMARY KEY,
            start_time text ,
            end_time text ,
            duration integer
            )";

            createCodingTimeTable.ExecuteNonQuery();
        }
    }
}
