using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BlazorCRUD1.Entities
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int BookSize { get; set; }

        public List<Authors> AuthorList { get; set; }
        public List<BookGenres> BookGenreList { get; set; }
        public string AuthorListView
        {
            get
            {
                var result = "    ";
                foreach (var item in AuthorList)
                {
                    result += item.FullNmae + ", ";
                }

                return result[0..^2];
            }
        }
        public string BookGenreListView
        {
            get
            {
                var result = "    ";
                foreach (var item in BookGenreList)
                {
                    result += item.BookGenreName + ", ";
                }

                return result[0..^2];
            }
        }

    }
}
