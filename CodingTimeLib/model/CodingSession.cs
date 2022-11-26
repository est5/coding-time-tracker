namespace CodingTimeLib.model;

public class CodingSession
{
    private static int _id = 0;

    public CodingSession() { }

    public CodingSession(string startTime, string endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = CalculateTime();
        Id = _id++;
    }
    public int Id { get; private set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Duration { get; private set; }

    private int CalculateTime()
    {
        return 0;
    }

}
