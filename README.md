Hello, to use this program you should change db connections in appsettings.json file. 
Then you should run the program. You will see the result in the console.
```csharp
{
  "ConnectionStrings": {
    "DefaultConnection": "Server={YourDB};Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
```
When you run the program you have to open Swagger and use endpoints

Currently there is only 2 indexes. One is primary and the other one in on PULocationId becouse of frequent search by that field. 

Q&A
1. What if the CSV file is 10GB?
In this case, I would not use the current method of insertion because all the entities would need to be loaded into memory, making the process very slow. Additionally, each entity would be tracked by EF Core, which adds significant overhead.
Instead, I would use SqlBulkCopy to insert the data directly into the database. This method is much faster than using EF Core. However, as a developer, you lose some control over aspects like validation, model binding, and other EF Core features.
For such a large file, I would also avoid storing the file locally. Instead, I would use streaming from Azure Blob Storage or a similar service to process the data efficiently without requiring local storage.