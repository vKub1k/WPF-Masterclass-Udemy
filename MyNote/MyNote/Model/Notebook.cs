using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace MyNote.Model;

public class Notebook
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int UserId { get; set; }
    public string Name { get; set; }
}