namespace PruebaTecnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigraciónInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.arls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        valor = c.Decimal(precision: 18, scale: 2),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                        habilitado = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.contratoslaborales",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idestado = c.Int(),
                        idarl = c.Int(),
                        idcargo = c.Int(),
                        idtipodocumento = c.Int(),
                        numerodocumento = c.Decimal(precision: 18, scale: 2),
                        primerapellido = c.String(),
                        segundoapellido = c.String(),
                        primernombre = c.String(),
                        segundonombre = c.String(),
                        fechainicio = c.DateTime(),
                        fechafin = c.DateTime(),
                        salario = c.Decimal(precision: 18, scale: 2),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                        arl_id = c.Int(),
                        cargos_id = c.Int(),
                        estados_id = c.Int(),
                        tipodocumento_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.arls", t => t.arl_id)
                .ForeignKey("dbo.cargos", t => t.cargos_id)
                .ForeignKey("dbo.estados", t => t.estados_id)
                .ForeignKey("dbo.tipodocumentoes", t => t.tipodocumento_id)
                .Index(t => t.arl_id)
                .Index(t => t.cargos_id)
                .Index(t => t.estados_id)
                .Index(t => t.tipodocumento_id);
            
            CreateTable(
                "dbo.cargos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.estados",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tipodocumentoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.novedadesnominas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        periodolaborado = c.String(),
                        horaslaboradas = c.Decimal(precision: 18, scale: 2),
                        horaextradiurna = c.Decimal(precision: 18, scale: 2),
                        horaextranocturna = c.Decimal(precision: 18, scale: 2),
                        horaextradominical = c.Decimal(precision: 18, scale: 2),
                        horaextrafestiva = c.Decimal(precision: 18, scale: 2),
                        descuentos = c.Decimal(precision: 18, scale: 2),
                        usuariocreacion = c.String(),
                        fechacreacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.contratoslaborales", "tipodocumento_id", "dbo.tipodocumentoes");
            DropForeignKey("dbo.contratoslaborales", "estados_id", "dbo.estados");
            DropForeignKey("dbo.contratoslaborales", "cargos_id", "dbo.cargos");
            DropForeignKey("dbo.contratoslaborales", "arl_id", "dbo.arls");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.contratoslaborales", new[] { "tipodocumento_id" });
            DropIndex("dbo.contratoslaborales", new[] { "estados_id" });
            DropIndex("dbo.contratoslaborales", new[] { "cargos_id" });
            DropIndex("dbo.contratoslaborales", new[] { "arl_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.novedadesnominas");
            DropTable("dbo.tipodocumentoes");
            DropTable("dbo.estados");
            DropTable("dbo.cargos");
            DropTable("dbo.contratoslaborales");
            DropTable("dbo.arls");
        }
    }
}
