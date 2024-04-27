
## DevApplers.Utilities 
(.net core version and NuGet Package Coming Soon!)

This project provides a collection of reusable C# classes designed to streamline common development tasks and enhance developer productivity.

#### Key Features
- **Database Interactions:** Effortlessly interact with databases using a simplified wrapper for Entity Framework.

- **File Management:** Manage files and directories efficiently.
- **Security Utilities:** Enhance application security with encryption and hashing functions.
- **Email Sending:** Simplify sending emails with a dedicated class.
- **Data Serialization:** Easily serialize 

#### Benefits and Advantages
- **Increased Efficiency:** Focus on core logic by leveraging pre-built functionalities.
- **Improved Code Quality:** Well-written, documented code ensures readability and maintainability.
- **Reduced Boilerplate:** Eliminates the need for repetitive `db.SaveChanges()` calls.
- **Lazy Loading Support:** Provides efficient data retrieval with lazy loading options.
- **Flexibility:** Works with various entity types without code modifications.


### Getting Started:

#### Prerequisites
- .NET Framework 4.8 

#### Installation:

Clone or download the project source code.
Build the project using Visual Studio or the command line:
```sh
git clone https://github.com/shakiba-lotus/Devapplers.Utilities.git
```

##### Building the Library (Optional):
If you prefer to build the library from source code:

1. Clone or download the source code for the DevApplers.Utilities library.
2. Open the solution file (e.g., DevApplers.Utilities.sln) in Visual Studio.
3. Ensure all project references are resolved (if applicable).
4. Build the solution:
    - In Visual Studio: Go to Build -> Build Solution (or press F6).
    - From the command line: Navigate to the solution folder and run msbuild DevApplers.Utilities.sln 

**Note:** Building the library is only necessary if you want to use the source code directly. 

##### Adding a Reference to Your Project:

If you built the library from source code (or have the compiled DLLs):

1. Right-click on the References node of your project in Solution Explorer.
2. Select Add Reference....
3. Go to the Browse tab.
4. Navigate to the folder containing the compiled DevApplers.Utilities.dll file.
5. Select the DLL and click Add.
This will add the necessary reference to your project, allowing you to use the classes and functionalities provided by the DevApplers.Utilities library.



### Usage
## 1. Database Interactions with `DevApplers.Utilities.Db
The `DevApplers.Utilities.Db` class provides a simplified wrapper for Entity Framework, offering CRUD (Create, Read, Update, Delete) operations and lazy loading capabilities.

**Important Note:** This class assumes you have already configured your Entity Framework context and data model.

 For a traditional ASP.NET project (not ASP.NET Core), integrating the C# class library with database interactions would involve adding a line to the `Configuration` method in the `Global.asax` file (assuming you're using a web application) and configuring the connection string in the `web.config` file. Here's a breakdown of the steps:

#### Configuration in Global.asax:

- Open the `Global.asax` file in your ASP.NET project.
- Locate the `Configuration` method. It's typically called within the `Application_Start` event handler.
- Add the following line to establish the database context using your library's `Db` class:
```bash
protected void Application_Start(object sender, EventArgs e)
{
    // ... other application startup logic

    string connectionStringName = "YourConnectionStringName"; // Replace with actual name

    Db.SetContext(connectionStringName);
}
```

#### Connection String Configuration (web.config):

- Open the `web.config` file in your ASP.NET project.
- Locate the `<connectionStrings>` section within the `<configuration>` element.
- Locate your database connection string name, specifying the name you'll reference in the `Db.SetContext` line:

```bash
<configuration>
    <connectionStrings>
        <add name="YourConnectionStringName" 
             connectionString="server=your_server_address;database=your_database;Integrated Security=True"
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    </configuration>
```
- Replace the placeholders (`YourConnectionStringName`, `server=your_server_address`, etc.) with your actual connection string details.


### - Usage Examples:

- **Get:** Retrieve entities by ID or using custom filters:

```bash
// Get a user by ID with eager loading
User user = Db.Get<User>(1);

//  Get all users with lazy loading (data retrieved only when accessed)

IEnumerable<User> users = Db.Get<User>();
```

- **Create:** Add a new entity to the database:

```bash
User newUser = new User { Name = "John Doe" };
Db.Create(newUser);
```

- **Edit:** Modify existing entities:

```bash
User userToUpdate = _db.Get<User>(2);
userToUpdate.Email = "new_email@example.com";
Db.Edit(userToUpdate);
```

- **Delete:** Remove entities from the database:
```bash
User userToDelete = _db.Get<User>(3);
Db.Delete(userToDelete);
```

 ### - Lazy Loading:

The `ApiGet` methods are similar to `Get` but leverage lazy loading. Data associated with the entity (e.g., related objects) is retrieved only when explicitly accessed, improving performance for large datasets.

```bash
// Get a user with lazy loading
User user = _db.ApiGet<User>(1);

// Accessing the user posts will trigger the actual data retrieval
IEnumerable<Post> userPosts = user.Posts;
```
### - Saving Changes:

This wrapper automatically tracks changes made to entities. You don't need to call `db.SaveChanges()` explicitly after each operation. 


### Contributing:
We welcome contributions to improve and expand the DevApplers.Utilities library!
