namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookingModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "Room_Id" });
            AddColumn("dbo.Bookings", "CardName", c => c.String());
            DropColumn("dbo.Bookings", "Room_Id");
            DropTable("dbo.Rooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.Int(nullable: false),
                        RoomType = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bookings", "Room_Id", c => c.Int());
            DropColumn("dbo.Bookings", "CardName");
            CreateIndex("dbo.Bookings", "Room_Id");
            AddForeignKey("dbo.Bookings", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
