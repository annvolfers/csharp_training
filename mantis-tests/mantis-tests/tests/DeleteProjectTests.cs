using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class DeleteProjectTests : AuthTestBase
    {
        [Test]
        public void DeleteProjectTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> oldProjects = new List<ProjectData>();
            if (app.Project.GetProjectCount() == 0)
            {
                ProjectData project = new ProjectData("qqq");
                //app.Project.Create(project);
                app.API.CreateProject(account, project);
            }
            //oldProjects = app.Project.GetProjectList();
            oldProjects = app.API.GetProjectList(account);

            app.Project.Remove(0);

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());


            //List<ProjectData> newProjects = app.Project.GetProjectList();
            List<ProjectData> newProjects = app.API.GetProjectList(account);

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
