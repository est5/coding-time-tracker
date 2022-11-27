namespace CodingTimeLib.model;

public class CodingSession
{

    public CodingSession() { }

    public CodingSession(string startTime, string endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = CalculateTime();
        Id = Random.Shared.Next();
    }
    public int Id { get; private set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Duration { get; private set; }

    private int CalculateTime()
    {
        var start = DateTime.Parse(StartTime);
        var end = DateTime.Parse(EndTime);

        var diff = end.Subtract(start).TotalHours;
        return (int)diff;
    }

}
