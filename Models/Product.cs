using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HPlusSportAPI.Models
{
    public class Product
    {
        public  int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool ISAvailable { get; set; }
        public int CatgoryId { get; set; }
        [JsonIgnore]
        public virtual Catgory Catgory { get; set; }


    }
}
