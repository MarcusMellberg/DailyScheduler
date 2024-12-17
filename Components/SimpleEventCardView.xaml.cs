using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace DailyScheduler.Components
{
    public partial class SimpleEventCardView : ContentView
    {
        public SimpleEventCardView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(SimpleEventCardView));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), typeof(object), typeof(SimpleEventCardView));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
