using ArvitumNew.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArvitumNew
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<InformationChannel> InformationChannels { get; set; }
        public virtual DbSet<TypeWorkPlace> TypeWorkPlaces { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<WorkPlace> WorkPlaces { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<ExaminationBottomPhoto> ExaminationBottomPhotos { get; set; }
        public virtual DbSet<ExaminationBackPhoto> ExaminationBackPhotos { get; set; }
        public virtual DbSet<ExaminationHistory> ExaminationHistorys { get; set; }
        public virtual DbSet<ExaminationStatus> ExaminationStatuss { get; set; }
        public virtual DbSet<ShoesType> ShoesTypes { get; set; }
        public virtual DbSet<ShoesSize> ShoesSizes { get; set; }
        public virtual DbSet<CoatingType> CoatingTypes { get; set; }
        public virtual DbSet<CoatingThickness> CoatingThicknesss { get; set; }
        public virtual DbSet<ScannerResolution> ScannerResolutions { get; set; }
        public virtual DbSet<PayType> PayTypes { get; set; }

        public DbQuery<ExaminationListView> ExaminationListViews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Customer>().Property(u => u.FullName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            modelBuilder.Entity<Customer>().Property(u => u.DateRegistration).HasDefaultValueSql("getdate()"); 
            modelBuilder.Entity<Customer>().HasIndex(u => u.Phone).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Examination>().Property(u => u.DateExamination).HasDefaultValueSql("getdate()"); 
            modelBuilder.Entity<Examination>().Property(u => u.ShoesTypeId).HasDefaultValue(1);
            modelBuilder.Entity<Examination>().Property(u => u.ShoesSizeId).HasDefaultValue(1);
            modelBuilder.Entity<Examination>().Property(u => u.CoatingTypeId).HasDefaultValue(1);
            modelBuilder.Entity<Examination>().Property(u => u.CoatingThicknessId).HasDefaultValue(1);
            modelBuilder.Entity<Examination>().Property(u => u.ExaminationStatusId).HasDefaultValue(1);
            modelBuilder.Entity<Examination>().Property(u => u.Activ).HasDefaultValue(true);
            modelBuilder.Entity<Examination>().Property(u => u.Express).HasDefaultValue(false);

            modelBuilder.Entity<ExaminationHistory>().Property(u => u.DateChangeStatus).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ExaminationBottomPhoto>().Property(u => u.ShoesSizeLId).HasDefaultValue(1);
            modelBuilder.Entity<ExaminationBottomPhoto>().Property(u => u.ShoesSizeRId).HasDefaultValue(1);

            modelBuilder.Entity<Customer>().Property(u => u.InformationChannelId).HasDefaultValue(1);

            modelBuilder.Entity<ShoesType>().Property(u => u.Activ).HasDefaultValue(true);
            modelBuilder.Entity<ShoesType>().HasIndex(u => u.ShoesTypeName).IsUnique();

            modelBuilder.Entity<ShoesSize>().Property(u => u.Activ).HasDefaultValue(true);
            modelBuilder.Entity<ShoesSize>().HasIndex(u => u.FootLength).IsUnique();
            modelBuilder.Entity<ShoesSize>().HasIndex(u => u.Size).IsUnique();

            modelBuilder.Entity<InformationChannel>().Property(u => u.Activ).HasDefaultValue(true);
            modelBuilder.Entity<InformationChannel>().HasIndex(u => u.InformationChannelName).IsUnique();

            modelBuilder.Entity<CoatingThickness>().Property(u => u.Activ).HasDefaultValue(true);

            modelBuilder.Entity<CoatingType>().Property(u => u.Activ).HasDefaultValue(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
