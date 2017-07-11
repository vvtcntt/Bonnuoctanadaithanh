using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class Village
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> DistributeId { get; set; }
        public virtual Distribute Distribute { get; set; }
        public virtual District District { get; set; }
    }
}
