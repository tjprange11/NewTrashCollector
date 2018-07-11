namespace NewTrashCollector.Migrations
{
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
                pickUp.Day = "Monday";
                context.PickUpDays.Add(pickUp);
                context.SaveChanges();

                PickUpDay pickUp2 = new PickUpDay();
                pickUp.Day = "Tuesday";
                context.PickUpDays.Add(pickUp2);
                context.SaveChanges();

                PickUpDay pickUp3 = new PickUpDay();
                pickUp.Day = "Wednesday";
                context.PickUpDays.Add(pickUp3);
                context.SaveChanges();

                PickUpDay pickUp4 = new PickUpDay();
                pickUp.Day = "Thursday";
                context.PickUpDays.Add(pickUp4);
                context.SaveChanges();

                PickUpDay pickUp5 = new PickUpDay();
                pickUp.Day = "Friday";
                context.PickUpDays.Add(pickUp5);
                context.SaveChanges();

                PickUpDay pickUp6 = new PickUpDay();
                pickUp.Day = "Saturday";
                context.PickUpDays.Add(pickUp6);
                context.SaveChanges();

                PickUpDay pickUp7 = new PickUpDay();
                pickUp.Day = "Sunday";
                context.PickUpDays.Add(pickUp7);
                context.SaveChanges();
            }
        }
    }
}
