

Documantation
Documantation
1. Add an empty solution named YMS5177_CMS.
2. Under Solution, a project named YMS5177_CMS.Entity is added in Class Library (.Net Core) project type.
2.1. Install the packages below.
2.1.1. Microsoft.AspNetCore.Http.Features
2.1.2. Microsoft.Extensions.Identity.Stores
2.2. The Enums folder opens. The Status.cs class is added as enum type.
2.3. The Entities folder opens.
2.3.1. The Interfaces folder opens. The IBaseEntity.cs class opens.

public interface IBaseEntity
{
DateTime CreateDate {get; set; }
DateTime? UpdateDate {get; set; }
DateTime? DeleteDate {get; set; }
Status Status {get; set; }
}

2.3.2. The concrete folder opens. The assets needed in the project are created according to the principles of OOP and SOLID, as well as a relational database.
Note: Since we will use the Core.Identity class in this project, we have to act in the concrete folder for the needs. For example, the Core.Identity class offers us many ready-made tables and structures. Therefore, we will not be able to print Id from the BaseEntity.cs class to all tables as you are used to. Because Identity class has its own Id in string type.
2.4. The Extention folder opens.
3. Under Solution, a project named YMS5177_CMS.Map is added in Class Library (.Net Core) project type.
3.1. Install the following packages
3.1.1. Microsoft.EntityFrameworkCore
3.2. Add YMS5177_CMS.Entity project to related layer references
3.3. The Mapping folder opens.
3.3.1. The Abstract folder opens. Create BaseMap.cs class marked as abstract. Common properties of entities are mapped here. Also, the related class is used as an ancestor of other concrete map classes.
3.3.2. The concrete caliper is opened. The assets used in the project are mapped here.
4. Under Solution, a project named YMS5177_CMS.Data is added in Class Library (.Net Core) project type.
4.1. Install the following packages
4.1.1. Microsoft.AspNetCore.Identity.EntityFrameworkCore
4.1.2. Microsoft.EntityFrameworkCore.SqlServer
4.1.3. Microsoft.EntityFrameworkCore.Tools
4.1.4. Microsoft.EntityFrameworkCore.Proxies (For Layz Loading)
4.2. Add the following projects to the references of the relevant layer
4.2.1. YMS5177_CMS.Entity layer
4.2.2. YMS5177_CMS.Map layer
4.3. The SeedData folder is added. The test data set to be sent to the database is created in the SeedPages.cs class.
4.4. Context folder is created. The ApplicationDbContext.cs class is created. The corresponding class IdentityDbContext is inherited. Assets are DbSet. Map operations are implemented. SeedData is added.
4.5. The Repositories folder opens. We will mark the CRUD operations in this project as "Task", that is, we will do asynchronous programming. CRUD operation of a specific entity has been executed, you will say what is the need for this in the controler. However, the operations we will do here will allow a few linq to queries to run on our page in the advanced stages of the project, and they will work without waiting for each other and looking at each other. We've been discussing this issue since a very early date and remember, we were giving a Linkedin example.
4.5.1. The Interfaces caliper opens.
4.5.1.1. Base caliper opens.
4.5.1.2. EntityTypeRepositories opens.
4.5.2. The concrete folder opens.
4.5.2.1. Base caliper opens. BaseRespository.cs is added.
4.5.2.2. EntityTypeRepositories opens.
5. A layer of Asp .Net Core Web Application type named YMS5177_CMS.Web is added under Solution.
5.1. Install the following packages
5.1.1.
5.1.2.
5.2. Add the following projects to the relevant layer as a reference
5.2.1. YMS5177_CMS.Data
5.2.2. YMS5177_CMS.Entity
5.3. I have registered to Startup.cs class, my dependent classes in my project to Built-in IoC container. Thus, instead of new, I will prepare my dependent classes for use with constructor injection instead.

            services.AddDbContext <ApplicationDbContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));

            services.AddScoped <IAppUserRepository, AppUserRepository> ();
            services.AddScoped <ICategoryRepository, CategoryRepository> ();
            services.AddScoped <IPageRepository, PageRepository> ();
            services.AddScoped <IProductRepository, ProductRepository> ();

