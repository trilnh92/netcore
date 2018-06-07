# netcore
This is my assignment for ASP.NET Core 2.0 Intensive course internal. 

“Building a simple Blog Engine using ASP.NET Core”.

REQUIREMENTS:

For blog owner:

Login/Logout
Manage categories (Name, Description)
Manage post (Slug, ShortDescription, Content, ThumbnailImage, CreatedDate, UpdatedDate)
Manage comments (optional)

For internet users:

Home page: lasted posts, categories menu
View posts by category
View post details
Add comments (optional)

The project should apply as much techniques of ASP.NET MVC Core as possible. For example: TagHelpers, ViewComponents, Razor Pages and have Unit Test. The Unit Test don’t need to have a high coverage number but should demonstrate the ability to write unit test for common components: Controllers, ViewComponents, Services, …
Some people might prefer SPA. They can use any SPA frameworks but only for the back-office (blog owner). The front-office (internet users) must use Razor Views

INSTRUCTIONS:

1/Please make sure your connection strings in appsettings.json point to a local SQL Server instance.

2/Open a command prompt in the Web folder (~\WebBlog\WebBlog) and execute the following commands:

dotnet restore
dotnet ef database update -c WebBlogDbContext -p ../WebBlog.Database/WebBlog.Database.csproj -s WebBlog.csproj
dotnet ef database update -c ApplicationDbContext -p ../WebBlog.Database/WebBlog.Database.csproj -s WebBlog.csproj

3/If you need to create migrations, you can use these commands:

-- create migration (from Web folder CLI)
dotnet ef migrations add InitialModel --context WebBlogDbContext -p ../WebBlog.Database/WebBlog.Database.csproj -s WebBlog.csproj -o Data/Migrations
dotnet ef migrations add InitialIdentityModel --context ApplicationDbContext -p ../WebBlog.Database/WebBlog.Database.csproj -s WebBlog.csproj -o Data/Migrations

4/I built the client SPA with technologies: (React + webpack + typescript). 
    #Why react? With my opinions. React just a library, it's lighter than Angular framework. React render via DOM vitual tree. Render performance of it, is better than Angular.
    #Why webpack? With my opinions. Webpack is helpful to bundle javascript and css. It's help performance loading faster. (https://medium.com/the-self-taught-programmer/what-is-webpack-and-why-should-i-care-part-1-introduction-ca4da7d0d8dc)
    #Why typescript? With my opinions. Typescript support OOP programming (interface, abstract) and type checking. It's useful when we want to apply Dependency Injection pattern.

5/ Open a command promt in the Web SPA folder (~\WebBlog\WebBlog.SPA) and execute the following commands:
    npm install 
    npm run-script build
    npm start
