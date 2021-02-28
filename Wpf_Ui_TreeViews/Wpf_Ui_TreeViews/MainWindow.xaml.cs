
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace Wpf_Ui_TreeViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

   

    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default Constructor 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded 
        /// <summary>
        ///  Wnen appliaction First Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create new item for tree view
                TreeViewItem item = new TreeViewItem();
                
                // Set the header
                item.Header = drive;
                item.Tag = drive;
                
                // Add a dumny item
                item.Items.Add(null);

                // Listen out for item veing expandeed
                item.Expanded += FolderExpanded;
                
                // Add it to the main tree view
                FolderView.Items.Add(item);
          
                
            }

        }
        #endregion

        #region Folder Expanded

        private void FolderExpanded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = (TreeViewItem)sender;
                
                // If the item only contains the dumny data 
                if (item.Items.Count != 1 && item.Items[0] != null)
                {
                    return;
                }
                
                // Clear dumny data
                item.Items.Clear();
                
                // Get full path
                var fullPath = (string)item.Tag;

                #region Get Directiores

                // Create blank list
                var directories = new List<string>();

                // Ignore errors
                // Try get directories
                try
                {
                    var dirs = Directory.GetDirectories(fullPath);

                    if (dirs.Length > 0)
                    {
                        directories.AddRange(dirs);
                    }
                }
                catch 
                { 
                }

                // For each directiory... 

                directories.ForEach(directoryPath => 
                {
                    // Create directory item
                    var subItem = new TreeViewItem()
                    {
                        // Set header
                        Header = GetFilefolderName(directoryPath),
                        Tag = directoryPath
                    };

                  
                    // Add dumnt item
                    subItem.Items.Add(null);

                    // Add handle
                    subItem.Expanded += FolderExpanded;

                    // Add item to parent

                    item.Items.Add(subItem);
                });
                #endregion

                #region Get files

                var files = new List<string>();

                // Ignore errors
                // Try get files
                try
                {
                    var fs = Directory.GetFiles(fullPath);

                    if (fs.Length > 0)
                    {
                        files.AddRange(fs);
                    }
                }
                catch
                {
                }

                // For each files... 

                files.ForEach(filePath =>
                {
                    // Create file item
                    var subItem = new TreeViewItem()
                    {
                        // Set header
                        Header = GetFilefolderName(filePath),
                        Tag = filePath
                    };

                    // Add item to parent

                    item.Items.Add(subItem);
                });

            
        
        #endregion
    }
        }
        #endregion

        #region Helpers
        private object GetFilefolderName(string path)
        { 
            // No path, return empty
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/','\\');

            // Find the last backlash in the path
            var lastIndex =  normalizedPath.LastIndexOf('\\');

            //If we dont have backlash return it self
            if (lastIndex <= 0)
            {
                return path;
            }

            // Return Name of the last backslash
            return path.Substring(lastIndex+1);
        }
        #endregion



    }
}
