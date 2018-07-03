using System;
using System.Collections.Generic;
using System.Text;

namespace WebBlog.Common
{
    public class Constants
    {
        public static readonly string API_URL = "";
        public static readonly string DefaultLanguage = "vi";
        public const double BriefContentCharater = 200;
        public const int PageSizeAdmin = 10;
        public const int PageSizeClient = 5;
    }

    public class EnumCategoryType
    {
        public const string ARTICLE = "Article";
        public const string USER = "User";
        public const string MEDIA = "Media";

        public static Dictionary<string, string> List = new Dictionary<string, string>()
        {
            { "Article", "Bài viết" },
            { "User", "Người dùng" },
            { "Media", "Media" }
        };
    }
}
