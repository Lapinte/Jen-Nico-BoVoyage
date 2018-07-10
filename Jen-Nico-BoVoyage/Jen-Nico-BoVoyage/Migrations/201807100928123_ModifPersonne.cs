namespace Jen_Nico_BoVoyage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifPersonne : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "DateNaissance", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Participants", "DateNaissance", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Participants", "DateNaissance", c => c.String());
            AlterColumn("dbo.Clients", "DateNaissance", c => c.String());
        }
    }
}
