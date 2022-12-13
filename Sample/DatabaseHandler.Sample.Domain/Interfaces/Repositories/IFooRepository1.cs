using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Repositories
{
    public interface IFooRepository1
    {
        Task<IEnumerable<dynamic>> GetFoos();
        Task<dynamic> GetFooById(int id);
    }
}
