using System;
using System.Collections.Generic;
using System.Text;

namespace AChangeTracker.Entities
{
    public class Student: IAuditable02
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }
    }
}
