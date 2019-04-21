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
            List<ProjectData> oldProjects = new List<ProjectData>();
            if (app.Project.GetProjectCount() == 0)
            {
                ProjectData project = new ProjectData("qqq");
                app.Project.Create(project);
            }
            oldProjects = app.Project.GetProjectList();

            app.Project.Remove(0);

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());


            List<ProjectData> newProjects = app.Project.GetProjectList();

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
