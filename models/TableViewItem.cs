using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMySQLDemo.Models
{
    public partial class TableViewItem : ObservableObject
    {
        public Table Table { get; set; } = null!;

        [ObservableProperty]
        private bool isReserved;
    }
}
