using System;
using SQLite;

namespace MyNote.Model;

public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int NoteboookId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public string FileLocation { get; set; }
}