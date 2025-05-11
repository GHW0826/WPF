using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TreeView.ViewModels;

namespace TreeView.Views
{
    public partial class TreeViewView : UserControl
    {
        public TreeViewView()
        {
            InitializeComponent();
            this.Loaded += (_, _) =>
            {
                if (this.DataContext is TreeViewModel vm)
                {
                    Tree.SelectedItemChanged += (s, e) =>
                    {
                        vm.SelectedNode = e.NewValue as TreeNode;
                    };
                    TreeViewControl.SelectedItemChanged += (s, e) =>
                    {
                        vm.AuthSelectedNode = e.NewValue as AuthTreeNode;
                    };
                }
            };
        }
    }
}
