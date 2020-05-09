using core.seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.entities
{
    [Table("people", Schema = Schema.SCHEMA_ENTITY)]
    public class People : BaseEntity<Guid>
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(11)]
        [Required]
        public string Cpf { get; set; }

        [ForeignKey(nameof(Team))]
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }


        public People()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreatedAt = DateTime.Today;
        }
    }
}
