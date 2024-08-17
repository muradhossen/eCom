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
            #region Category
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
            #endregion


            #region Subcategory
            migrationBuilder.Sql(@"	CREATE OR REPLACE FUNCTION generate_subcategory_code()
                                    RETURNS TRIGGER AS $$
                                    BEGIN
                                        IF NEW.""Code"" IS NULL OR NEW.""Code"" = '' THEN
                                            NEW.""Code"" := 'SUBCAT-' || NEW.""Id"";
                                        END IF;
                                        RETURN NEW;
                                    END;
                                    $$ LANGUAGE plpgsql;");

            migrationBuilder.Sql(@"	CREATE OR REPLACE TRIGGER subcategory_generate_code_trigger
                                        BEFORE INSERT
                                        ON  public.""SubCategories""
                                        FOR EACH ROW
                                        EXECUTE FUNCTION public.generate_subcategory_code();");


            #endregion

            #region Product
            migrationBuilder.Sql(@"
	                                CREATE OR REPLACE FUNCTION generate_product_code()
                                    RETURNS TRIGGER AS $$
                                    BEGIN
                                        IF NEW.""Code"" IS NULL OR NEW.""Code"" = '' THEN
                                            NEW.""Code"" := 'PROD-' || NEW.""Id"";
                                        END IF;
                                        RETURN NEW;
                                    END;
                                    $$ LANGUAGE plpgsql;");

            migrationBuilder.Sql(@"
	                                CREATE OR REPLACE TRIGGER generate_product_code_trigger
                                    BEFORE INSERT
                                    ON public.""Products""
                                    FOR EACH ROW
                                    EXECUTE FUNCTION public.generate_product_code();");
            #endregion

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS generate_category_code CASCADE;");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS ""category_generate_code_trigger"" ON ""Categories"";");
        }
    }
}
