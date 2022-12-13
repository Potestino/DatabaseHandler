using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Repositories
{
    public interface IFooRepository3
    {
        Task<dynamic> GetFoos();
        Task<dynamic> GetFooByDescription(string description);
    }
}
