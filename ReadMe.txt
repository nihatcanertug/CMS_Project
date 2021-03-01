


Documantasyon

1. CMS_Project adinda bir blank solution acilir.

2. İlk katman olarak Entity isminde Class Library (.NetCore) projesi eklenir.
	
	2.1. Enums klasoru acilir.İçine Status adinda sinif eklenir ve tipi enum olarak isaretlenir.

	 public enum Status { Active=1, Modified=2, Passive=3   }

	2.2. Entities klasoru acilir ve concrete ve ınterface klasorleri acilir ve concrete'e varliklar,Interface'e de Genel olarak kullandigimiz ozellikler icin IBaseEntity sinifi acilir ve interface olarak isaretlenir.

	public interface IBaseEntity
    {
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? DeleteDate { get; set; }
        Status Status { get; set; }
    }

    2.3. Concrete siniflarimiz acilir,ozellikleri eklenir ve interface siniftan kalitim verilerek bagimliligi en az'a indirme kuralina uyulur ve ozellikler kalitim yoluyla aktarilmis olur.

    public class Page : IBaseEntity
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "Must type a title")]
        [MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "Must type a title")]
        [MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Content { get; set; }

        public int? Sorting { get; set; }
     
        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }

    public class AppUser: IdentityUser, IBaseEntity
    {

        // Features that the IdentityUser class does not contain but need in the project can be added here.
        public string Occupation { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }

     public class Category:IBaseEntity
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Must type a title")]
        [MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only allowed letters")]
        public string Name { get; set; }
        public string Slug { get; set; }

        //IBaseEntity'den gelen özellekler
        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        //public string slug { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }

        public virtual List<Product> Products { get; set; }
    }

     public class Product:IBaseEntity
    {
     
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Mininum lenght is 2")]
        public string Name { get; set; }

        [Required, MinLength(2, ErrorMessage = "Mininum lenght is 2")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "You must to choose a category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
=======

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
>>>>>>> d00dc905af9b4c7ede55e2c4d429b45cc2e4afdd

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }

    2.4. Product'lar için ekledigimiz image'in uzantisinin "jpg","png","jpeg" olmasi icin kural yazdik.Bu kuralda bir file eklendiginde extension yani uzantisini aliyoruz ve istedigimiz kriteri sagliyorsa onaylayacak ve ekleyecek,farkli bir uzantili olan bir dosya eklenirse error message gondererek hatali bir formatta ekleme yaptigini soyleyen bir uyari mesaji alacak.

     public class FileExtensionAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result) new ValidationResult(ErrorMessage = "Allowed extension are jpg, png, jpeg");
            }

            return ValidationResult.Success;
        }
    }

    3. CMS_Project.Map (Class Library .NetCore) adinda bir katman açilir ve burada entity'lerin Mapping islemi yapilir.

    3.1. CMS_Project.Entity katmani referanslarina eklenir ve Microsoft.EntityFrameworkCore paketi nuget package manager'dan yuklenir.

    3.2. Abstract klasoru acilir.Abstract olarak isaretlenmis BaseMap.cs sinifi yaratilir.Varliklarin ortak ozellikleri burada Map edilir.Ayrica ilgili sinif diger concrete map siniflarininda atasi olarak kullanilir.
    
     public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.DeleteDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
        }
    }

    3.3. Concrete klasoru acilir.Projede kullanilan varliklarin mapping islemleri burada gerceklestirilir.

    public class AppUserMap:BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Occupation).IsRequired(true);
            base.Configure(builder);
        }
    }

     public class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired(true);
            base.Configure(builder);
        }
    }

    public class PageMap:BaseMap<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Slug).IsRequired(false);
            builder.Property(x => x.Sorting).IsRequired(false);
            builder.Property(x => x.Content).IsRequired(true);
            base.Configure(builder);
        }
    }

     public class ProductMap:BaseMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.UnitPrice).IsRequired(true);
            builder.Property(x => x.Image).IsRequired(true);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            base.Configure(builder);
        }

    }

    3.4. CMS_Project.Data (Class Library .NetCore) adinda bir katman açilir ve burada Repository'ler Migration islemleri icin Context klasoru ve data tohumlamasi yapmak icin SeedData klasoru acilir.

    3.5. CMS_Project.Entity ve CMS_Project.Map katmanlari referanslara eklenir.

    3.6. Gerekli olan Microsoft.EntityFrameworkCore.SqlServer ve Microsoft.EntityFrameworkCore.Tools paketleri yuklenir.

    3.7.SeedData klasoru eklenir.SeedPages.cs sinifi icerisinde veritabanina gonderilecek test veri seti olusturulur.

    public class SeedPages:IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasData(
                new Page { Id = 1, Title = "Home", Content = "Home Page", Slug = "home-page", Sorting = 100 },
                new Page { Id = 2, Title = "About Us", Content = "About Us Page", Slug = "about-page", Sorting = 100 },
                new Page { Id = 3, Title = "Services", Content = "Services Page", Slug = "service-page", Sorting = 100 },
                new Page { Id = 4, Title = "Contact Us", Content = "Conctact Us Page", Slug = "contact-page", Sorting = 100 });
        }
    }
    
    3.8. Context klasoru olusturulur.ApplicationDbContext.cs sinifi yaratilir.Varliklar DbSet edilir.Map islemleri implement edilir.SeedData eklenir.

    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new PageMap());
            builder.ApplyConfiguration(new ProductMap());         
            builder.ApplyConfiguration(new SeedPages());

            base.OnModelCreating(builder);

            //builder.Entity<Product>().Property(x => x.UnitPrice).HasColumnType("decimal");          
            //builder.Entity<IBaseEntity>().Property(x => x.CreateDate).HasColumnType("datetime2");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }

    3.9. Repositories klasoru acilir,icine interface ve concrete klasorleri eklenir.Bu projedeki CRUD operasyonlarini "Task" olarak isaretleyecegiz yani asenkron programming yapacagiz.Asenkron programming yazilan birden fazla  linq to sorgusunun calismasina ve bunlarin birbirlerini beklemeden,birbirlerine ne yapiyorlar diye bakmadan calismasini saglar.Birden fazla Task(iş)'i yonetebiliriz ve birbirinden bagimsiz calistiklari icin herhangi biri patlasa uygulama durmaz diger tasklar calismaya devam eder.Kisaca es zamanli olarak islemlerin yurutulmesi islemine denir.

    3.10. Interface klasorunun icine Base klasoru acilir ve IKernelRepository acilir.

      public interface IKernelRepository<T> where T : IBaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T> FindByDefault(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }

    3.11. EntityTypeRepository klasoru acilir ve varlik repositoryleri base'den kalitim verilir.

    public interface IAppUserRepository: IKernelRepository<AppUser> { } 
    public interface ICategoryRepository: IKernelRepository<Category> { }
    public interface IPageRepository:IKernelRepository<Page> { }
    public interface IProductRepository : IKernelRepository<Product> { }

    3.12. Concrete klasoru acilir ve yazilan interfacelerin govdeleri doldurulur.

     public class KernelRepository<T>: IKernelRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> _table;

        public KernelRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _table = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await _table.AnyAsync(expression);

        public async Task Delete(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindByDefault(Expression<Func<T, bool>> expression) => await _table.FirstAsync(expression);

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();

        public async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task<T> GetById(int id) => await _table.FindAsync(id);

        public async Task Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    4. CMS_Project.Web (Asp.Net Core web application) adinda MVC katmani acilir ve Data,Entity katmanlari referans olarak eklenir.

    4.1. Startup'daki IoContainer'a gidip bagimliligi en az'a indirmek icin new'lemek yerine gidip container'da (register) tanimliyoruz ve resolve(cozunme islemini gerceklestiriyoruz.).Bu sayede Controller'da interface'ini kullanabileceğiz ve resolve yaptigimizdan dolayi da concrete'in ozelliklerini interfaceler uzerinden kullanacagiz.Projede belirledigimiz bagimli siniflari core'un sagladigi gomulu ioc'da register ettik cunku ihtiyacimiz olan yerde new'lemek yerine constructer da inject edebileyim.

    public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache(); //Github April 4, bir makalede gereksiz olduğunu gördüm
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(30);
                //opt.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            services.AddControllersWithViews();
            services.AddRouting(options => options.LowercaseUrls = true);
            //services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAppUserRepository , AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPageRepository    , PageRepository>();
            services.AddScoped<IProductRepository , ProductRepository>();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

        4.2. Migration islemi icin appsettings.json 'da Connectionimizi yaziyoruz ve migration islemi gerceklestirilir.

        "AllowedHosts": "*",
        "ConnectionStrings": {
        "DefaultConnection": "Server=NIHOO;Database=CMSDb;uid=nihatcan;pwd=123;"
  }

        Microsoft.Entity.FrameworkCore.Design (5.0.2) paketi yuklenir.

        Package Manager Console 'da ilk once add-migration InitialCreate ardindan update-database yazilir.

        4.3. Areas klasoru acilir,Admin area eklenir.Startup.cs sinifina giderek admin area icin bir endpoint yazilir.

        endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        4.4. Global View altindaki Shared + _ViewImports + _ViewStart dosyalari alinarak Admin area altindaki Views'a eklenir. 

        4.5. Admin klasorune PageController eklenir ve CRUD islemleri icin gerekli kodlar yazilir.

        {
    [Authorize(Roles = "employee")]
    [Authorize(Roles = "admin")]
    [Authorize(Roles = "member")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;
        public PageController(IPageRepository pageRepository) => _pageRepository = pageRepository;

        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null) return View(await _pageRepository.FirstOrDefault(x => x.Slug == "home-page"));

            Page page = await _pageRepository.FirstOrDefault(x => x.Slug == slug);

            if (page == null) return NotFound();

            return View(page);
        }

    }

    4.6. Yazdigimiz Page method'a Razor View ekledik
    .@model Page
    @{
    ViewData["Title"] = "Page";
    }

    @Html.Raw(Model.Content)

    4.7. _ViewImports'a kullanacagimiz klasorleri ekledik.

    @using CMS_Project.Web
    @using CMS_Project.Web.Models
    @using CMS_Project.Entity.Entities.Concrete
    @using CMS_Project.Web.Models.DTOs
    @using CMS_Project.Web.Models.VMs
    @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers

    4.8.Admin->Controller-> CategoryController acilir ve CRUD operasyonlari icin asenkron methodlar yazilir.

     public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;


        public CategoryController(ICategoryRepository categoryRepository) => _categoryRepo = categoryRepository;
              
        public IActionResult Create() =>  View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = await _categoryRepo.FirstOrDefault(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exsist..!");
                    TempData["Warning"] = "The category already exsist..!";
                    return View(category);
                }
                else
                {
                    await _categoryRepo.Add(category);
                    TempData["Success"] = "The category added..!";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = "The category hasn't been added..!";
                return View(category);
            }
        }

        public async Task<IActionResult> List() => View(await _categoryRepo.Get(x => x.Status != Status.Passive));

        public async Task<IActionResult> Edit(int id) => View(await _categoryRepo.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = _categoryRepo.FindByDefault(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "There is already a category..!");
                    TempData["Warning"] = "The page  is already exsist..!";
                    return View(category);
                }
                else
                {
                    category.UpdateDate = DateTime.Now;
                    category.Status = Status.Modified;
                    await _categoryRepo.Update(category);
                    TempData["Success"] = "The category has been edited..!";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = "The category hasn't been edited..!";
                return View(category);
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            Category category = await _categoryRepo.GetById(id);
            if (category != null)
            {
                await _categoryRepo.Delete(category);
                TempData["Success"] = "The category deleted..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The category hasn't been deleted..!";
                return RedirectToAction("List");
            }

         }
    }

    4.9. Yazilan methodlari kullaniciya gostermek icin view'lari yazilir.

    @model Category
