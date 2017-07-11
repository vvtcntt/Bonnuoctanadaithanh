using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class Province
    {
        public Province()
        {
            this.Districts = new List<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> DistributeId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual Distribute Distribute { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
