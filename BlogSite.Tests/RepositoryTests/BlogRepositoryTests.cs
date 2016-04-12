using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Collections.Generic;

namespace BlogSite.Tests.RepositoryTests
{
    [TestClass]
    public class BlogRepositoryTests
    {
        IBlogRepository repo = null;

        [TestInitialize]
        public void InitialiseTestDatabase()
        {
            repo = new BlogRepository();
            repo.DeleteAllUsers();
        }

        [TestMethod]
        [TestCategory("Blog")]
        public void GetUsersTest()
        {
            // BlogRepository repo = new BlogRepository();
            repo.InsertUser("test@mail.net", "password");
            repo.InsertUser("test2@mail.net", "password");
            repo.InsertUser("test3@mail.net", "password");
            repo.InsertUser("test4@mail.net", "password");
            List<string> users = repo.GetUserNames();
            Assert.IsTrue(users.Count == 4);

        }

        [TestMethod]
        [TestCategory("Blog")]
        public void InsertUsersTest()
        {
            //   BlogRepository repo = new BlogRepository();
            repo.InsertUser("test@mail.net", "password");
            List<string> users = repo.GetUserNames();
            Assert.IsTrue(repo.GetUserCount() == 1);

        }

        [TestMethod]
        [TestCategory("Blog")]
        public void LoginUserTest()
        {
            //   BlogRepository repo = new BlogRepository();
            string username = "test@mail.net";
            string password = "password";
            bool result = false;
            repo.InsertUser("test@mail.net", "password");
            result = repo.LoginUser(username, password);
            Assert.IsTrue(result);

        }

        [TestCleanup]
        public void CleanUpResources()
        {
            repo = null;
        }
    }
}
