using System.Windows.Controls;

namespace MaiQuocAnhWPF
{
    public partial class ReportViewControl : UserControl
    {
        public ReportViewControl(string reportType)
        {
            InitializeComponent();
            ReportText.Text = $"{reportType} - Feature coming soon!";
        }
    }
}