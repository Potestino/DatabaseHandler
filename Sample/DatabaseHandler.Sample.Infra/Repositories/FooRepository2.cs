using DatabaseHandler.Sample.Domain.Consts;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Infra.Repositories
{
    public class FooRepository2 : IFooRepository2
    {
        private readonly IDbHandlerFactory _db;
        public FooRepository2(IDbHandlerFactory db) => _db = db;

        public async Task<dynamic> GetFoos() => await _db.GetSql(DbConsts.FOO_DATABASE_2)
                                                         .QueryFirstOrDefaultAsync($"SELECT TOP 20 * FROM {DbConsts.SQL_TABLE_WITH_IDENTITY} ORDER BY 1 DESC");
                                                 
        public async Task<dynamic> GetFooDescriptionById(int id) => await _db.GetSql(DbConsts.FOO_DATABASE_2)
                                                                             .QueryFirstOrDefaultAsync($"SELECT Description FROM {DbConsts.SQL_TABLE_WITH_IDENTITY} WHERE Id = @Id", new { id });
    }
}
