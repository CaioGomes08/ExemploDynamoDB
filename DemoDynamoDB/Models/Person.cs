using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

[DynamoDBTable("persons")]
public class Person
{
    [DynamoDBHashKey("id")]
    public string? Id { get; set; }

    [DynamoDBProperty("name")]
    public string Name { get; set; }

    [DynamoDBProperty("age")]
    public int Age { get; set; }

    [DynamoDBProperty("fone")]
    public string? Fone { get; set; }

    [DynamoDBProperty("address")]
    public Address Address { get; set; }
}

public class Address
{
    [DynamoDBProperty("street")]
    public string Street { get; set; }

    [DynamoDBProperty("number")]
    public int Number { get; set; }
}
