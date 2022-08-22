using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class TaxInTaxGroup
    {
        public Guid Id { get; set; }
        public Guid TaxGroupId { get; set; }
        public Guid TaxId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Taxis Tax { get; set; } = null!;
        public virtual TaxGroup TaxGroup { get; set; } = null!;
    }
}
