namespace MyBookcase.Models.Entities
{
    public class Loan
    {

        public int Id { get; set; }
        public int bookId { get; set; }
        public int userId { get; set; }
        public DateTime loanDate { get; set; }
        public DateTime returnDate { get; set; }
        public int Status { get; set; }


    }
}
