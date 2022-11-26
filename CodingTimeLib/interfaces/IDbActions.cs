using CodingTimeLib.db;
using CodingTimeLib.model;

namespace CodingTimeLib.interfaces;

public interface IDbActions
{
    void AddSession(CodingSession session);
    CodingDTO GetOneSession(int id);
    List<CodingDTO> GetAllSessions();
    void DeleteSession(int id);
    void UpdateSession(int id, CodingSession session);
}
