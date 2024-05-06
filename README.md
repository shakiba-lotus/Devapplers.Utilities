
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
### 1. Database Interactions with `DevApplers.Utilities.Db
The `DevApplers.Utilities.Db` class provides a simplified wrapper for Entity Framework, offering CRUD (Create, Read, Update, Delete) operations and lazy loading capabilities.

**Important Note:** This class assumes you have already configured your Entity Framework context and data model.

 For a traditional ASP.NET project (not ASP.NET Core), integrating the C# class library with database interactions would involve adding a line to the `Configuration` method in the `Global.asax` file (assuming you're using a web application) and configuring the connection string in the `web.config` file. Here's a breakdown of the steps:

#### Configuration in Global.asax:

- Open the `Global.asax` file in your ASP.NET project.
- Locate the `Configuration` method. It's typically called within the `Application_Start` event handler.
- Add the following line to establish the database context using your library's `Db` class:
```cs
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

```cs
<configuration>
    <connectionStrings>
        <add name="YourConnectionStringName" 
             connectionString="server=your_server_address;database=your_database;Integrated Security=True"
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    </configuration>
```
- Replace the placeholders (`YourConnectionStringName`, `server=your_server_address`, etc.) with your actual connection string details.


#### - Usage Examples:

- **Get:** Retrieve entities by ID or using custom filters:

```cs
// Get a user by ID with eager loading
User user = Db.Get<User>(1);

//  Get all users with lazy loading (data retrieved only when accessed)
IEnumerable<User> users = Db.Get<User>();
```

- **Create:** Add a new entity to the database:

```cs
User newUser = new User { Name = "John Doe" };
Db.Create(newUser);
```

- **Edit:** Modify existing entities:

```cs
User userToUpdate = _db.Get<User>(2);
userToUpdate.Email = "new_email@example.com";
Db.Edit(userToUpdate);
```

- **Delete:** Remove entities from the database:
```cs
User userToDelete = _db.Get<User>(3);
Db.Delete(userToDelete);
```

 #### - Lazy Loading:

The `ApiGet` methods are similar to `Get` but leverage lazy loading. Data associated with the entity (e.g., related objects) is retrieved only when explicitly accessed, improving performance for large datasets.

```cs
// Get a user with lazy loading
User user = _db.ApiGet<User>(1);

// Accessing the user posts will trigger the actual data retrieval
IEnumerable<Post> userPosts = user.Posts;
```
#### - Saving Changes:

This wrapper automatically tracks changes made to entities. You don't need to call `db.SaveChanges()` explicitly after each operation. 

### CascadeDropDown Class
The `CascadeDropDown` class in the `DevApplers.ClassLibrary.Utilities`  provides functionalities for handling cascading dropdown lists in ASP.NET MVC applications. 
- Simplifies the process of implementing cascading dropdown lists in your ASP.NET MVC applications.
- Promotes code reusability and reduces boilerplate code.

#### Features:
- Filter data in a secondary table based on a primary key value from another table.

- Convert a collection of selected items from the secondary table into a `SelectList` object suitable for use in dropdown lists (MVC views).

#### Usage:

The `CascadeDropDown` class offers two static methods:

##### 1. GetCascadingItems<T>(int id, string fk):
- Filters records from the second table (`T`) based on a primary key value (`id`).
- The parameter `fk` specifies the name of the foreign key property in the second table that references the primary key of the first table.
- Returns an `IList<T>` containing the filtered records from the second table.

###### Example:

```cs
// Assuming Customer has an Orders collection with a Foreign Key 'CustomerID'
int selectedCustomerID = 10;
IList<Order> customerOrders = CascadeDropDown.GetCascadingItems<Order>(selectedCustomerID, "CustomerID");

// Use customerOrders in your application logic or to populate a view
```
##### 2. IEnumerable<T>.ToCascade<T>(Func<T, string> textField, Func<T, string> valueField):

- This extension method converts a collection of items from the second table (`T`) into a `SelectList` object.
- The `textField` parameter is a delegate function that specifies how to get the text displayed for each item in the dropdown list.
- The `valueField` parameter is a delegate function that specifies how to get the value associated with each item in the dropdown list.
- Returns an `IEnumerable<SelectListItem>` representing the dropdown list options.

```cs
IEnumerable<SelectListItem> orderOptions = selectedOrders.ToCascade(
    order => order.ProductName,
    order => order.OrderID.ToString()
);

// Use orderOptions in your MVC view to populate a dropdown list
```

### PersianMonths Class
This class provides functionalities for generating a dropdown list populated with the months of the year in the Persian language, suitable for use in ASP.NET MVC applications.

#### Features:
- Simplifies the process of creating Persian month dropdown lists in your ASP.NET MVC applications.  (فروردین to اسفند)
- Promotes code reusability and reduces boilerplate code.
- Offers flexibility in choosing between controller or view-based generation of the dropdown list.

#### Usage:

1. Include the Namespace:

```cs 
using DevApplers.ClassLibrary.Utilities; // Import the namespace in your view
```

2. Generate and Use the SelectList in Your View (Two Options):

- **Option 1:** Using the Function in the Controller (if needed):
```cs
// In your controller (if preferred)
public ActionResult MyAction()
{
    SelectList monthSelectList = PersianMonths.GetSelectList();
    return View(monthSelectList);
}

// In your view (if using controller-generated list)
@model SelectList

<h2>Select a Month</h2>
@Html.DropDownList("SelectedMonth", Model)
```
- **Option 2:** Direct Function Call in the View (recommended):
```cs
@using DevApplers.ClassLibrary.Utilities // Import the namespace

<h2>Select a Month</h2>
@Html.DropDownList("SelectedMonth", PersianMonths.GetSelectList())
```



### Contributing:
We welcome contributions to improve and expand the DevApplers.Utilities library!
