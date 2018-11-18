using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Common.Components
{
    public class DataGridEx : DataGrid
    {
        public IEnumerable<string> HiddenColumns
        {
            get { return (IEnumerable<string>)GetValue(HiddenColumnsProperty); }
            set { SetValue(HiddenColumnsProperty, value); }
        }


        public static readonly DependencyProperty HiddenColumnsProperty =
            DependencyProperty.Register("HiddenColumns",
                                         typeof(IEnumerable<string>),
                                         typeof(DataGridEx),
                                         new PropertyMetadata(HiddenColumnsChanged));

        private static void HiddenColumnsChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var dg = sender as DataGrid;
            if (dg == null || args.NewValue == args.OldValue)
                return;

            var hiddenColumns = (IEnumerable<string>)args.NewValue;
            if (hiddenColumns == null || hiddenColumns.Count() < 1) {
                foreach (var column in dg.Columns) {
                    column.Visibility = Visibility.Visible;
                }

            } else {
                foreach (var column in dg.Columns) {
                    if (hiddenColumns.Contains((string)column.Header)) {
                        column.Visibility = Visibility.Collapsed;
                    } else {
                        column.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
