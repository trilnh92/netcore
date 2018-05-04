using System;
using System.Collections.Generic;
using System.Text;
using WebBlog.Database.Models;

namespace WebBlog.Database.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
    }
}
