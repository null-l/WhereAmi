using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WhereAmi
{

    internal class DragUtility
    {
        public static readonly DependencyProperty EnableDragProperty = DependencyProperty.RegisterAttached(
            "EnableDrag",
            typeof(bool),
            typeof(DragUtility),
            new PropertyMetadata( default(bool), OnLoaded )
        );

        public static void SetEnableDrag( DependencyObject element, bool value )
        {
            element.SetValue( EnableDragProperty, value );
        }

        public static bool GetEnableDrag( DependencyObject element ) => (bool)element.GetValue( EnableDragProperty );

        private static void OnLoaded( DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs )
        {
            if( dependencyObject is not UIElement uiElement || dependencyPropertyChangedEventArgs.NewValue is bool == false )
            {
                return;
            }
            if( (bool)dependencyPropertyChangedEventArgs.NewValue )
            {
                uiElement.MouseMove += UIElementOnMouseMove;
            }
            else
            {
                uiElement.MouseMove -= UIElementOnMouseMove;
            }
        }

        private static void UIElementOnMouseMove( object sender, MouseEventArgs mouseEventArgs )
        {
            if( sender is UIElement uiElement )
            {
                if( uiElement is CheckBox checkBox )
                {
                    checkBox.ReleaseMouseCapture();
                }
                if( mouseEventArgs.LeftButton == MouseButtonState.Pressed )
                {
                    DependencyObject parent = uiElement;
                    var avoidInfiniteLoop = 0;
                    // Search up the visual tree to find the first parent window.
                    while (parent is Window == false)
                    {
                        parent = VisualTreeHelper.GetParent( parent );
                        avoidInfiniteLoop++;
                        if( avoidInfiniteLoop == 1000 )
                        {
                            // Something is wrong - we could not find the parent window.
                            return;
                        }
                    }
                    var window = parent as Window;
                    window.DragMove();
                }
            }
        }
    }

}