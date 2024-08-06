using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQL.DIARY.UI.Migrations
{
    public partial class MssqlTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   UserName = table.Column<string>(maxLength: 256, nullable: true), 
                   Password = table.Column<string>(nullable: true), 
                   IsAdmin=table.Column<bool>(nullable:false),
                   SERVER_NAME=table.Column<string>(nullable:false),
                   CONNECTION_STRING =table.Column<string>(nullable:false)
              },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Users", x => x.Id);
                    
               });



            migrationBuilder.CreateTable(
              name: "DatabaseModule",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  ServerName = table.Column<string>(maxLength: 256, nullable: true),
                  DatabaseName = table.Column<string>(nullable: true),
                  DbModuleName = table.Column<string>(nullable: true),
                  tables = table.Column<string>(nullable: true)
                  
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_DatabaseModule", x => x.Id);

              });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
