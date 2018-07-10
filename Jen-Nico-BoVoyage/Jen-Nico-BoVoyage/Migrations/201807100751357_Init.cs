namespace Jen_Nico_BoVoyage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Civilite = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateNaissance = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(),
                        Pays = c.String(),
                        Region = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dossiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(),
                        PrixTotal = c.Single(nullable: false),
                        Assurance = c.Boolean(nullable: false),
                        VoyageID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: true)
                .Index(t => t.VoyageID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateAller = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        PlacesDisponibles = c.Int(nullable: false),
                        TarifToutCompris = c.Single(nullable: false),
                        DestinationID = c.Int(nullable: false),
                        AgenceID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agences", t => t.AgenceID, cascadeDelete: true)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: true)
                .Index(t => t.DestinationID)
                .Index(t => t.AgenceID);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroUnique = c.Int(nullable: false),
                        Reduction = c.Single(nullable: false),
                        DossierID = c.Int(nullable: false),
                        Civilite = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateNaissance = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dossiers", t => t.DossierID, cascadeDelete: true)
                .Index(t => t.DossierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "DossierID", "dbo.Dossiers");
            DropForeignKey("dbo.Dossiers", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.Voyages", "DestinationID", "dbo.Destinations");
            DropForeignKey("dbo.Voyages", "AgenceID", "dbo.Agences");
            DropForeignKey("dbo.Dossiers", "ClientID", "dbo.Clients");
            DropIndex("dbo.Participants", new[] { "DossierID" });
            DropIndex("dbo.Voyages", new[] { "AgenceID" });
            DropIndex("dbo.Voyages", new[] { "DestinationID" });
            DropIndex("dbo.Dossiers", new[] { "ClientID" });
            DropIndex("dbo.Dossiers", new[] { "VoyageID" });
            DropTable("dbo.Participants");
            DropTable("dbo.Voyages");
            DropTable("dbo.Dossiers");
            DropTable("dbo.Destinations");
            DropTable("dbo.Clients");
            DropTable("dbo.Agences");
        }
    }
}
