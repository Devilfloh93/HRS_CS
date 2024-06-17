
namespace HumanResourcesApp.Dao
{
    public abstract class BaseDAO
    {
        protected const SqliteConnection CONNECTION = new("Data Source=Sql\\sqlite\\human_resource_app.db");
    }
}
