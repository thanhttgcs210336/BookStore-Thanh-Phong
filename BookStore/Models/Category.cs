using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }
}
