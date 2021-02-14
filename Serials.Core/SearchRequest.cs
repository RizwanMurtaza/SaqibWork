namespace Serials.Core
{
    public class SearchRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string SerialNumber { get; set; }
    }
}