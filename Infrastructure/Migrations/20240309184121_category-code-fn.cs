using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class categorycodefn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION generate_category_code()
                                    RETURNS TRIGGER AS $$
                                    BEGIN
                                        IF NEW.""Code"" IS NULL OR NEW.""Code"" = '' THEN
                                            NEW.""Code"" := 'CAT-' || NEW.""Id"";
                                        END IF;
                                        RETURN NEW;
                                    END;
                                    $$ LANGUAGE plpgsql;");


            migrationBuilder.Sql(@"CREATE TRIGGER category_generate_code_trigger
                                   BEFORE INSERT ON ""Categories""
                                   FOR EACH ROW
                                   EXECUTE FUNCTION generate_category_code();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS generate_category_code CASCADE;");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS ""category_generate_code_trigger"" ON ""Categories"";");
        }
    }
}
