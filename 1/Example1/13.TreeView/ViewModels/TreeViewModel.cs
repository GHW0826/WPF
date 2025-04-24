using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeView.ViewModels
{
    public enum NodeType
    {
        Folder,
        File,
        User,
        Admin
    }
    public class NodeTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                NodeType.Folder => "/Assets/folder.png",
                NodeType.User => "/Assets/user.png",
                NodeType.Admin => "/Assets/admin.png",
                _ => "/Assets/file.png"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class TreeNode
    {
        public string Name { get; set; }

        public ObservableCollection<TreeNode> Children { get; set; } = new();
    }

    public partial class AuthTreeNode : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isChecked;

        public ObservableCollection<AuthTreeNode> Children { get; set; } = new();
    }

    public partial class IconTreeNode : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private NodeType type;

        public ObservableCollection<IconTreeNode> Children { get; set; } = new();
    }

    public partial class TreeViewModel : ObservableObject
    {
        [ObservableProperty]
        private TreeNode selectedNode;

        public ObservableCollection<TreeNode> RootNodes { get; } = new()
        {
            new TreeNode
            {
                Name = "프로그래밍 언어",
                Children =
                {
                    new TreeNode { Name = "C++" },
                    new TreeNode { Name = "C#" },
                    new TreeNode { Name = "Java" },
                }
            },
            new TreeNode
            {
                Name = "프레임워크",
                Children =
                {
                    new TreeNode { Name = ".NET" },
                    new TreeNode { Name = "Spring" },
                    new TreeNode { Name = "Node.js" },
                }
            }
        };

        partial void OnSelectedNodeChanged(TreeNode value)
        {
            AddChildCommand.NotifyCanExecuteChanged();
            RemoveNodeCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private string searchKeyword;

        [RelayCommand]
        private void Search()
        {
            var found = FindNodeByName(RootNodes, SearchKeyword);
            if (found != null) {
                SelectedNode = found;
            }
        }

        private TreeNode? FindNodeByName(ObservableCollection<TreeNode> nodes, string keyword)
        {
            foreach (var node in nodes) {
                if (node.Name.Contains(keyword))
                    return node;

                var found = FindNodeByName(node.Children, keyword);
                if (found != null)
                    return found;
            }

            return null;
        }


        [RelayCommand(CanExecute = nameof(CanModifyNode))]
        private void AddChild()
        {
            SelectedNode?.Children.Add(new TreeNode { Name = "새 하위 노드" });
        }


        [RelayCommand(CanExecute = nameof(CanModifyNode))]
        private void RemoveNode()
        {
            RemoveNodeRecursive(RootNodes, SelectedNode);
        }

        private bool RemoveNodeRecursive(ObservableCollection<TreeNode> nodes, TreeNode target)
        {
            if (nodes.Remove(target))
                return true;

            foreach (var node in nodes) {
                if (RemoveNodeRecursive(node.Children, target))
                    return true;
            }

            return false;
        }

        private bool CanModifyNode() => SelectedNode != null;




        ////////////////////////////////////////////////////////////////

        [ObservableProperty]
        private AuthTreeNode authSelectedNode;

        private bool CanModifyAuthNode()  => AuthSelectedNode != null;

        public ObservableCollection<AuthTreeNode> AuthRootNodes { get; } = new()
        {
            new AuthTreeNode
            {
                Name = "권한 설정",
                Children =
                {
                    new AuthTreeNode { Name = "읽기" },
                    new AuthTreeNode { Name = "쓰기" },
                    new AuthTreeNode
                    {
                        Name = "관리자",
                        Children =
                        {
                            new AuthTreeNode { Name = "사용자 추가" },
                            new AuthTreeNode { Name = "권한 변경" }
                        }
                    }
                }
            }
        };


        [RelayCommand]
        private void RenameSelected()
        {
            if (AuthSelectedNode != null) {
                AuthSelectedNode.Name += " (수정됨)";
            }
        }

        [RelayCommand]
        private void ToggleCheck()
        {
            if (AuthSelectedNode != null) {
                AuthSelectedNode.IsChecked = !AuthSelectedNode.IsChecked;
            }
        }

        //////////////////////////////////////////////////////////////
        ///
        public ObservableCollection<IconTreeNode> IconRootNodes { get; } = new()
        {
            new IconTreeNode
            {
                Name = "문서",
                Type = NodeType.Folder,
                Children =
                {
                    new IconTreeNode { Name = "기획서.docx", Type = NodeType.File },
                    new IconTreeNode { Name = "보고서.pdf", Type = NodeType.File }
                }
            },
            new IconTreeNode
            {
                Name = "사용자 관리",
                Type = NodeType.Folder,
                Children =
                {
                    new IconTreeNode { Name = "홍길동", Type = NodeType.User },
                    new IconTreeNode { Name = "관리자", Type = NodeType.Admin }
                }
            }
        };
    }
}
