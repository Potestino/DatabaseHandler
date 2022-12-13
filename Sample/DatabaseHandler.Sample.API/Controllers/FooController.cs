using DatabaseHandler.Sample.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        private readonly IFooService1 _fooService1;
        private readonly IFooService2 _fooService2;
        private readonly IFooService3 _fooService3;        
        public FooController(IFooService1 fooService1,
                             IFooService2 fooService2,
                             IFooService3 fooService3)
        {
            _fooService1 = fooService1;
            _fooService2 = fooService2;
            _fooService3 = fooService3;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            // EXAMPLE: Calling multiple methods from different services in the same scope

            await _fooService1.DoSomething(); //Shares all databases connections
            await _fooService2.DoSomething(); //Shares all databases connections
            await _fooService3.DoSomething(); //Shares all databases connections
            
            return Ok();

            //Afterwards .NET DependencyInjection disposes IDbHandlerFactory instance containing 2 SQLConnection's and 1 OracleConnection that was shared between services inside the repos
        }
    }
}
