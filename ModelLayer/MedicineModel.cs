using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class MedicineModel
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public string Price { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Expiration { get; set; }
    }
}
