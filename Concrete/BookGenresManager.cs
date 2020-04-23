using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCRUD1.Concrete
{
    public class BookGenresManager: IBookGenresManager
    {
        private readonly IDapperManager _dapperManager;

        public BookGenresManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(BookGenres article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("BookGenreName", article.BookGenreName, DbType.String);
            dbPara.Add("MoreLikeBookGenreId", null, DbType.Int32);
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_BookGenres]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<BookGenres> GetById(int id)
        {
            var article = Task.FromResult(_dapperManager.Get<BookGenres>($"select * from [BookGenres] where BookGenreId = {id}", null,
                    commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [BookGenres] where BookGenreId = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [BookGenres] WHERE BookGenreName like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<BookGenres>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var articles = Task.FromResult(_dapperManager.GetAll<BookGenres>
                ($"SELECT * FROM [BookGenres] WHERE BookGenreName like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(BookGenres article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", article.BookGenreId);
            dbPara.Add("BookGenreName", article.BookGenreName, DbType.String);
            dbPara.Add("MoreLikeBookGenreId", article.MoreLikeBookGenreId, DbType.Int32);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_BookGenres]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
