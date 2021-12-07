using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace spo8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCode.ShowLineNumbers = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void OpenDialog(bool AllFile)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = AllFile ? "All files (*.*)|*.*|Cs files (*.cs)|*.cs" : "Cs files (*.cs)|*.cs";
            if (openFileDialog.ShowDialog() == true)
            {
                txtCode.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(System.IO.Path.GetExtension(openFileDialog.FileName));
                txtCode.Load(openFileDialog.FileName);
                FullFileName.Text = openFileDialog.FileName;
            }
        }

        //Menu File
        private void OpenCSarp_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog(false);
        }
        private void OpenAll_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog(true);
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (FullFileName.Text != "Path")
                txtCode.Save(FullFileName.Text);
        }
        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                txtCode.Save(dialog.FileName);
                FullFileName.Text = dialog.FileName;
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Clear();
            FullFileName.Text = "Path";
        }
        //Menu Edit
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Cut();
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Copy();
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Paste();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Clear();
        }

        private void txtCode_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
        }

        private void txtCode_Drop(object sender, DragEventArgs e)
        {
            foreach (string obj in (string[])e.Data.GetData(DataFormats.FileDrop))
                if (Directory.Exists(obj))
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    txtCode.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(System.IO.Path.GetExtension(obj.ToString()));
                    txtCode.Load(obj.ToString());
                    FullFileName.Text = obj.ToString();
                }
        }

        private void txtCode_DragLeave(object sender, DragEventArgs e)
        {

        }
    }
}
