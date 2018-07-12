namespace NewTrashCollector.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewTrashCollector.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NewTrashCollector.Models.ApplicationDbContext";
        }

        protected override void Seed(NewTrashCollector.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if(context.PickUpDays.ToList().Count == 0)
            {
                PickUpDay pickUp = new PickUpDay();
                pickUp.Day = DayOfWeek.Monday;
                context.PickUpDays.Add(pickUp);
                context.SaveChanges();

                PickUpDay pickUp2 = new PickUpDay();
                pickUp2.Day = DayOfWeek.Tuesday;
                context.PickUpDays.Add(pickUp2);
                context.SaveChanges();

                PickUpDay pickUp3 = new PickUpDay();
                pickUp3.Day = DayOfWeek.Wednesday;
                context.PickUpDays.Add(pickUp3);
                context.SaveChanges();

                PickUpDay pickUp4 = new PickUpDay();
                pickUp4.Day = DayOfWeek.Thursday;
                context.PickUpDays.Add(pickUp4);
                context.SaveChanges();

                PickUpDay pickUp5 = new PickUpDay();
                pickUp5.Day = DayOfWeek.Friday;
                context.PickUpDays.Add(pickUp5);
                context.SaveChanges();

                PickUpDay pickUp6 = new PickUpDay();
                pickUp6.Day = DayOfWeek.Saturday;
                context.PickUpDays.Add(pickUp6);
                context.SaveChanges();

                PickUpDay pickUp7 = new PickUpDay();
                pickUp7.Day = DayOfWeek.Sunday;
                context.PickUpDays.Add(pickUp7);
                context.SaveChanges();

            }
        }
        
    }
}
