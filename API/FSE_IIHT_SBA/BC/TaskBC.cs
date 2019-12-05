using System.Collections.Generic;
using System.Linq;
using MODEL = FSE_SBA.Models;
using DAC = FSE_IIHT_SBA.DAC;


namespace FSE_SBA.BC
{
    public class TaskBC
    {
        DAC.ProjectManagerEntities dbContext = null;
        public TaskBC()
        {
            dbContext = new DAC.ProjectManagerEntities();
        }

        public TaskBC(DAC.ProjectManagerEntities context)
        {
            dbContext = context;
        }
        public List<MODEL.Task> RetrieveTaskByProjectId(int projectId)
        {
            using (dbContext)
            {
                return dbContext.Tasks.Where(z => z.Project_ID == projectId).Select(x => new MODEL.Task()
                {
                    TaskId = x.Task_ID,
                    Task_Name = x.Task_Name,
                    ParentTaskName = dbContext.ParentTasks.Where(y => y.Parent_ID == x.Parent_ID).FirstOrDefault().Parent_Task_Name,
                    Start_Date = x.Start_Date,
                    End_Date = x.End_Date,
                    Priority = x.Priority,
                    Status = x.Status,
                    User = dbContext.Users.Where(y => y.Task_ID == x.Task_ID).Select(z => new MODEL.User()
                    {
                        UserId = z.User_ID,
                        FirstName = z.First_Name
                    }).FirstOrDefault(),
                }).ToList();
            }

        }

        public List<MODEL.ParentTask> RetrieveParentTasks()
        {
            using (dbContext)
            {
                return dbContext.ParentTasks.Select(x => new MODEL.ParentTask()
                {
                    ParentTaskId = x.Parent_ID,
                    ParentTaskName = x.Parent_Task_Name
                }).ToList();
            }
        }


        public int InsertTaskDetails(MODEL.Task task)
        {
            using (dbContext)
            {

                if (task.Priority == 0)
                {
                    dbContext.ParentTasks.Add(new DAC.ParentTask()
                    {
                        Parent_Task_Name = task.Task_Name

                    });
                }
                else
                {
                    DAC.Task taskDetail = new DAC.Task()
                    {
                        Task_Name = task.Task_Name,
                        Project_ID = task.Project_ID,
                        Start_Date = task.Start_Date,
                        End_Date = task.End_Date,
                        Parent_ID = task.Parent_ID,
                        Priority = task.Priority,
                        Status = task.Status
                    };
                    dbContext.Tasks.Add(taskDetail);
                    dbContext.SaveChanges();

                    var editDetails = (from editUser in dbContext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).ToList();
                    // Modify existing records
                    if (editDetails != null && editDetails.Count > 0)
                    {
                        editDetails.First().Task_ID = taskDetail.Task_ID;
                    }
                }
                return dbContext.SaveChanges();
            }
        }

        public int UpdateTaskDetails(MODEL.Task task)
        {
            using (dbContext)
            {
                var editDetails = (from editTask in dbContext.Tasks
                                   where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                   select editTask).First();
                // Modify existing records
                if (editDetails != null)
                {
                    editDetails.Task_Name = task.Task_Name;
                    editDetails.Start_Date = task.Start_Date;
                    editDetails.End_Date = task.End_Date;
                    editDetails.Status = task.Status;
                    editDetails.Priority = task.Priority;

                }
                var editDetailsUser = (from editUser in dbContext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).First();
                // Modify existing records
                if (editDetailsUser != null)
                {
                    editDetails.Task_ID = task.TaskId;
                }
                return dbContext.SaveChanges();
            }

        }

        public int DeleteTaskDetails(MODEL.Task task)
        {
            using (dbContext)
            {
                var deleteTask = (from editTask in dbContext.Tasks
                                  where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                  select editTask).First();
                // Delete existing record
                if (deleteTask != null)
                {
                    deleteTask.Status = 1;
                }
                return dbContext.SaveChanges();
            }

        }


    }
}