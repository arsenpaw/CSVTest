Hello, to use that program you should change db connections in appsettings.json file. 
Then you should run the program and you will see the result in the console.
```csharp
{
  "ConnectionStrings": {
    "DefaultConnection": "Server={YoureDB};Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
```
When you run the program you have to open swagger and use endpoints

Currently there is only 2 indexes. One is primary and the other one in on PULocationId becouse of frequent search by that field. 

Q&A
1. If CSV has 10gb ?
In that case I will not use that method of insertation,
 becouse all that entity will be in memory and it will be very slow. Also every once entity is tracked by EF core.
So I will use SqlBulkCopy to insert data to the database. Of course It will work faster than EF core,
 but as developer we wont have so mach controll like validation, model binding and e t c. Also if there will be a 10GB file,
  I wont store that file locally, I will use streaming from Azure Blob storage or smth like that.