5.4. Go to appsettings.json file and write default connection string.
5.5. The migrations process is executed.
5.6. The Areas folder opens.
5.6.1. Admin area is added.
5.6.2. An endpoint routing for the Admin area is done by going to the Stratup.cs class. When we add the area, the MvcRoute that comes in the ScaffoldingReadMe.txt file is not used. The following code block startup.cs is added.

endpoints.MapControllerRoute (
                    name: "areas",
                    pattern: "{area: exists} / {controller = Home} / {action = Index} / {id?}");

5.6.2. Obtaining "Shared + _ViewImports + _ViewStart" files from Global View. It is pasted under Views under the Admin area.
5.6.3. The PageController.cs is added and CRUD operations are executed for the page entity.
5.6.4. CategoryController.cs is added and CRUD operations are executed for the page entity.
5.6.5. ProductController.cs is added and CRUD operations are executed for the page entity.
5.7. Models => Components folder opens => MainMenuViewComponent.cs class is added. This class inherits from the "ViewComponent.cs" class.
5.8. Shared => Components folder opens => ManinMenu folder opens => Default partial view opens.
5.9. The viewcomponent is placed in _Layout.cshtml by design.
5.10. Models => Components => CategoriesViewComponent.cs opens and inherits from the "ViewComponent.cs" class.
5.11. Shared => Components => Categories caliper opens => Default partial view opens.
5.12. A viewcomponent is placed in _Layout.cshtml by design.
5.13. Controllers => ProductController.cs opens. Action methods are written to bring products according to all products and categories. View pages are opened for the relevant action methods.
5.14. Add the following endpoints to the cofigure method to fetch products according to their categories and go to the relevant page from Navigation.

endpoints.MapControllerRoute (
                    "page",
                    "{slug?}",
                    defaults: new {controller = "Page", action = "Page"});

                endpoints.MapControllerRoute (
                    "product",
                    "product / {categorySlug}",
                    defaults: new {controller = "Product", action = "ProductByCategory"});

5.15. Controllers => AccountController.cs opens. The models needed for Register, Login and User Edit operations are stored in the "Dtos" folder under the "Models" folder and the relevant CRUD operations are executed.

Note: For the register operation, go to the database and make the entities in the "dbo.AspNetUser" table nullable.

Note: Perform the required register and resolver operation in the Built-In container.

services.AddIdentity <AppUser, IdentityRole> (opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores <ApplicationDbContext> ()
            .AddDefaultTokenProviders ();

5.16. The following steps are followed for the approved deletion operation.
5.16.1. The following codo block is added to the main javasctipt file of the project, namely "site.js".

if ($ ("a.confirmDeletion")) (
$ ("a.confirmDeletion"). click (() => (
if (! confirm ("Confirm Deletion")) (
return false;
}
});
}
5.17.2. It is added to the "remove links" class sections, especially in the listing areas of the assets where needed in the project.

<td>
               <a asp-action="Remove" asp-route-id="@item.Id" class="btn btn-danger confirmDeletion"> Remove </a>
            </td>

5.17. Role Management operation
5.17.1. AdminAreas => Controllers => RoleController.cs is added. Role Create, Index and Edit operations are performed within these controls.
Note: Assigning roles to existing users will be done in the edit actionmethod under RoleController.cs. For this, follow the steps below.
5.17.2. Models => TagHelpers folder opens. => RoleTagHelper.cs opens. The purpose here is to use the users with the roler in the table where the roles are listed, without causing line repetition.

5.18. Cart operations
Warning: We will store the CRUD operations on the basket in the browser local storage with the help of the session. For this process, follow the steps below.
5.18.1. The Models => Extensions folder opens. The SessionExtention.cs class opens.

public static class SessionExtention
{
public static void SetJson (this ISession session, string key, object value)
{
session.SetString (key, JsonConvert.SerializeObject (value));
}

public static T GetJson <T> (this ISession session, string key)
{
var sessionData = session.GetString (key);
return sessionData == null? default (T): JsonConvert.DeserializeObject <T> (sessionData);
}
}

5.18.2. Session is registered in the Startup.cs class.

services.AddSession (opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays (30);
                //opt.IdleTimeout = TimeSpan.FromSeconds (10);
            });

5.18.3. Let's create a model for cart transactions. Models => Dtos => CartItem.cs opens.