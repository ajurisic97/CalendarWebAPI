using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class TaxGroup
    {
        public TaxGroup()
        {
            TaxInTaxGroups = new HashSet<TaxInTaxGroup>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TaxInTaxGroup> TaxInTaxGroups { get; set; }
    }
}
