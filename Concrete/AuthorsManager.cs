﻿using BlazorCRUD1.Contracts;
using BlazorCRUD1.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCRUD1.Concrete
{
    public class AuthorsManager:IAuthorsManager
    {
        private readonly IDapperManager _dapperManager;

        public AuthorsManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Authors article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("FullNmae", article.FullNmae, DbType.String);  
            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Authors]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<Authors> GetById(int id)
        {
            var article = Task.FromResult(_dapperManager.Get<Authors>($"select * from [Authors] where AuthorId = {id}", null,
                    commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [Authors] where AuthorId = {id}", null,
                    commandType: CommandType.Text));
            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Authors] WHERE FullNmae like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<Authors>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var articles = Task.FromResult(_dapperManager.GetAll<Authors>
                ($"SELECT * FROM [Authors] WHERE FullNmae like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(Authors article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", article.AuthorId);
            dbPara.Add("FullNmae", article.FullNmae, DbType.String);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Authors]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
