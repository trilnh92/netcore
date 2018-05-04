using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebBlog.Database.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        [Required]
        public string Name { get; set; }

        //Type = Controller. It should set default by developer.
        [Required]
        public string Type { get; set; }

        [Required]
        public string Language { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }

        public bool HasChildren { get; set; }

        public bool HasUrl { get; set; }

        public int Index { get; set; }
    }
}
