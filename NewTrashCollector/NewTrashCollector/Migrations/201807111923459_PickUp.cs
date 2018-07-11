namespace NewTrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickUpDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PickUpDays");
        }
    }
}
