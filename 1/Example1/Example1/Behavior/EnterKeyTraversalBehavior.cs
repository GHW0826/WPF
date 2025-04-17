using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace Example1.Behavior
{ 
    public class EnterKeyTraversalBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
            => AssociatedObject.PreviewKeyDown += OnKeyDown;

        protected override void OnDetaching()
            => AssociatedObject.PreviewKeyDown -= OnKeyDown;

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                var request = new TraversalRequest(FocusNavigationDirection.Next);
                (Keyboard.FocusedElement as UIElement)?.MoveFocus(request);
            }
        }
    }
}
