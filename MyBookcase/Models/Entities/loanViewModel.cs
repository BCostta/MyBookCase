namespace MyBookcase.Models.Entities
{
    public class loanViewModel
    {
        public List<Loan> loans { get; set; }
        public List<User> users { get; set; }
        public List<Book> books { get; set; }

        public List<Category> categories { get; set; }
    }
}
