using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEWINGROUP = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            CloseGroupsDialogue(dialogue);
            return list;
        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox) dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            //ошибка
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialogue(dialogue);
        }

        public void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        public Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public void Remove(GroupData group)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get(SearchCriteria.ByText(group.Name)).Click();
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            manager.MainWindow.ModalWindow(GROUPWINTITLE).ModalWindow(DELETEWINGROUP).Get<Button>("uxOKAddressButton").Click();
            //ошибка
            CloseGroupsDialogue(dialogue);
        }

        public double GetGroupCount()
        {
            Window dialogue = OpenGroupsDialogue();
            int count = dialogue.Get<Tree>("uxAddressTreeView").Nodes[0].Nodes.Count;
            CloseGroupsDialogue(dialogue);
            return count;
        }

        public bool IsGroupPresent()
        {
            bool flag = false;
            Window dialogue = OpenGroupsDialogue();
            if (dialogue.Get<Tree>("uxAddressTreeView").Nodes[0].Nodes.Count > 1)
            {
                flag = true;
            }
            CloseGroupsDialogue(dialogue);
            return flag;
        }
    }
}