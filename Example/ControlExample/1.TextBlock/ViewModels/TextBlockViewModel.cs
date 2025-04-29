using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
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

namespace TextBlock.ViewModels
{
    public partial class TextBlockViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "안녕하세요!\nTextBlock 줄바꿈 테스트입니다.";

        ////////////////////////////////////////////////////////////////////////////////
        public ObservableCollection<Inline> FormattedText { get; }

        public Span FormattedSpan { get; }

        ////////////////////////////////////////////////////////////////////////////////
        public Span LinkedSpan { get; }

        ////////////////////////////////////////////////////////////////////////////////

        private readonly ResourceManager _rm = TextBlock.Resources.Strings.ResourceManager;

        [ObservableProperty]
        private string greeting;

        [ObservableProperty]
        private string description;

        ////////////////////////////////////////////////////////////////////////////////
        public Span EmphasisSpan { get; }

        ////////////////////////////////////////////////////////////////////////////////

        public ObservableCollection<Inline> DynamicInlines { get; }

        ////////////////////////////////////////////////////////////////////////////////
        public ObservableCollection<Inline> HighlightedInlines { get; }

        private readonly HashSet<string> keywords = new() { "경고", "실패", "주의" };

        ////////////////////////////////////////////////////////////////////////////////


        public TextBlockViewModel()
        {
            FormattedText = new ObservableCollection<Inline>
            {
                new Run("안녕하세요! FormattedText ") { Foreground = Brushes.DarkSlateBlue, FontSize = 16 },
                new Bold(new Run("강조된 텍스트")) { FontSize = 18 },
                new Run("\n"),
                new Italic(new Run("이탤릭 텍스트입니다.")) { Foreground = Brushes.Gray },
                new Run("\n\n"),
                new Run("MVVM 구조로 TextBlock에 Inlines를 넣었습니다.") { FontWeight = FontWeights.SemiBold }
            };

            FormattedSpan = new Span();
            FormattedSpan.Inlines.Add(new Run("안녕하세요! FormattedSpan ")
            {
                Foreground = Brushes.DarkSlateBlue,
                FontSize = 16
            });
            FormattedSpan.Inlines.Add(new Bold(new Run("강조된 텍스트"))
            {
                FontSize = 18
            });
            FormattedSpan.Inlines.Add(new LineBreak());
            FormattedSpan.Inlines.Add(new Italic(new Run("이탤릭 텍스트입니다."))
            {
                Foreground = Brushes.Gray
            });
            FormattedSpan.Inlines.Add(new LineBreak());
            FormattedSpan.Inlines.Add(new LineBreak());
            FormattedSpan.Inlines.Add(new Run("MVVM 구조로 Inlines 생성!")
            {
                FontWeight = FontWeights.SemiBold
            });

            ////////////////////////////////////////////////////////////////////////////////

            LinkedSpan = new Span();
            LinkedSpan.Inlines.Add(new Run("공식 사이트를 방문하려면: ") { FontSize = 14 });
            var link = new Hyperlink(new Run("Google 홈페이지"))
            {
                NavigateUri = new System.Uri("https://www.google.com"),
                Foreground = Brushes.Blue,
                FontWeight = FontWeights.Bold
            };
            link.RequestNavigate += (s, e) =>
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                e.Handled = true;
            };
            LinkedSpan.Inlines.Add(link);

            ////////////////////////////////////////////////////////////////////////////////

            SetCulture("ko");

            ////////////////////////////////////////////////////////////////////////////////

            EmphasisSpan = new Span();
            EmphasisSpan.Inlines.Add(new Run("문장을 구성하고, 특정 "));
            // 강조 단어
            var keyword = new Run("단어")
            {
                Foreground = Brushes.OrangeRed,
                FontWeight = FontWeights.Bold
            };
            var tooltipped = new Span(keyword)
            {
                ToolTip = new ToolTip
                {
                    Content = "이건 강조된 단어입니다."
                }
            };
            EmphasisSpan.Inlines.Add(tooltipped);
            EmphasisSpan.Inlines.Add(new Run(" 에 마우스를 올리면 설명이 나옵니다."));

            ////////////////////////////////////////////////////////////////////////////////

            DynamicInlines = new ObservableCollection<Inline>();
            AddGreeting();

            ////////////////////////////////////////////////////////////////////////////////

            HighlightedInlines = new ObservableCollection<Inline>();
            SetMessage("이 작업은 실패할 수 있습니다. 경고: 백업을 먼저 수행하세요.");

        }
        ////////////////////////////////////////////////////////////////////////////////

        public void SetMessage(string message)
        {
            HighlightedInlines.Clear();
            foreach (var word in message.Split(' '))
            {
                Run run = new(word + " ");
                if (keywords.Contains(word.Replace(":", "").Replace(".", ""))) // 기본 정리
                {
                    run.Foreground = Brushes.Red;
                    run.FontWeight = FontWeights.Bold;
                }
                else
                {
                    run.Foreground = Brushes.Black;
                }
                HighlightedInlines.Add(run);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public void AddGreeting()
        {
            DynamicInlines.Clear();
            DynamicInlines.Add(new Run("안녕하세요! ")
            {
                FontSize = 16,
                Foreground = Brushes.DarkSlateBlue
            });
            DynamicInlines.Add(new Run("실시간으로 ")
            {
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Teal
            });
            DynamicInlines.Add(new Run("텍스트가 구성됩니다."));
        }

        public void AddAlert()
        {
            DynamicInlines.Clear();
            DynamicInlines.Add(new Run("경고: ")
            {
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Red
            });
            DynamicInlines.Add(new Run("이 작업은 되돌릴 수 없습니다."));
        }
        ////////////////////////////////////////////////////////////////////////////////

        public void SetCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            Greeting = _rm.GetString(nameof(Greeting), culture);
            Description = _rm.GetString(nameof(Description), culture);
        }
    }
}
