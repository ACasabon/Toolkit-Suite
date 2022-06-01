using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ToolkitSuite.Helpers
{
    public static class MyFileDialog
    {
        public static string fileName;
        public static List<string> fileNames;
        public static string pathsString;

        public static string OpenFile(FilterType filter, string initialDirectory = null)
        {
            string path = String.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = GetFilter(filter);
            //ofd.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(initialDirectory))
                ofd.InitialDirectory = initialDirectory;

            // Show open file dialog box
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                path = ofd.FileName;
                fileName = ofd.SafeFileName;
            }
            return path;
        }

        public static List<string> OpenFiles(FilterType filter)
        {
            List<string> paths = new List<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = GetFilter(filter);
            ofd.RestoreDirectory = true;

            // Show open file dialog box
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                string fileDialogFolderPath = ofd.FileName.Remove(ofd.FileName.LastIndexOf('\\') - 1);
                paths = ofd.FileNames.ToList();
                fileNames = ofd.SafeFileNames.ToList();
                //Paths String
                pathsString = "";
                foreach (var name in fileNames)
                    pathsString += '"' + name + '"' + ' ';
                pathsString = pathsString.Remove(pathsString.LastIndexOf(' '));
            }
            return paths;
        }

        public static string SaveFile(FilterType filter, string fileName = "")
        {
            string path = String.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = fileName;
            sfd.Filter = GetFilter(filter);
            sfd.RestoreDirectory = true;

            // Show open file dialog box
            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                path = sfd.FileName;
            }
            return path;
        }

        static string GetFilter(FilterType filter)
        {
            string filterString = String.Empty;
            switch (filter)
            {
                case FilterType.PdfFilter:
                    filterString = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    break;
                case FilterType.ExcelFilter:
                    filterString = "xlsx files (*.xlsx)|*.xlsx|xls files (*.xls)|*.xls|All files (*.*)|*.*";
                    break;
                case FilterType.SidFilter:
                    filterString = "sid files (*.sid)|*.sid|All files (*.*)|*.*";
                    break;
                case FilterType.TXT:
                    filterString = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                    break;
                case FilterType.WSET:
                    filterString = "settings files (*.wset)|*.wset|All files (*.*)|*.*";
                    break;
                case FilterType.JSON:
                    filterString = "PDF logic files (*.json)|*.json|All files (*.*)|*.*";
                    break;
                default:
                    break;
            }
            return filterString;
        }
    }

    public enum FilterType
    {
        PdfFilter,
        ExcelFilter,
        SidFilter,
        TXT,
        WSET,
        JSON
    }
}