@{
    ViewData["Title"] = "Edit";
}

<div class="row mt-2">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header">
                <h4 class="card-title">Edit Category</h4>
            </div>
            <div class="card-body">
                <form asp-action="Edit">
                    <input type="hidden" asp-for="Id" />
                    <input type="hidden" asp-for="Slug" />
                    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
                    <div class="form-group">
                        <label asp-for="Name" class="control-label"></label>
                        <input asp-for="Name" class="form-control" />
                        <span asp-validation-for="Name" class="text-danger"></span>
                    </div>
                    <div class="form-group float-right">
                        <input type="submit" value="Edit" class="btn btn-primary" />
                    </div>
                </form>
            </div>
            <div class="card-footer">
                <a asp-action="List" class="btn btn-block btn-dark">Back to List</a>
            </div>
        </div>
    </div>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}


   4.10. Cart operasyonları:
    
    Sepet uzerinde yapacagımız CRUD işlemlerini session yardimiyla browser local storage icerisinde depolayacagiz.

   4.11. Models->Extensions->SessionExtension sinifi acilir.

    public static class SessionExtention
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }

  





<<<<<<< HEAD
=======
5.18.3. Let's create a model for cart transactions. Models => Dtos => CartItem.cs opens.
>>>>>>> d00dc905af9b4c7ede55e2c4d429b45cc2e4afdd
