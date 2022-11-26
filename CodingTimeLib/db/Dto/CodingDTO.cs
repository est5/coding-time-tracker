namespace CodingTimeLib.db;

public class CodingDTO
{
    public CodingDTO()
    {
    }

    public CodingDTO(int id, string startTime, string endTime, int duration)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
    }

    public int Id { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Duration { get; set; }

}
