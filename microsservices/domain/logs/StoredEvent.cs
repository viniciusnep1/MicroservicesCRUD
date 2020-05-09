using core.seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.logs
{
    [Table("stored_event", Schema = Schema.SCHEMA_LOGS)]
    public class StoredEvent : BaseEntity<Guid>
    {
        public Guid AggregateId { get; set; }

        public string AggregateEntity { get; set; }

        public string Data { get; set; }

        public string User { get; set; }


        public StoredEvent()
        {
            Id = Guid.NewGuid();
        }

    }
}
