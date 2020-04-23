using System.ComponentModel.DataAnnotations;
using System;

namespace BlazorCRUD1.Entities
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }
        public string FullNmae { get; set; }
    }
}
