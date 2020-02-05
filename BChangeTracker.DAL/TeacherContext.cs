using AChangeTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace BChangeTracker.DAL
{
    public class TeacherContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var auditableEntity = modelBuilder.Model.GetEntityTypes().Where(c => typeof(IAuditable02).IsAssignableFrom(c.ClrType));
            foreach (var item in auditableEntity)
            {
                modelBuilder.Entity(item.ClrType).Property<int>("insertby");
                modelBuilder.Entity(item.ClrType).Property<int>("Updateby");
                modelBuilder.Entity(item.ClrType).Property<DateTime>("insertDate");
                modelBuilder.Entity(item.ClrType).Property<DateTime>("UpdateDate");


            }
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           

            optionsBuilder.UseSqlServer("Server=.;Database=ChangeTrackerDB;User Id = sa;Password = ABCabc123456;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Teacher> Teacher { get; set; }
       public DbSet<Student> Students { get; set; }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(c => typeof(IAuditable).IsAssignableFrom(c.Entity.GetType()));
            var TechReplace = ChangeTracker.Entries().Where(c => typeof(Teacher).IsAssignableFrom(c.Entity.GetType())).Select(c => c.Entity).ToList();
            LogContext logContext = new LogContext();

            foreach (var item in TechReplace)
            {
                if(item is Teacher)
                {
                    var track = item as Teacher;

                    track.FirstName = track.FirstName.Replace("ي", "ی");
                    track.FirstName = track.FirstName.Replace("ﻛ", "ک");
                    track.LastName = track.LastName.Replace("ي", "ی");
                    track.LastName = track.LastName.Replace("ﻛ", "ک");
                }
            }
            foreach (var item in entities)
            {
                var temp = item.Entity as IAuditable;
                var Teacher = item.Entity as Teacher;
                if (item.State==EntityState.Added)
                {
                    temp.InsertBy = 1;
                    temp.InertDate = DateTime.Now;
                    temp.UpdateBy = 1;
                    temp.UpdateDate = DateTime.Now;
                }
                if(item.State==EntityState.Modified||item.State==EntityState.Added)
                {
                    temp.UpdateBy = 1;
                    temp.UpdateDate = DateTime.Now;
                    var serilaizeData = JsonConvert.SerializeObject(Teacher);
                    logContext.DataChangeHistory.Add(new DataChangeHistory
                    {
                         EtityID = Teacher.TeacherID.ToString(),
                         EntityType= Teacher.GetType().FullName,
                         RegistrationDate=DateTime.Now,
                         SerializeData= serilaizeData

                    });
                    
                }
            }
            logContext.SaveChanges();
            return base.SaveChanges();
        }


    }
}
