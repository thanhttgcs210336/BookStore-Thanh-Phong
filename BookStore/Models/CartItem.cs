using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class CartItem
    {
        public int quantity { get; set; } 
        public  Book book { get; set; }
    }
}
