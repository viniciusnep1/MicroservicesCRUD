using core.seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.entities
{
    public class CatalogItem : BaseEntity<Guid>
    {
        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [MaxLength(10)]
        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        [ForeignKey(nameof(CatalogType))]
        public Guid CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }

        [ForeignKey(nameof(CatalogBrand))]
        public Guid CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }

        public CatalogItem()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Today;
        }
    }
}
