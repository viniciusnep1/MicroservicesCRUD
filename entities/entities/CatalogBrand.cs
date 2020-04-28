using core.seedwork;
using System;
using System.ComponentModel.DataAnnotations;

namespace entities.entities
{
    public class CatalogBrand : BaseEntity<Guid>
    {
        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public CatalogBrand()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Today;
        }
    }
}