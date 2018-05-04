using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebBlog.Database.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        public int? CategoryArticleId { get; set; }

        public string CategoryArticleName { get; set; }

        public int ViewCount { get; set; }

        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        public string BriefContent { get; set; }

        public string FullContent { get; set; }

        public string Source { get; set; }

        public int? Index { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        [StringLength(4000)]
        public string SEName { get; set; }

        [StringLength(4000)]
        public string SEOTitle { get; set; }

        [StringLength(4000)]
        public string SEODescription { get; set; }

        [StringLength(4000)]
        public string SEOKeywords { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsHot { get; set; }

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

        public int? Position { get; set; }

        public string Ext { get; set; }

        public string Ext1 { get; set; }

        public string Ext2 { get; set; }

        public string Ext3 { get; set; }
    }
}
