using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSE_SBA.Controllers;
using System.Collections.Generic;
using System.Web;
using Models=FSE_SBA.Models;
using DAC = FSE_IIHT_SBA.DAC;
using BC = FSE_SBA.BC;
using System.Data.Entity;

namespace ProjectManager.Test
{
    class MockProjectManagerEntities : DAC.ProjectManagerEntities
    {
        private DbSet<DAC.User> _users = null;
        private DbSet<DAC.Project> _projects = null;
        private DbSet<DAC.Task> _tasks = null;
        public override DbSet<DAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<DAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.GetUser() as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<Models.User>));
            Assert.AreEqual((result.Data as List<Models.User>).Count, 2);
        }

        [TestMethod]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "ankita";
            user.LastName = "ghosh";
            user.EmployeeId = "123456";
            user.UserId = 123;
            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Khush";
            user.LastName = "Mandal";
            user.EmployeeId = "123";
            user.UserId = 401630;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "KHUSH");
        }

        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Arnabi";
            user.LastName = "Mandal";
            user.EmployeeId = "401630";
            user.UserId = 401630;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "401638",
                First_Name = "Amantrita",
                Last_Name = "Ghosh",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 401638
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "401630",
                First_Name = "Arnabi",
                Last_Name = "Mandal",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 401630
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as Models.JSendResponse;
        }

    }
}

