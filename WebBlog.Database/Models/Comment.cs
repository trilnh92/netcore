using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebBlog.Database.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public List<Comment> Children { get; set; }

        public string Type { get; set; }

        public int ParentId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedDate { get; set; }

        [StringLength(256)]
        public string DeletedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public string Content { get; set; }

        public string Ext { get; set; }

        public int ArticleId { get; set; }
    }
}
