using DatabaseHandler.Sample.Domain.Consts;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Infra.Repositories
{
    public class FooRepository3 : IFooRepository3
    {
        private readonly IDbHandlerFactory _db;
        public FooRepository3(IDbHandlerFactory db) => _db = db;
        
        public async Task<dynamic> GetFoos()
        {
            string sql = @$"SELECT
                              F.Id,                               
                              F.Description                              
                           FROM 
                              {DbConsts.ORACLE_TABLE_SEQUENCE_WITH_TRIGGER} F
                           WHERE 
                              ROWNUM <= 20";

            return await _db.GetOracle(DbConsts.FOO_DATABASE_3).QueryAsync(sql);
        }

        public async Task<dynamic> GetFooByDescription(string description)
        {
            string sql = @$"SELECT
                              F.Id,                               
                              F.Description                              
                           FROM 
                              {DbConsts.ORACLE_TABLE_SEQUENCE_WITH_TRIGGER} F
                           WHERE                               
                              F.Description = :description";

            return await _db.GetOracle(DbConsts.FOO_DATABASE_3).QueryFirstOrDefaultAsync(sql, new { description });
        }
    }
}
