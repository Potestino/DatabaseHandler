using DatabaseHandler.Sample.Domain.Consts;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Infra.Repositories
{
    public class FooRepository1 : IFooRepository1
    {
        private readonly IDbHandlerFactory _db;
        public FooRepository1(IDbHandlerFactory db) => _db = db;

        public async Task<IEnumerable<dynamic>> GetFoos() => await _db.GetSql(DbConsts.FOO_DATABASE_1)
                                                                      .QueryAsync($"SELECT TOP 20 Id, Description FROM {DbConsts.SQL_TABLE_WITH_IDENTITY} ORDER BY 1 DESC");
        public async Task<dynamic> GetFooById(int id) => await _db.GetSql(DbConsts.FOO_DATABASE_1)
                                                                  .QueryFirstOrDefaultAsync($"SELECT Id, Description FROM {DbConsts.SQL_TABLE_WITH_IDENTITY} WHERE Id = @id", new { id });
    }
}
