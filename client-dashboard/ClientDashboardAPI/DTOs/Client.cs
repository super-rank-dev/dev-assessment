using System.Collections.Generic;

namespace ClientDashboardAPI.DTOs {
    public class Client
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public List<PhoneNumber> phone_numbers { get; set; }
        public bool archived { get; set; }
    }
}