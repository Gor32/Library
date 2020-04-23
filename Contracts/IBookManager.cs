using BlazorCRUD1.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCRUD1.Contracts
{
    public interface IBookManager
    {
        Task<int> Create(Books article);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Books article);
        Task<Books> GetById(int Id);
        Task<List<Books>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
