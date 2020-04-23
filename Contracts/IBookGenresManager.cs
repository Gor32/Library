using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorCRUD1.Entities;

namespace BlazorCRUD1.Contracts
{
    public interface IBookGenresManager
    {
        Task<int> Create(BookGenres article);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(BookGenres article);
        Task<BookGenres> GetById(int Id);
        Task<List<BookGenres>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
