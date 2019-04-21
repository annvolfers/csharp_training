using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projectList = new List<ProjectData>();
            manager.Navigator.GoToManageProjectPage();
            ICollection<IWebElement> elements = driver
                .FindElement(By.CssSelector("table.table.table-striped.table-bordered.table-condensed.table-hover"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
            foreach (IWebElement element in elements)
            {
                string[] words = new string[element.FindElements(By.TagName("td")).Count];
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = element.FindElements(By.TagName("td"))[i].Text;
                }
                projectList.Add(new ProjectData(words[0])
                {
                    Name = words[0]
                });
            }
            return projectList;
        }

        public void Remove(int index)
        {
            manager.Navigator.GoToManageProjectPage();
            SelectProjectToRemove(index);
            InitProjectRemove();
            SubmitProjectRemove();
        }

        public void SubmitProjectRemove()
        {
            driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-white.btn-round")).Click();
        }

        public void InitProjectRemove()
        {
            driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-sm.btn-white.btn-round")).Click();
        }

        public void SelectProjectToRemove(int index)
        {
            driver.FindElement(By.CssSelector("table.table.table-striped.table-bordered.table-condensed.table-hover"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"))[index]
                .FindElements(By.TagName("td"))[0]
                .FindElement(By.TagName("a"))
                .Click();
        }

        public void Create(ProjectData project)
        {
            manager.Navigator.GoToManageProjectPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-white.btn-round")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-white.btn-round")).Click();
        }

        public int GetProjectCount()
        {
            manager.Navigator.GoToManageProjectPage();
            return driver
                .FindElement(By.CssSelector("table.table.table-striped.table-bordered.table-condensed.table-hover"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr")).Count;
        }
    }
}
