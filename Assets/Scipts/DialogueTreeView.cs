using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class DialogueTreeView : TreeView
{
    public DialogueTreeView(TreeViewState state) : base(state)
    {
        Reload();
    }

    protected override TreeViewItem BuildRoot()
    {
        TreeViewItem root = new TreeViewItem() { displayName = "Start Conversation", depth = -1, id = 0 };

        List<TreeViewItem> items = new List<TreeViewItem>()
        {
            new TreeViewItem(){ id = 1, depth = 1, displayName = "First Option" },
            new TreeViewItem(){ id = 2, depth = 1, displayName = "Second Option" },
            new TreeViewItem(){ id = 3, depth = 2, displayName = "First Option Follow Up 1"},
            new TreeViewItem(){ id = 4, depth = 2, displayName = "First Option Follow Up 2"},
            
            new TreeViewItem(5, 2, "Second Option Follow Up")
            
        };

        SetupParentsAndChildrenFromDepths(root, items);

        return root;
    }

}

//public class DialogTreeItem : TreeViewItem
//{
//    public List<DialogOption> followUps = new List<DialogOption>(); 
//}

//public class DialogOption
//{
//    public DialogTreeItem Selection;
//}
