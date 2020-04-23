using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCRUD1.Concrete
{
    public class ClientsManager : IClientsManager
    {
        private readonly IDapperManager _dapperManager;

        public ClientsManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Clients article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("ClientFullName", article.ClientFullName, DbType.String);
            dbPara.Add("ClientCategoryId", article.ClientCategoryId, DbType.Int32);
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Client]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<Clients> GetById(int id)
        {
            return Task.Run(() =>
            {
                var article = _dapperManager.Get<Clients>($"select * from [Clients] where ClientId = {id}", null,
                        commandType: CommandType.Text);
                article.BookList = _dapperManager.GetAll<Books>($"select b.* From Books as b Inner join ClientBook as c on c.BookId = b.BookId where c.ClientId = {id}", null,
                          commandType: CommandType.Text);

                return article;
            });
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [Clients] where ClientId = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Clients] WHERE ClientFullName like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<Clients>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            return Task.Run(() =>
            {
                var articles = _dapperManager.GetAll<Clients>
                    ($"SELECT * FROM [Clients] WHERE ClientFullName like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                foreach (var item in articles)
                {
                    item.BookList = _dapperManager.GetAll<Books>
                   ($"select b.* From Books as b Inner join ClientBook as c on c.BookId = b.BookId where c.ClientId = {item.ClientId}; ", null, commandType: CommandType.Text);
                }
                return articles;
            });
        }

        public Task<int> Update(Clients article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", article.ClientId);
            dbPara.Add("ClientFullName", article.ClientFullName, DbType.String);
            dbPara.Add("ClientCategoryId", article.ClientCategoryId, DbType.Int32);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Client]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
        public Task UpdateBook(Clients article)
        {
            return Task.Run(() =>
            {
                _dapperManager.Execute($"delete ClientBook where ClientId  = {article.ClientId}", null,
                           commandType: CommandType.Text);
                foreach (var item in article.BookList)
                {
                    var dbPara = new DynamicParameters();
                    dbPara.Add("ClientId", article.ClientId, DbType.Int32);
                    dbPara.Add("BookId", item.BookId, DbType.Int32);
                    _dapperManager.Insert<int>("[dbo].[SP_Add_ClientBook]",
                                    dbPara,
                                    commandType: CommandType.StoredProcedure);
                }
            });
        }
    }
}
