using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD1.Models
{
    public class ClientsModel
    {
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int ClientCategoryId { get; set; }
    }
}
