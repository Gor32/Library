using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace BlazorCRUD1.Concrete
{
    public class ClientCategoresManager: IClientCategoresManager
    {

        private readonly IDapperManager _dapperManager;

        public ClientCategoresManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(ClientCategores article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("CategoryName", article.CategoryName, DbType.String);
            dbPara.Add("ClientCategoryId", article.ClientCategoryId, DbType.Int32);
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_ClientCategores]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<ClientCategores> GetById(int id)
        {
            var article = Task.FromResult(_dapperManager.Get<ClientCategores>($"select * from [ClientCategores] where ClientCategoryId = {id}", null,
                    commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [ClientCategores] where ClientCategoryId = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [ClientCategores] WHERE CategoryName like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<ClientCategores>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var articles = Task.FromResult(_dapperManager.GetAll<ClientCategores>
                ($"SELECT * FROM [ClientCategores] WHERE CategoryName like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(ClientCategores article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", article.ClientCategoryId);
            dbPara.Add("CategoryName", article.CategoryName, DbType.String);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_ClientCategores]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
