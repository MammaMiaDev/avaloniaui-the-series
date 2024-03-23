using Avalonia.Controls;

namespace AvaloniaMiaDev.Views;

public partial class ButtonPageView : UserControl
{
    public ButtonPageView()
    {
        InitializeComponent();
    }

    // TODO: replace with MVVM pattern (https://github.com/AvaloniaUI/Avalonia/issues/3766)
    public void OnSpin(object sender, SpinEventArgs e)
    {
        var spinner = (ButtonSpinner)sender;

        if (spinner.Content is not TextBlock txtBox) return;

        var text = txtBox.Text;
        if (!int.TryParse(text, out var value)) return;

        if (e.Direction == SpinDirection.Increase)
        {
            if (value is int.MaxValue) return;
            value++;
        }
        else
        {
            if (value is int.MinValue) return;
            value--;
        }

        txtBox.Text = value.ToString();
    }
}
