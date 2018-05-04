using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebBlog.Database.Models
{
    public class Media
    {
        public int MediaId { get; set; }

        public int? CategoryMediaId { get; set; }

        public string CategoryMediaName { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedDate { get; set; }

        [StringLength(256)]
        public string DeletedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(4000)]
        public string SEName { get; set; }

        [StringLength(4000)]
        public string SEOTitle { get; set; }

        [StringLength(4000)]
        public string SEODescription { get; set; }

        [StringLength(4000)]
        public string SEOKeywords { get; set; }

        public string Ext { get; set; }

        public int HostId { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string ReferenceSource { get; set; }
    }
}
