namespace CarStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrandRequieredForCar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "Brand_Id" });
            AlterColumn("dbo.Cars", "Brand_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "Brand_Id");
            AddForeignKey("dbo.Cars", "Brand_Id", "dbo.Brands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "Brand_Id" });
            AlterColumn("dbo.Cars", "Brand_Id", c => c.Int());
            CreateIndex("dbo.Cars", "Brand_Id");
            AddForeignKey("dbo.Cars", "Brand_Id", "dbo.Brands", "Id");
        }
    }
}
