using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Menu_ContextMenu.ViewModels
{
    public partial class MenuItemModel : ObservableObject
    {
        public string Header { get; set; }
        public string CommandParameter { get; set; }
        public ObservableCollection<MenuItemModel> Children { get; set; } // 서브 메뉴용 추가
    }

    public partial class MenuContextMenuViewModel : ObservableObject
    {
        //////////////////////////////////////////////////////////////////
        public ObservableCollection<MenuItemModel> FileMenuItems { get; }
        public ObservableCollection<MenuItemModel> ContextMenuItems { get; }

        //////////////////////////////////////////////////////////////////
        ///
        public IRelayCommand<string> MenuCommand { get; }

        //////////////////////////////////////////////////////////////////

        [ObservableProperty]
        private ObservableCollection<MenuItemModel> contextMenuItems2;
        public IRelayCommand<string> ChangeContextMenuCommand { get; }

        //////////////////////////////////////////////////////////////////

        public MenuContextMenuViewModel()
        {
            FileMenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Header = "열기2", CommandParameter = "파일 열기2",
                    Children = new ObservableCollection<MenuItemModel> // 서브메뉴
                    {
                        new MenuItemModel { Header = "최근 파일1", CommandParameter = "최근 파일1 열기" },
                        new MenuItemModel { Header = "최근 파일2", CommandParameter = "최근 파일2 열기" }
                    }
                },
                new MenuItemModel { Header = "저장2", CommandParameter = "파일 저장2" },
                new MenuItemModel { Header = "종료2", CommandParameter = "프로그램 종료2" }
            };

            ContextMenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Header = "새로 만들기2", CommandParameter = "새로 만들기2" },
                new MenuItemModel { Header = "붙여넣기2", CommandParameter = "붙여넣기2" },
                new MenuItemModel { Header = "삭제2", CommandParameter = "삭제2" }
            };

            //////////////////////////////////////////////////////////////////

            MenuCommand = new RelayCommand<string>(OnMenuSelected);

            //////////////////////////////////////////////////////////////////
            
            ContextMenuItems2 = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Header = "새로 만들기", CommandParameter = "새로 만들기" },
                new MenuItemModel { Header = "붙여넣기", CommandParameter = "붙여넣기" },
                new MenuItemModel { Header = "삭제", CommandParameter = "삭제" }
            };

            MenuCommand = new RelayCommand<string>(OnMenuSelected);
            ChangeContextMenuCommand = new RelayCommand<string>(OnContextMenuChange);

        }

        private void OnMenuSelected(string menuItem)
        {
            MessageBox.Show($"메뉴 선택됨: {menuItem}");
        }

        //////////////////////////////////////////////////////////////////

        private void OnContextMenuChange(string mode)
        {
            ContextMenuItems2.Clear();

            if (mode == "파일")
            {
                ContextMenuItems2.Add(new MenuItemModel { Header = "파일 열기", CommandParameter = "파일 열기" });
                ContextMenuItems2.Add(new MenuItemModel { Header = "파일 저장", CommandParameter = "파일 저장" });
            }
            else if (mode == "편집")
            {
                ContextMenuItems2.Add(new MenuItemModel { Header = "복사", CommandParameter = "복사" });
                ContextMenuItems2.Add(new MenuItemModel { Header = "붙여넣기", CommandParameter = "붙여넣기" });
                ContextMenuItems2.Add(new MenuItemModel { Header = "잘라내기", CommandParameter = "잘라내기" });
            }
            else
            {
                ContextMenuItems2.Add(new MenuItemModel { Header = "기본 항목", CommandParameter = "기본" });
            }
        }
    }
}
