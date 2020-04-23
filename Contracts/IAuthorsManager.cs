using BlazorCRUD1.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCRUD1.Contracts
{
    public interface IAuthorsManager
    {
        Task<int> Create(Authors article);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Authors article);
        Task<Authors> GetById(int Id);
        Task<List<Authors>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
