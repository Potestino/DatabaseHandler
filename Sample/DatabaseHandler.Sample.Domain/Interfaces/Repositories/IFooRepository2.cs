using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Repositories
{
    public interface IFooRepository2
    {
        Task<dynamic> GetFoos();
        Task<dynamic> GetFooDescriptionById(int id);
    }
}
