using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD1.Entities
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int ClientCategoryId { get; set; }
        public ClientCategores ClientCategores { get; set; }
        public List<Books> BookList { get; set; }

        public string ClietnBooksListView
        {
            get
            {
                var result = "    ";
                foreach (var item in BookList)
                {
                    result += item.BookName + ", ";
                }

                return result[0..^2];
            }
        }
    }
}
