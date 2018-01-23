using System.Collections.Generic;
using System.Linq;

namespace UITests
{
    public class KnownUsers
    {
        public static BlogPostUser Get(string username)
        {
            return new List<BlogPostUser>
            {
                new BlogPostUser
                {
                    UserName = "Thomas",
                    Password = "blogposter123"
                },
                new BlogPostUser
                {
                    UserName = "Wiljag",
                    Password = "blogpost123"
                }
            }.FirstOrDefault(bpu => bpu.UserName.Equals(username));
        }
    }

    public class BlogPostUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
