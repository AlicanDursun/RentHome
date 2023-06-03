namespace RentAHome.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserType>()
            //  .HasKey(w => new { w.Id });
            //modelBuilder.Entity<User>()
            //    .HasKey(w => new { w.Id });
            ////modelBuilder.Entity<Image>()
            ////  .HasKey(w => new { w.Id, w.HouseId });
            //modelBuilder.Entity<HouseType>()
            // .HasKey(w => new { w.Id });

            //modelBuilder.Entity<HouseFeature>()
            //.HasKey(w => new { w.Id });

            ////modelBuilder.Entity<HouseAddress>()
            ////.HasKey(w => new { w.Id });
            ////modelBuilder.Entity<House>()
            ////    .HasOne<HouseAddress>(w => w.HouseAddress)
            ////    .WithOne(w => w.House)
            ////    .HasForeignKey<HouseAddress>(w => w.HouseId);
            //// modelBuilder.Entity<House>().HasAnnotation("SqlServer:Identity", "1,1")
            ////.HasKey(w => new { w.Id, w.HouseAddressId, w.HouseFeatureId, w.HouseTypeId });
            //modelBuilder.Entity<Favorite>().HasKey(w => new { w.HouseId });

            //modelBuilder.Entity<UserType>().HasData(
            //    new UserType
            //    {
            //        Id = 1,
            //        TypeName = "Admin"
            //    },
            //    new UserType
            //    {
            //        Id = 2,
            //        TypeName = "Agent"
            //    },
            //    new UserType
            //    {
            //        Id = 3,
            //        TypeName = "Customer"
            //    });

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        UserId = 2,
            //        FirstName = "Alican",
            //        LastName = "Dursun",


            //    });
            //modelBuilder.Entity<HouseType>().HasData(
            //    new HouseType
            //    {
            //        Id = 1,
            //        TypeName = "Apart"
            //    },
            //    new HouseType
            //    {
            //        Id = 2,
            //        TypeName = "Dublex"
            //    },
            //    new HouseType
            //    {
            //        Id = 3,
            //        TypeName = "Triplex"
            //    });
            //modelBuilder.Entity<Country>().HasData(
            //    new Country
            //    {
            //        Id = 1,
            //        Name = "Turkey",

            //    });
            //modelBuilder.Entity<City>().HasData(
            //    new City
            //    {
            //        Id = 1,
            //        CountryId = 1,
            //        Name = "Antalya",

            //    });
            //modelBuilder.Entity<HouseAddress>().HasData(
            //    new HouseAddress
            //    {
            //        HouseId = 1,
            //        Street = "Çağlayan Mahallesi 2052 Sokak 35/4",
            //        ZipCode = "07230",
            //        CountryId = 1,
            //        CityId = 1

            //    });
            //modelBuilder.Entity<HouseFeature>().HasData(
            //    new HouseFeature
            //    {
            //        HouseId = 1,
            //        Furnished = true,
            //        NaturalGas = true,
            //        Price = 15.550m,
            //        BalconyCount = 3,
            //        RoomCount = 4,
            //        SquareMeters = 250
            //    });
            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        UserId = 1,
            //        Email = "test@test.com",
            //        Password = "1",
            //        DateCreated = DateTime.Now,
            //    });
            //modelBuilder.Entity<House>().HasData(
            //    new House
            //    {
            //        Id = 1,
            //        HouseTitle = "test",
            //        HouseDescription = "testdescription",
            //        UserId = 1
            //    });

            //modelBuilder.Entity<Image>().HasData(
            //   new Image
            //   {
            //       Id = 1,
            //       HouseId = 1,
            //       ImageUrl = "images/5.jpg"

            //   });
        }
        public DbSet<User> Users { get; set; }
        //public DbSet<UserType> UserTypes { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HouseAddress> HouseAddresses { get; set; }
        public DbSet<HouseFeature> HouseFeatures { get; set; }
        //public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Images { get; set; }


    }
}
