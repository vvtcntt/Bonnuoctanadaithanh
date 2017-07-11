using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class Country
    {
        public Country()
        {
            this.Provinces = new List<Province>();
        }

        public int Id { get; set; }
        public string ISO { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> Default { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
