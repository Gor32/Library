using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD1.Entities
{
    public class BookGenres
    {
        [Key]
        public int BookGenreId { get; set; }
        public string BookGenreName { get; set; }
        public int MoreLikeBookGenreId { get; set; }
    }
}
