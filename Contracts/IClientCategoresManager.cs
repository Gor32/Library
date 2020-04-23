using BlazorCRUD1.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCRUD1.Contracts
{
   public interface IClientCategoresManager
    {
        Task<int> Create(ClientCategores client);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(ClientCategores client);
        Task<ClientCategores> GetById(int Id);
        Task<List<ClientCategores>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
