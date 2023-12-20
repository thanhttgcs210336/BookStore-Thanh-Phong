using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Book
{
    public string BookId { get; set; } = null!;

    public string? BookName { get; set; }

    public string? CategoryId { get; set; }

    public string? Image { get; set; }

    public string? Author { get; set; }

    public int Price { get; set; }
}
