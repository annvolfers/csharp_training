using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string typeofdata = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            if (typeofdata == "groups")
            {
                List<GroupDate> groups = new List<GroupDate>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupDate(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(100),
                        Footer = TestBase.GenerateRandomString(100)
                    });
                }
                if (format == "excel")
                {
                    writeGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        writeGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format - " + format);
                    }
                    writer.Close();
                }
            }
            else if (typeofdata == "contacts")
            {
                List<ContactDate> contacts = new List<ContactDate>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactDate(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
                }
                if (format == "excel")
                {
                    writeContactsToExcelFile(contacts, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        writeContactsToCsvFile(contacts, writer);
                    }
                    else if (format == "xml")
                    {
                        writeContactsToXmlFile(contacts, writer);
                    }
                    else if (format == "json")
                    {
                        writeContactsToJsonFile(contacts, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format - " + format);
                    }
                    writer.Close();
                }
            }
            else
            {
                System.Console.Out.Write("Unrecognized type of data - " + typeofdata);
            }
        }

        static void writeGroupsToExcelFile(List<GroupDate> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupDate group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullpath);
            wb.SaveAs(fullpath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeContactsToExcelFile(List<ContactDate> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactDate contact in contacts)
            {
                sheet.Cells[row, 1] = contact.First_name;
                sheet.Cells[row, 2] = contact.Last_name;
                row++;
            }

            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullpath);
            wb.SaveAs(fullpath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupDate> groups, StreamWriter writer)
        {
            foreach (GroupDate group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeContactsToCsvFile(List<ContactDate> contacts, StreamWriter writer)
        {
            foreach (ContactDate contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1}",
                    contact.First_name, contact.Last_name));
            }
        }

        static void writeGroupsToXmlFile(List<GroupDate> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupDate>)).Serialize(writer, groups);
        }

        static void writeContactsToXmlFile(List<ContactDate> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactDate>)).Serialize(writer, contacts);
        }

        static void writeGroupsToJsonFile(List<GroupDate> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToJsonFile(List<ContactDate> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
