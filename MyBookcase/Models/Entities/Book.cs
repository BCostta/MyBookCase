﻿namespace MyBookcase.Models.Entities
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string yearsOfPublication { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
        public int quantityInStock { get; set; }

    }

}
