using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Ui_TreeViews
{
    public static class DirectoryStructure
    {

        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
                // Create blank list
                var items = new List<DirectoryItem>();

                // Ignore errors
                // Try get directories
                try
                {
                    var dirs = Directory.GetDirectories(fullPath);

                    if (dirs.Length > 0)
                    {
                        items.AddRange(dirs.Select(dir => new DirectoryItem {FullPath = dir,Type = DirectoryItemType.directory }));
                    }
                }
                catch
                {
                }

             
                try
                {
                    var fs = Directory.GetFiles(fullPath);

                    if (fs.Length > 0)
                    {
                        items.AddRange(fs.Select( dir => new DirectoryItem{FullPath = dir,Type = DirectoryItemType.file }));
                    }
                }
                catch
                {
                }

            return items;

            }

        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive =>  new DirectoryItem {FullPath = drive,Type = DirectoryItemType.drive}).ToList();
        }

        #region Helpers
        public static string  GetFilefolderName(string path)
        {
            // No path, return empty
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backlash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If we dont have backlash return it self
            if (lastIndex <= 0)
            {
                return path;
            }

            // Return Name of the last backslash
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
