namespace CarStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBrandCar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Prestige = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Brand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "Brand_Id" });
            DropTable("dbo.Cars");
            DropTable("dbo.Brands");
        }
    }
}
