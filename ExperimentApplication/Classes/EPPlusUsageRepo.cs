using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ExperimentApplication.Classes
{
    public static class EPPlusUsageRepo
    {
        /// <summary>
        /// Attempt to parse information on named table.
        /// </summary>
        /// <param name="tableName"></param>
        public static void ParseTableInformation(string tableName = "AttendanceData")
        {
            var existingFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTest.xlsx");
            
            using (var package = new ExcelPackage(existingFile))
            {
                var ws = package.Workbook.Worksheets["Attendance Data"];
                var table = ws.Tables[tableName];
                var start = table.Address.Start;
                var end = table.Address.End;

                
                for (var row = start.Row + 1; row <= end.Row; row++) // skip the column header
                { // Row by row...
                    for (var col = start.Column; col <= end.Column; col++)
                    {
                        // ... Cell by cell...
                        var cellValue = ws.Cells[row, col].Text; // This got me the actual value I needed.
                        Console.WriteLine($"Row {row} Column {table.Columns[col - 1].Name}: {cellValue}");
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to parse information on named cell.
        /// </summary>
        /// <param name="cellName"></param>
        public static void ParseNamedCellInformation(string cellName = "ProjectName")
        {
            var existingFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTest.xlsx");

            using (var pck = new ExcelPackage(existingFile))
            {
                var wb = pck.Workbook; //Not workSHEET
                var namedCell1 = wb.Names[cellName];
                Console.WriteLine($"Parsed cell {cellName} : {namedCell1.Text}");
            }
        }

        /// <summary>
        /// Attempt to populate data and ensure the table extent is correct so that formula works correctly.
        /// </summary>
        /// <param name="tableName"></param>
        public static void PopulateTableData(string tableName = "AttendanceData")
        {
            var existingFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTestNoData.xlsx");
            var newFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTest (Output).xlsx");

            using (var package = new ExcelPackage(existingFile))
            {
                var ws = package.Workbook.Worksheets["Attendance Data"];
                var table = ws.Tables[tableName];
                var start = table.Address.Start;
                var body = ws.Cells[start.Row + 1, start.Column];

                var outRange = body.LoadFromDataTable(MakeDummyTable(tableName), false);
                // or however you wish to populate the table

                var newRange = $"{start.Address}:{outRange.End.Address}";
                var tableElement = table.TableXml.DocumentElement;
                tableElement.Attributes["ref"].Value = newRange;
                tableElement["autoFilter"].Attributes["ref"].Value = newRange;

                package.SaveAs(newFile);
            }
        }

        private static DataTable MakeDummyTable(string tableName)
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable(tableName);
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.AutoIncrement = false;
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Email";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Address";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Cohort";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["Name"];
            table.PrimaryKey = PrimaryKeyColumns;

            // Create three new DataRow objects and add 
            // them to the DataTable
            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["Name"] = $"Name:{i}";
                row["Email"] = $"Email:{i}";
                row["Address"] = $"Address:{i}";
                row["Cohort"] = i;
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Attempt to find a named cell in the workbook and change the value and save the excel file.
        /// </summary>
        /// <param name="cellName"></param>
        public static void EditNamedCellInformation(string cellName = "ProjectName")
        {
            var existingFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTestNoData.xlsx");
            var newFile = new FileInfo(@"C:\Users\Ronny\Desktop\SampleTest (Output).xlsx");

            using (var pck = new ExcelPackage(existingFile))
            {
                var wb = pck.Workbook;
                var namedCell1 = wb.Names[cellName];
                namedCell1.Value = "Change this";
                pck.SaveAs(newFile);
            }
        }
    }
}
