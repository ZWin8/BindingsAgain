using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BindingsAgain
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Loaded += MainPage_Loaded;
            this.InitializeComponent();
            // It is impossible to get actual width after initializecomponet.
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            MyConverterClass myConverter = new MyConverterClass();
            PropertyPath p = new PropertyPath("ActualWidth");
            Binding b = new Binding
            {
                ElementName = "page",
                Path = p,
                Converter = myConverter,
                ConverterParameter = "F7"
            };
            WTxt.SetBinding(TextBlock.TextProperty, b);
        }
    }
    // Demonstrate how to change the binding output format in code.
    public class MyConverterClass : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IFormattable && parameter is string && !String.IsNullOrEmpty(parameter as string) && targetType == typeof(string))
            {
                if (String.IsNullOrEmpty(language)) return (value as IFormattable).ToString(parameter as string, null);
                return (value as IFormattable).ToString(parameter as string, new CultureInfo(language));
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
