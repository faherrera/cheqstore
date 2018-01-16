namespace CheqStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "User");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.User", newName: "Users");
        }
    }
}
