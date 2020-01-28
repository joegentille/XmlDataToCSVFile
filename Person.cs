using System.ComponentModel.DataAnnotations;


namespace SomeCodeTools
{
    public class Person
    {
        [Display(Name = "FirstName")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }
        public string Firm { get; set; }

        [Display(Name="Primary Email")]
        public string Email { get; set; }
        public string Channel { get; set; }
        public string DocumentId { get; set; }

        [Display(Name="Document Title")]
        public string Title { get; set; }

        [Display(Name="Primary Ticker")]
        public string Security { get; set; }
        
        [Display(Name = "Primary Analyst")]
        public string Analyst { get; set; }
        public string Sector { get; set; }
        public string Read { get; set; }
        public string Engaged { get; set; }
        public string UpdateTime { get; set; }
    }
}
