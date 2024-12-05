using Microsoft.EntityFrameworkCore;
using project_sem4_api.Entities;

namespace project_sem4_api.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Evaluate> Evaluates { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }  
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<PaymentMethord> PaymentMethords { get; set; }
        public virtual DbSet<Restaurant_Table> Restaurant_Tables { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StatusDish> StatusDishes { get; set; }
        public virtual DbSet<StatusNotifacation> StatusNotifacations { get; set; }
        public virtual DbSet<StatusOrder> StatusOrders { get; set; }
        public virtual DbSet<StatusTable> StatusTables { get; set; }
        public virtual DbSet<TimeEmployee> TimeEmployees { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình quan hệ giữa Order và OrderItems
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.orderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
