namespace NewTrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SuspendedTimes", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.SuspendedTimes", new[] { "EmployeeId" });
            AddColumn("dbo.SuspendedTimes", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.SuspendedTimes", "CustomerId");
            AddForeignKey("dbo.SuspendedTimes", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.SuspendedTimes", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SuspendedTimes", "EmployeeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SuspendedTimes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SuspendedTimes", new[] { "CustomerId" });
            DropColumn("dbo.SuspendedTimes", "CustomerId");
            CreateIndex("dbo.SuspendedTimes", "EmployeeId");
            AddForeignKey("dbo.SuspendedTimes", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
