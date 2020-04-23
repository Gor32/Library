using System;
using System.ComponentModel.DataAnnotations;


namespace BlazorCRUD1.Entities
{
    public class ClientCategores
    {
        [Key]
        public int ClientCategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
