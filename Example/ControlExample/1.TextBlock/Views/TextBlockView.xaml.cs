using System;
using System.Collections.Generic;
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
using TextBlock.ViewModels;

namespace TextBlock.Views
{
    /// <summary>
    /// TextBlockView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextBlockView : UserControl
    {
        public TextBlockView()
        {
            InitializeComponent();
        }

        private void OnTextBlockLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
            {
                SpawnTextBlock.Inlines.Add(vm.FormattedSpan);
                LinkedTextBlock.Inlines.Add(vm.LinkedSpan);
                EmphasisTextBlock.Inlines.Add(vm.EmphasisSpan);
                
                ////////////////////////////////////////////////////////////////////////

                DynamicTextBlock.Inlines.Clear();
                foreach (var inline in vm.DynamicInlines)
                    DynamicTextBlock.Inlines.Add(inline);

                // Collection 변경 감지하여 Inlines 갱신
                vm.DynamicInlines.CollectionChanged += (_, __) =>
                {
                    DynamicTextBlock.Inlines.Clear();
                    foreach (var i in vm.DynamicInlines)
                        DynamicTextBlock.Inlines.Add(i);
                };

                ////////////////////////////////////////////////////////////////////////

                HighlightTextBlock.Inlines.Clear();
                foreach (var inline in vm.HighlightedInlines)
                    HighlightTextBlock.Inlines.Add(inline);
                vm.HighlightedInlines.CollectionChanged += (_, __) =>
                {
                    HighlightTextBlock.Inlines.Clear();
                    foreach (var i in vm.HighlightedInlines)
                        HighlightTextBlock.Inlines.Add(i);
                };
            }
        }

        private void OnKoreanClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
                vm.SetCulture("ko");
        }

        private void OnEnglishClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
                vm.SetCulture("en");
        }

        ////////////////////////////////////////////////////////////////////////

        private void OnGreetingClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
                vm.AddGreeting();
        }

        private void OnAlertClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
                vm.AddAlert();
        }

        ////////////////////////////////////////////////////////////////////////
        private void OnApplyMessage(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBlockViewModel vm)
                vm.SetMessage("주의: 네트워크 연결이 불안정합니다. 작업 실패 가능성이 있습니다.");
        }
    }
}
