using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coldons.Lib
{
    [Keyless]
    public partial class Territory
    {
        [Column(TypeName = "nvarchar")]
        public string TerritoryId { get; set; } = null!;
        [Column(TypeName = "nchar")]
        public string TerritoryDescription { get; set; } = null!;
        [Column(TypeName = "int")]
        public long RegionId { get; set; }
    }
}
