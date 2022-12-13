using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Services
{
    public interface IFooService1
    {
        Task DoSomething();
        Task DoSomethingWithTransaction();
    }
}
