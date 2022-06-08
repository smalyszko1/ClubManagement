namespace ClubManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Nationality = c.String(nullable: false),
                        YellowCards = c.Int(nullable: false),
                        RedCards = c.Int(nullable: false),
                        Brithday = c.DateTime(nullable: false),
                        Position = c.Int(nullable: false),
                        Goals = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        Minutes = c.Int(nullable: false),
                        Absence = c.Int(nullable: false),
                        Examination = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Players");
        }
    }
}
