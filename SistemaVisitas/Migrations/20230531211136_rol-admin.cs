using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class roladmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"if not exists ( select 1 from AspNetRoles where id = '48075062-44A2-43DC-A6EC-DC26CFEC5FB4' )
                                    begin
	                                    insert into AspNetRoles( Id, Name, NormalizedName )
	                                    select '48075062-44A2-43DC-A6EC-DC26CFEC5FB4','admin','ADMIN'
                                    end");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"delete from AspNetRoles where id = '48075062-44A2-43DC-A6EC-DC26CFEC5FB4' ");
        }
    }
}
