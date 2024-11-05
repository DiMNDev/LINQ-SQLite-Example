using SQLite;
namespace Dotnet_SQL_Tutorial.Library;


[Table("records")]
public class Record
{
    [PrimaryKey]
    [Column("id")]
    public Guid Id
    { get; set; }

    [Column("name")]
    public string Name
    { get; set; }

    [Column("age")]
    public int Age
    { get; set; }

    [Column("date")]
    public DateTime Date
    { get; set; }
}

public class DB_Conn
{

    SQLiteConnection _connection;

    public string ConnectionString { get; private set; } = "MyDB.sqlite3";

    public void CreateTable()
    {
        _connection.CreateTable<Record>();
    }

    public void InitialiseConnection()
    {
        SQLiteConnectionString options = new SQLiteConnectionString(ConnectionString, false);

        _connection = new SQLiteConnection(options);
    }

    public List<Record> GetRecords()
    {
        // var options = new SQLiteConnectionString(ConnectionString, false);
        // var conn = new SQLiteConnection(options);
        var conn = _connection;

        List<Record> results = conn.Table<Record>().ToList();

        conn.Close();

        return results;
    }

    public void InsertRecord(string name, int age)
    {
        var options = new SQLiteConnectionString(ConnectionString, false);
        var conn = new SQLiteConnection(options);

        var record = new Record { Id = new Guid(), Name = name, Age = age, Date = DateTime.Now };

        var results = conn.Insert(record);

        conn.Close();
    }

    public void UpdateRecord(Guid id)
    {
        var options = new SQLiteConnectionString(ConnectionString, false);
        var conn = new SQLiteConnection(options);

        var record = new Record { Id = id, Name = "Mark", Age = 23, Date = DateTime.Now };

        var results = conn.Update(record);

        conn.Close();
    }

    public void DeleteRecord(string id)
    {
        var options = new SQLiteConnectionString(ConnectionString, false);
        var conn = new SQLiteConnection(options);

        conn.Delete<Record>(id);
        conn.Close();
    }

}
