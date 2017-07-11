using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class tblConnectColorProduct
    {
        public int id { get; set; }
        public Nullable<int> idColor { get; set; }
        public Nullable<int> idPro { get; set; }
    }
}
