using System.Collections.Generic;

namespace Test.Models
{
    public class Employee
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public List<Review> Reviews;
    }

    public class Review
    {
        public int ReviewerId;
        public string Feedback;
    }
}