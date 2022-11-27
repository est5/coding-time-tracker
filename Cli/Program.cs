using Cli.Ui;
using CodingTimeLib.db;

var db = new SQLiteController();

Ui.Menu(db);
