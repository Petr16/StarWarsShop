using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsShop.DAL.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
