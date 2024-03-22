using Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Catalog.Infrastructure.EntityConfigurations;
using MediatR;
using Catalog.Domain.Entites;

namespace Catalog.Infrastructure;

public class CatalogContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "cat";
    public DbSet<Catalog.Domain.Entites.Product> Products { get; set; }
    public DbSet<Catalog.Domain.Entites.Course> Courses { get; set; }

    private IDbContextTransaction? _currentTransaction;
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {

        System.Diagnostics.Debug.WriteLine("CatalogContext::ctor ->" + this.GetHashCode());

    }
    
    public bool HasActiveTransaction => _currentTransaction != null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LessonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GameEntityTypeConfiguration());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course()
            {
                Id = 1,
                Name = "Easy Alphabet",
                DisplayName = "Bé Học Chữ",
                ImageUrl = "https://monkeymedia.vcdn.com.vn/upload/web/img/01-Game-hoc-chu-cai-tieng-Viet.jpg",
                IsActive = true,
                IsTop = true,
                SortIndex = 1
            },
            new Course()
            {
                Id = 2,
                Name = "Mathematical Thinking",
                DisplayName = "Toán Thông Minh",
                ImageUrl = "https://play-lh.googleusercontent.com/I1YRhi1oTYrFf1ZCbs3Dbx7J3Kj_h5SXICD8ObajQ5NOuYFJLNGCa1a774AD_z7D9w=w526-h296-rw",
                IsActive = true,
                IsTop = false,
                SortIndex = 2,
            });

        modelBuilder.Entity<Lesson>().HasData(
            new Lesson()
            {
                Id = 1,
                Name = "A",
                IsActive = true,
                SortIndex = 1,
                CourseId = 1,
            },
            new Lesson()
            {
                Id = 2,
                Name = "B",
                IsActive = true,
                SortIndex = 2, 
                CourseId = 1,
            }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game()
            {
                Id = 1,
                Name = "Bong Bóng",
                IsActive = true,
                SortIndex = 1,
                LessonId = 1,
            },
            new Game()
            {
                Id = 2,
                Name = "Trúc Xanh",
                IsActive = true,
                SortIndex = 2,
                LessonId = 1,
            },
            new Game()
            {
                Id = 3,
                Name = "Hái Táo",
                IsActive = true,
                SortIndex = 3,
                LessonId = 1,
            },
            new Game()
            {
                Id = 4,
                Name = "Đuổi Hình Bắt Chữ",
                IsActive = true,
                SortIndex = 4,
                LessonId = 1,
            },
            new Game()
            {
                Id = 5,
                Name = "Shopping",
                IsActive = true,
                SortIndex = 5,
                LessonId = 1,
            }
        );
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed

        var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt= DateTimeOffset.Now;
            }
        }

        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
