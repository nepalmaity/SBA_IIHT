using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSE_SBA.Controllers;
using Models=FSE_SBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAC = FSE_IIHT_SBA.DAC;
using BC = FSE_SBA.BC;
namespace ProjectManager.Test
{

    [TestClass]
    public class TaskControllerTest
    {
        [TestMethod]
        public void TestRetrieveTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();

            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "401628",
                First_Name = "Gargi",
                Last_Name = "Pal",
                User_ID = 123,
                Task_ID = 1
            });
            context.Users = users;
            int projectid = 1234;
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 123456,
                Project_ID = 1234,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0

            });
            context.Tasks = tasks;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(projectid) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<Models.Task>));
        }

        [TestMethod]
        public void TestRetrieveParentTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "ANB"

            });
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveParentTasks() as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<Models.ParentTask>));
            Assert.AreEqual((result.Data as List<Models.ParentTask>).Count, 2);
        }

        [TestMethod]
        public void TestInsertTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "654219",
                First_Name = "Rajesh",
                Last_Name = "Ray",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            var task = new Models.Task()
            {

                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,
                User = new Models.User()
                {
                    FirstName = "Rajesh",
                    LastName = "Ray",
                    EmployeeId = "654219",
                    UserId = 1000
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as Models.JSendResponse;


            Assert.IsNotNull(result);

            Assert.IsNotNull((context.Users.Local[0]).Task_ID);
        }

        [TestMethod]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = 401638.ToString(),
                First_Name = "TEST",
                Last_Name = "TEST2",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 123
            });
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            context.Users = users;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 30,
                Status = 0,
                User = new Models.User()
                {
                    FirstName = "Gargi",
                    LastName = "Pal",
                    EmployeeId = "123456",
                    UserId = 123
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(testTask) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Priority, 30);
        }

        [TestMethod]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();

            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(testTask) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Status, 1);
        }

        [TestMethod]
        public void TestRetrieveTaskByProjectId_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "ANB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "401628",
                First_Name = "Gargi",
                Last_Name = "Pal",
                User_ID = 123,
                Task_ID = 12345,
                Project_ID = 1234
            });
            context.Users = users;
            tasks.Add(new DAC.Task()
            {
                Project_ID = 12345,
                Parent_ID = 12345,
                Task_ID = 12345,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            tasks.Add(new DAC.Task()
            {
                Project_ID = 123,
                Parent_ID = 123,
                Task_ID = 123,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            context.Tasks = tasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(12345) as Models.JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<Models.Task>));
            Assert.AreEqual((result.Data as List<Models.Task>).Count, 1);
            Assert.AreEqual((result.Data as List<Models.Task>)[0].Task_Name, "TEST");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestRetrieveTaskByProjectId_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(-12345) as Models.JSendResponse;
        }





        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(null) as Models.JSendResponse;
        }


        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as Models.JSendResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(null) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as Models.JSendResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(null) as Models.JSendResponse;
        }



        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(task) as Models.JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(task) as Models.JSendResponse;
        }        
    }
}
