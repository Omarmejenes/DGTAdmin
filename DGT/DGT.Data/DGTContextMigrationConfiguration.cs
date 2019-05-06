using System.Data.Entity.Migrations;

namespace DGT.Data
{
    class DGTContextMigrationConfiguration : DbMigrationsConfiguration<DGTContext>
    {
        public DGTContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

#if DEBUG
        protected override void Seed(DGTContext context)
        {
            new DGTDataSeeder(context).Seed();
        }
#endif
    }
}
