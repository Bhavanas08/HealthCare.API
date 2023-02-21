namespace HealthCare.API.Models
{
    public class Patient
    {
        public int id { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string Patientcity { get; set; }
        public string Gender { get;set; }
        public int age { get; set; }

        public string email { get; set; }

        public User users { get; set; }
        public int UserId{ get; set; }
    }
}