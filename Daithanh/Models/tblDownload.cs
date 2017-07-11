using System;
using System.Collections.Generic;

namespace Daithanh.Models
{
    public partial class tblDownload
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public string HeadShort { get; set; }
        public string ImageName { get; set; }
        public string ImageLink { get; set; }
        public string ImageLinkRoot { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
    }
}
