using Dotnet_SQL_Tutorial.Library;
Console.WriteLine("Hello, World!");

DB_Conn dBConn = new();

dBConn.InitialiseConnection();

try
{
    dBConn.InsertRecord("Jane", 25);
}
catch (System.Exception)
{
    Console.WriteLine("Data already exists");
}

List<Record> data = dBConn.GetRecords();

Console.WriteLine($"{data[0].Name} is {data[0].Age}");