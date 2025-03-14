using Microsoft.AspNetCore.Components;

namespace Dj.Models
{
    public class DJEvent
    {
        public string ID { get; set; }
        public string DJID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string QRCode { get; set; }

        public DJEvent(string iD, string dJID, string name, string description, DateTime date, string location, string qRCode)
        {
            ID = iD;
            DJID = dJID;
            Name = name;
            Description = description;
            Date = date;
            Location = location;
            QRCode = qRCode;
        }
    }
}
