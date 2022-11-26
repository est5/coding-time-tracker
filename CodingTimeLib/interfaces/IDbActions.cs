using CodingTimeLib.model;

namespace CodingTimeLib.interfaces;

public interface IDbActions
{
    void AddSession(CodingSession session);
    CodingSession GetOneSession(int id);
    List<CodingSession> GetAllSessions();
    void RemoveSession(int id);
    void UpdateSession(int id);
}
