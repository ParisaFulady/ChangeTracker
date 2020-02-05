using AChangeTracker.Entities;
using BChangeTracker.DAL;
using System;
using System.Linq;

namespace CChangeTracker.EndPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new TeacherContext();
            //add(ctx);
            var teach = ctx.Teacher.FirstOrDefault();
            update(ctx, teach);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void update(TeacherContext ctx, Teacher teach)
        {
            teach.FirstName = "نیکا";
            ctx.SaveChanges();
            teach.LastName = "After Update";
            ctx.SaveChanges();
            teach.FirstName = "پریسا";
            ctx.SaveChanges();
        }

        private static void add(TeacherContext ctx)
        {
            ctx.Teacher.Add(new Teacher
            {
                FirstName = "نیکا",
                LastName = "نیکا"
            });
            ctx.Teacher.Add(new Teacher
            {
                FirstName = "پرکیا",
                LastName = "پریکا"
            });

            ctx.SaveChanges();
        }
    }
}
