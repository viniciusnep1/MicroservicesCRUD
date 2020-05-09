using core.seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.entities
{
    [Table("team", Schema = Schema.SCHEMA_ENTITY)]
    public class Team : BaseEntity<Guid>
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public Team()
        {

        }
    }
}
