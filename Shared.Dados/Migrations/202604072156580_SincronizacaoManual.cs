namespace CadastroUsuarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SincronizacaoManual : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioModels", newName: "Usuarios");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Usuarios", newName: "UsuarioModels");
        }
    }
}
