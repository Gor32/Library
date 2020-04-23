using BlazorCRUD1.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCRUD1.Contracts
{
   public interface IClientsManager
    {
        Task<int> Create(Clients client);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Clients client);
        Task UpdateBook(Clients client);
        Task<Clients> GetById(int Id);
        Task<List<Clients>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
