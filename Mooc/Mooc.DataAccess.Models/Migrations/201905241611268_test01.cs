namespace Mooc.DataAccess.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PassWord = c.String(nullable: false, maxLength: 20),
                        Email = c.String(),
                        NickName = c.String(),
                        UserState = c.Int(nullable: false),
                        RoleType = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
