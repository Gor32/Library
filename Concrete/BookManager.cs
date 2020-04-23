using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCRUD1.Concrete
{
    public class BookManager : IBookManager
    {
        private readonly IDapperManager _dapperManager;

        public BookManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }
        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Books] WHERE BookName like '%{search}%'", null,
                  commandType: CommandType.Text));
            return totArticle;
        }

        public Task<int> Create(Books article)
        {
            return Task.Run(() =>
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("BookName", article.BookName, DbType.String);
                dbPara.Add("BookSize", article.BookSize, DbType.Int32);
                var articleId = _dapperManager.Insert<int>("[dbo].[SP_Add_Books]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                foreach (var author in article.AuthorList)
                {
                    dbPara = new DynamicParameters();
                    dbPara.Add("AuthorId", author.AuthorId, DbType.Int32);
                    dbPara.Add("BookId", articleId, DbType.Int32);
                    _dapperManager.Insert<int>("[dbo].[SP_Add_AuthorBook]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                }

                foreach (var author in article.BookGenreList)
                {
                    dbPara = new DynamicParameters();
                    dbPara.Add("BookGenreId", author.BookGenreId, DbType.Int32);
                    dbPara.Add("BookId", articleId, DbType.Int32);
                    _dapperManager.Insert<int>("[dbo].[SP_Add_BookBookGenrese]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                }
                return articleId;
            });
        }

        public Task<int> Delete(int id)
        {
            return Task.Run(() =>
            {
                var deleteArticle = _dapperManager.Execute($"Delete [Books] where BookId = {id}", null,
                       commandType: CommandType.Text);
                _dapperManager.Execute($"Delete AuthorBook where BookId = {id}", null,
                       commandType: CommandType.Text);
                _dapperManager.Execute($"Delete BookBookGenres where BookId = {id}", null,
                       commandType: CommandType.Text);

                return deleteArticle;
            });
        }

        public Task<Books> GetById(int id)
        {
            return Task.Run(() =>
            {
                var article = _dapperManager.Get<Books>($"select * from [Books] where BookId = {id}", null,
                      commandType: CommandType.Text);
                article.AuthorList = _dapperManager.GetAll<Authors>
                   ($"SELECT a.* FROM dbo.Authors a INNER JOIN dbo.AuthorBook ab ON a.AuthorId = ab.AuthorId WHERE ab.BookId = {id}; ", null, commandType: CommandType.Text);
                article.BookGenreList = _dapperManager.GetAll<BookGenres>
                   ($"SELECT a.* FROM dbo.BookGenres a INNER JOIN dbo.BookBookGenres ab ON a.BookGenreId = ab.BookGenreId WHERE ab.BookId = {id}; ", null, commandType: CommandType.Text);
                return article;
            }
            );
        }

        public Task<List<Books>> ListAll(int skip, int take, string orderBy, string direction, string search)
        {
            return Task.Run(() =>
            {
                var articles = _dapperManager.GetAll<Books>
               ($"SELECT * FROM [Books] WHERE BookName like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                foreach (var item in articles)
                {
                    item.AuthorList = _dapperManager.GetAll<Authors>
                   ($"SELECT a.* FROM dbo.Authors a INNER JOIN dbo.AuthorBook ab ON a.AuthorId = ab.AuthorId WHERE ab.BookId = {item.BookId}; ", null, commandType: CommandType.Text);
                }
                foreach (var item in articles)
                {
                    item.BookGenreList = _dapperManager.GetAll<BookGenres>
                   ($"SELECT a.* FROM dbo.BookGenres a INNER JOIN dbo.BookBookGenres ab ON a.BookGenreId = ab.BookGenreId WHERE ab.BookId = {item.BookId}; ", null, commandType: CommandType.Text);
                }
                return articles;
            });
        }

        public Task<int> Update(Books article)
        {
            return Task.Run(() =>
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("BookId", article.BookId);
                dbPara.Add("BookName", article.BookName, DbType.String);
                dbPara.Add("BookSize", article.BookSize, DbType.Int32);

                var updateArticle = _dapperManager.Update<int>("[dbo].[SP_Update_Books]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                _dapperManager.Execute($"Delete AuthorBook where BookId = {article.BookId}", null,
                         commandType: CommandType.Text);
                _dapperManager.Execute($"Delete BookBookGenres where BookId = {article.BookId}", null,
                       commandType: CommandType.Text);
                var articleId = article.BookId;
                foreach (var author in article.AuthorList)
                {
                    dbPara = new DynamicParameters();
                    dbPara.Add("AuthorId", author.AuthorId, DbType.Int32);
                    dbPara.Add("BookId", articleId, DbType.Int32);
                    _dapperManager.Insert<int>("[dbo].[SP_Add_AuthorBook]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                }

                foreach (var author in article.BookGenreList)
                {
                    dbPara = new DynamicParameters();
                    dbPara.Add("BookGenreId", author.BookGenreId, DbType.Int32);
                    dbPara.Add("BookId", articleId, DbType.Int32);
                    _dapperManager.Insert<int>("[dbo].[SP_Add_BookBookGenrese]",
                                dbPara,
                                commandType: CommandType.StoredProcedure);
                }

                return updateArticle;
            });
        }
    }
}
