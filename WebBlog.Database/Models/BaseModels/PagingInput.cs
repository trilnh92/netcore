using System;
using System.Collections.Generic;
using System.Text;

namespace WebBlog.Database.Models.BaseModels
{
    public class PagingInput
    {
        public PagingInput(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public PagingInput()
        {
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public bool Descending { get; set; }

        public string SearchValue { get; set; }
    }
}
