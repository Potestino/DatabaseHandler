using DatabaseHandler.Sample.Domain.Consts;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using DatabaseHandler.Sample.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Service
{
    public class FooService1 : IFooService1
    {
        private readonly IFooRepository1 _fooRepo1;
        private readonly IFooRepository2 _fooRepo2;
        private readonly IFooRepository3 _fooRepo3;
        private readonly IDbHandlerFactory _db;
        public FooService1(IFooRepository1 fooRepo1,
                           IFooRepository2 fooRepo2,
                           IFooRepository3 fooRepo3,
                           IDbHandlerFactory db)
        {
            _fooRepo1 = fooRepo1;
            _fooRepo2 = fooRepo2;
            _fooRepo3 = fooRepo3;
            _db = db;
        }

        public async Task DoSomething()
        {
            // EXAMPLE: Calling multiple methods from different repos with different type of connections

            // Test (SQL)
            _ = await _fooRepo1.GetFoos();
            _ = await _fooRepo1.GetFooById(777);

            // User (SQL -> other connectionString)
            _ = await _fooRepo2.GetFoos();
            _ = await _fooRepo2.GetFooDescriptionById(777);

            // Customer (Oracle)
            _ = await _fooRepo3.GetFoos();
            _ = await _fooRepo3.GetFooByDescription("Massive Item - With Identity - 1");
        }

        public async Task DoSomethingWithTransaction()
        {
            using (var transaction = _db.GetSql(DbConsts.FOO_DATABASE_1).BeginTransaction())
            {
                try
                {
                    await _db.GetSql(DbConsts.FOO_DATABASE_1).ExecuteAsync("INSERT INTO DBH_Tb_With_Identity VALUES (@description, @value)",
                                                                 new { description = "Transaction - 1", value = 1 },
                                                                 transaction);

                    await _db.GetSql(DbConsts.FOO_DATABASE_1).ExecuteAsync("INSERT INTO DBH_Tb_With_Identity VALUES (@description, @value)",
                                                                 new { description = "Transaction - 2", value = 2 },
                                                                 transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
