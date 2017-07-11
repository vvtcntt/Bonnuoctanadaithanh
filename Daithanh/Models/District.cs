using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class District
    {
        public District()
        {
            this.Villages = new List<Village>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProvinceId { get; set; }
        public Nullable<int> DistributeId { get; set; }
        public virtual Distribute Distribute { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<Village> Villages { get; set; }
    }
}
