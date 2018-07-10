namespace Jen_Nico_BoVoyage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifTableParticipants : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Participants", "NumeroUnique");
            DropColumn("dbo.Participants", "Reduction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Reduction", c => c.Single(nullable: false));
            AddColumn("dbo.Participants", "NumeroUnique", c => c.Int(nullable: false));
        }
    }
}
