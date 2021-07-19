using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APS.Model
{
    public class Plane
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("MAKE")]
        public string Make { get; set; }
        [Column("MODEL")]
        public string Model { get; set; }
        [Column("REGISTRATION")]
        public string Registration { get; set; }
        [Column("LOCATION")]
        public string Location { get; set; }
        [Column("ENTRYDATETIME")]
        public DateTime EntryDatetime { get; set; }
    }
}
