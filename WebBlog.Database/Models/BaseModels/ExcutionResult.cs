using System;
using System.Collections.Generic;
using System.Text;

namespace WebBlog.Database.Models.BaseModels
{
    public class ExecutionResult
    {
        public ExecutionResult()
        {
            Messages = new List<string>();
            Errors = new Dictionary<string, List<string>>();
            Resources = new Dictionary<string, object>();
        }

        public bool Succeed { get; set; }

        public double TotalRecords { get; set; }

        public bool RequirePermission { get; set; }

        public string Error { get; set; }

        public ICollection<string> Messages { get; set; }

        public IDictionary<string, List<string>> Errors { get; set; }

        public IDictionary<string, string> Labels { get; set; }

        public IDictionary<string, object> Resources { get; set; }

        public object Result { get; set; }
    }

    public class ExecutionResult<T> : ExecutionResult
    {
        public new T Result
        {
            get
            {
                return (T)base.Result;
            }
            set
            {
                base.Result = value;
            }
        }
    }
}
