using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewDirectorCooperationTime
    {
        public string FirstDirectorName { get; set; } = null!;
        public string SecondDirectorName { get; set; } = null!;
        public long MovieCount { get; set; }
    }
}
