## Setting Up a SQLite Database with Dotnet

Using a database takes the applications we write as developers to a whole new level! In this simple guide I will explain how to set it up in simple and easy steps.

#### What you'll learn

- How to setup a simple SQLite database.
- How to Create, Read, Update, and Delete data in the database.
- **Advanced Topics**
  - The difference between LINQ and SQL query syntax

### Installing `sqlite-net-pcl`

One of the first things we need is a way to interact with the database.
Run `dotnet add package sqlite-net-pcl` inside your _classlib_ project.
This gives us an easy to use library that abstracts away the complexities so we can to interact with the database.

### Setting up the DB_Conn Class

Now that we have the package installed, We'll start by defining a database connection class called _DB_Conn_ that will hold the methods we will use to interact with the database

```
C#
public class DB_Conn
{
    SQLiteConnection _connection;

    public string ConnectionString { get; private set; } = "YourDatabaseName.sqlite3";
}
```

The code above simply defines a few properties we will use for interacting with our database. The first line `SQLiteConnection _connection;` will be a variable where we will store the connection which we will initialize down below in the next code block. The second line `public string ConnectionString { get; private set; } = "YourDatabaseName.sqlite3";` will be the connection string. This is what allows us to reference what database we want to interact with. Note: You can name _YourDatabaseName_ whatever you like!

```
C#
public void InitialiseConnection()
{
    SQLiteConnectionString options = new SQLiteConnectionString(ConnectionString, false);

    _connection = new SQLiteConnection(options);
}
```

The code above will define the connection options, initialize the database connection and _cache_ the connection in the `_connection` we defined above.

### Creating Tables

In a database, especially a relational database like SQL we need data. In relational databases data is stored in tables. So we will create a class to model our data and use it to create a table in the database.

```
C#
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

```

Now that we have modeled the data in a way that SQLite can understand, we can write a method to create a new table.

```
C#
public void CreateTable()
{
  _connection.CreateTable<Record>();
}
```

### Create - Adding New Data

Great! We now have a database, a data model, and created a method to create a table. Next we need to add data to the database.
To add data to the database we can write another method

```
C#
public void InsertRecord(string name, int age)
{

  var conn = _connection;

  var record = new Record { Id = new Guid(), Name = name, Age = age, Date = DateTime.Now };

  var results = conn.Insert(record);

  conn.Close();
}
```
