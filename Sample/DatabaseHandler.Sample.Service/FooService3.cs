using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using DatabaseHandler.Sample.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Service
{
    public class FooService3 : IFooService3
    {
        private readonly IFooRepository1 _fooRepo1;
        private readonly IFooRepository2 _fooRepo2;
        private readonly IFooRepository3 _fooRepo3;
        public FooService3(IFooRepository1 fooRepo1,
                           IFooRepository2 fooRepo2,
                           IFooRepository3 fooRepo3)
        {
            _fooRepo1 = fooRepo1;
            _fooRepo2 = fooRepo2;
            _fooRepo3 = fooRepo3;
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
            _ = await _fooRepo3.GetFooByDescription("77276464075");
        }
    }
}
