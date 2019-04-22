using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddProjectTests : AuthTestBase
    {
        [Test]
        public void AddProjectTest()
        {
            ProjectData project = new ProjectData("project name");
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            //List<ProjectData> oldProjects = app.Project.GetProjectList();
            List<ProjectData> oldProjects = app.API.GetProjectList(account);

            //app.Project.Create(project);
            app.API.CreateProject(account, project);

            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectCount());

            //List<ProjectData> newProjects = app.Project.GetProjectList();
            List<ProjectData> newProjects = app.API.GetProjectList(account);
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
