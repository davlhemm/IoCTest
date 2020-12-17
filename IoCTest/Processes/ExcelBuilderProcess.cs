using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IoCTest.Helpers;
using OfficeOpenXml;
using static IoCTest.Processes.LineListConfig;

namespace IoCTest.Processes
{
    public class ExcelBuilderProcess
    {
        /// <summary>
        /// Creates a dictionary of all the excel data.
        /// </summary>
        /// <param name="excelInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, IList<LineListWriterInfo>> LoadExcelRows(ExcelInfo excelInfo, List<LineListWriterInfo> list, string lookup = "LOOKUP")
        {
            string excelPath = excelInfo.FilePath;

            if (string.IsNullOrWhiteSpace(excelPath) || !File.Exists(excelPath))
                throw new System.Exception($"The path to the Excel file doesn't exist: {excelPath}");

            Dictionary<string, IList<LineListWriterInfo>> excelRows = new Dictionary<string, IList<LineListWriterInfo>>();

            using (FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    try
                    {
                        ExcelWorkbook book = package.Workbook;
                        ExcelWorksheets sheets = book.Worksheets;
                        ExcelWorksheet sheet = sheets.FirstOrDefault(w => w.Name == excelInfo.SheetName);

                        // Ensure sheet exists
                        if (sheet != null)
                        {
                            int totalColumns = sheet.Dimension.End.Column;
                            int rowNum = excelInfo.StartingRow;

                            // While we're not at the last row
                            do
                            {
                                // Each column of the list configuration
                                foreach (LineListWriterInfo col in list)
                                {
                                    // If we have indeces to grab
                                    if (col.Index.Count != 0)
                                    {
                                        if (col.Index.Count > 1)
                                        {
                                            List<string> values = new List<string>();

                                            // Walk down and populate required indeces for this column
                                            foreach (string s in col.Index)
                                            {
                                                Int32.TryParse(s, out var number);

                                                values.Add(sheet.Cells[rowNum, number].Text);
                                            }

                                            col.Value = String.Format(col.Format, values.ToArray());
                                        }
                                        else
                                        {
                                            Int32.TryParse(col.Index[0], out var colNum);

                                            string theText = sheet.Cells[rowNum, colNum].Text;
                                            string colFormat = col.Format;
                                            col.Value = String.Format(colFormat, theText);
                                        }
                                    }
                                }

                                //LOOKUP is a constructed value in the config file that is used to give
                                //each row in excel a unique value. (ex. LineSize-LineNumber)
                                //TODO: Support alteration of this (configurable)
                                var lookupValue = list.First(x => x.Name == lookup).Value;

                                //Grab all of the excel rows that map to the described key
                                var lookUpKeys = excelRows.Keys.FirstOrDefault(p => p == lookupValue);

                                if (lookUpKeys == null && !String.IsNullOrWhiteSpace(lookupValue))
                                {
                                    excelRows.Add(lookupValue, list);
                                }

                                rowNum++;
                            }
                            while (!ExcelBuilderProcess.IsLastRow(sheet, rowNum, excelInfo.ColumnKey));

                            return excelRows;
                        }
                        else
                        {
                            string existingSheets = string.Empty;
                            foreach (ExcelWorksheet ws in sheets)
                            {
                                existingSheets += ", \"" + ws.Name + "\"";
                            }
                            existingSheets.TrimStart(",".ToCharArray());

                            throw new System.Exception(
                                $"The worksheet \"{excelInfo.SheetName}\" is empty. Present sheets are{existingSheets}");
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// Build out key-value pairs for specified sheets as a reference
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="keyIndex"></param>
        /// <param name="valueIndex"></param>
        /// <param name="lookAhead">Number of rows to look ahead and terminate building</param>
        /// <param name="excelFile">Excel file path</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetExcelDictionary(string sheetName, int keyIndex, int valueIndex, int lookAhead = 1, string excelFile = default)
        {
            if (string.IsNullOrWhiteSpace(excelFile) || !File.Exists(excelFile))
                throw new System.Exception(String.Format("The path to the Excel file doesn't exist: {0}", excelFile));

            //Mapping for 
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            using (FileStream fs = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorkbook wb = package.Workbook;
                    ExcelWorksheet ws = wb.Worksheets.FirstOrDefault(w => w.Name == sheetName);

                    if (ws != null)
                    {
                        int row = 2;

                        do
                        {
                            string key = ws.Cells[row, keyIndex].Text;
                            string value = ws.Cells[row, valueIndex].Text;

                            if (!string.IsNullOrEmpty(key) && !dictionary.Keys.Contains(key))
                            {
                                dictionary.Add(key, value);
                            }

                            row++;
                        }
                        while (!IsLastRow(ws, row, keyIndex, lookAhead));

                    }
                }

                fs.Close();
            }

            return dictionary;
        }

        protected virtual void LoadExplicitCell(ExcelInfo excel, string rowKey, string valKey, int row, string col)
        {
            if (string.IsNullOrWhiteSpace(excel.FilePath) || !File.Exists(excel.FilePath))
                throw new System.Exception($"The path to the Excel file doesn't exist: {excel.FilePath}");

            using (FileStream fs = new FileStream(excel.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    try
                    {
                        ExcelWorkbook wb = package.Workbook;
                        ExcelWorksheet ws = wb.Worksheets.FirstOrDefault(w => w.Name == excel.SheetName);

                        if (ws != null)
                        {
                            string text = ws.Cells[row, col.LetterToInt(0)].Text;
                            if (excel.ExcelRows != null)
                            {
                                try
                                {
                                    excel.ExcelRows[rowKey].FirstOrDefault(p => p.Name == valKey).Value = text;
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// ColumnKey is whichever value you want to use in excel to determine if the row is empty.
        /// Most common example would be a line number since almost every row in the line list should have a line number.
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="row"></param>
        /// <param name="columnKey"></param>
        /// <param name="rowLookAhead">Number of rows to look ahead and terminate building</param>
        /// <returns></returns>
        public static bool IsLastRow(ExcelWorksheet ws, int row, int columnKey, int rowLookAhead = 1)
        {
            if (String.IsNullOrWhiteSpace(ws.Cells[row, columnKey].Text))
            {
                if (String.IsNullOrWhiteSpace(ws.Cells[row + rowLookAhead, columnKey].Text))
                {
                    return true;
                }
            }

            return false;
        }

    }



    public class ExcelInfo
    {
        public string FilePath { get; set; }
        public string SheetName { get; set; }
        public int StartingRow { get; set; }
        public int ColumnKey { get; set; }
        
        //TODO: Why accessible everywhere? Better way to do this...
        public Dictionary<string, IList<LineListWriterInfo>> ExcelRows { get; set; }
        
        
        private ExcelInfo()
        {
            ExcelRows = new Dictionary<string, IList<LineListWriterInfo>>();
        }

        public ExcelInfo(string filePath, string sheet, int start, int column): this()
        {
            FilePath = filePath;
            SheetName = sheet;
            StartingRow = start;
            ColumnKey = column;
        }

        //TODO: Poor design, pull ExcelRows model out of the builder after testing...
        public bool LoadExcelRows(List<LineListWriterInfo> list)
        {
            ExcelBuilderProcess.LoadExcelRows(this, list);
            
            return true;
        }
    }
}