using CodingTimeLib.db;
using CodingTimeLib.model;

var db = new SQLiteController();

var session = new CodingSession("start", "end");
db.AddSession(session);

var s = db.GetOneSession(0);
System.Console.WriteLine(s.EndTime);

var sessions = db.GetAllSessions();
System.Console.WriteLine(sessions[0].EndTime);

session.EndTime = "ennnnd";
db.UpdateSession(0, session);

s = db.GetOneSession(0);
System.Console.WriteLine(s.EndTime);

db.DeleteSession(0);

s = db.GetOneSession(0);
System.Console.WriteLine(s.EndTime);
