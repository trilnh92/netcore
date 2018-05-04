using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebBlog.Database.Models.BaseModels
{
    public class PagedResult
    {
        public PagedResult(PagingInput pagingInput)
        {
            CurrentPage = pagingInput.Page;
            _pageSize = pagingInput.PageSize;
            _totalPages = this.CalculateTotalPages();
        }

        public PagedResult() { }

        private int _totalPages;
        private int _totalRecords;
        private int _pageSize;

        public IEnumerable Records { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                _totalPages = this.CalculateTotalPages();
            }
        }

        public int TotalRecords
        {
            get { return _totalRecords; }
            set
            {
                _totalRecords = value;
                _totalPages = this.CalculateTotalPages();
            }
        }

        public int TotalRecordsWithoutFiltering { get; set; }

        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
        }

        protected int CalculateTotalPages()
        {
            var pageCount = 0;
            if (PageSize > 0)
            {
                pageCount = (TotalRecords / PageSize);
                if (TotalRecords % PageSize > 0)
                {
                    ++pageCount;
                }
            }

            return pageCount;
        }
    }

    public class PagedResult<T> : PagedResult
    {
        public PagedResult(PagingInput pagingInput) : base(pagingInput)
        {
        }

        public PagedResult() { }

        public new IEnumerable<T> Records
        {
            get
            {
                return base.Records.OfType<T>();
            }
            set
            {
                base.Records = value;
            }
        }
    }
}
