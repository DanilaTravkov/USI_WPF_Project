by Danila Travkov sv86-2023, specification for USI project.

------------------------------------------------------------------------------------------------------------------------------------------
I wrote this to clarify how the project should be structured and what naming conventions we will follow to better understand each other's code (All points marked with a "*" are recommendations, however I insist that we stick to all of these rules in the future):

------------------------------------------------------------------------------------------------------------------------------------------
1. How we write our code

-1*. Names of private fields (properties, methods and variables) should begin with a _ and their public counterparts without a _ but they obviously should have the same name, example: 

private string? _Email; 
public string? Email { get => _Email, set => _Email = value };

-2. Methods' names should begin with a capital letter (CamelCase), so should properties' names

-3. Model classes need to have a public access modifier (by default they are internal, this has caused many errors in previous versions of our project)

-4*. The more comments you add the better, this applies to .cs, .xaml and .xaml.cs files.

-5. In .xaml files if a component has a name it should follow this template: "Purpose_NameOfComponent", for example: <ListView x:Name="UserListView"> or "UserList" ...

-6. For collections which are used to display data in views use ObservableCollection<T> type	

------------------------------------------------------------------------------------------------------------------------------------------
2. SQLite - a file-based db which is perfect for us since it won't require any cloud solutions like hosting a db server.

Install what is needed:

1. Click on the project and find "manage NuGet packages"
2. In the Browse panel find Microsoft.Data.Sqlite.Core and Microsoft.EntityFrameworkCore.Sqlite and Microsoft.EntityFrameworkCore.Tools install them

Documentation: 
https://learn.microsoft.com/ru-ru/dotnet/standard/data/sqlite/?tabs=netcore-cli
https://docs.microsoft.com/ef/core/
https://learn.microsoft.com/ru-ru/ef/core/

------------------------------------------------------------------------------------------------------------------------------------------
3. After you make ANY changes to entities (classes in Model folder) you have to create and apply migrations to ensure that the database is up-to-date with the code:

-1. Navigate to Tools > NuGet Package Manager > Package Manager Console
-2. Create a migration, run: add-migration "migration name"
-3. Apply migrations, run: update-database
-4. Enjoy life!

------------------------------------------------------------------------------------------------------------------------------------------
4. WPF does not provide session storing so to keep the current logged-in user I've created the UserSession class in Session folder with a static field which represent a singleton object of a user session.
Use it to get data about the current user.

------------------------------------------------------------------------------------------------------------------------------------------
5.
Log in as a teacher:

login: john@gmail.com
password: 123

Log in as a student:

login: test@gmail.com
password: 123
