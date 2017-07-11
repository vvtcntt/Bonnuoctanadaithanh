using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class Distribute
    {
        public Distribute()
        {
            this.Districts = new List<District>();
            this.Villages = new List<Village>();
            this.Provinces = new List<Province>();
        }

        public int Id { get; set; }
        public string Explain { get; set; }
        public string Display { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Village> Villages { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
