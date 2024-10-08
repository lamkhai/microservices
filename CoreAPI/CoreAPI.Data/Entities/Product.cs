﻿namespace CoreAPI.Data.Entities;

public sealed class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }
}