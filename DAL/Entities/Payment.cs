namespace proeduedge.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Amount { get; set; }
        public string PaymentType { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
