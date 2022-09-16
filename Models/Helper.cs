using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public static class Helper
    {
        public static string jsonToCSV(string jsonContent, string delimiter)
        {
            //    StringWriter csvString = new StringWriter();
            //    using (var csv = new CsvWriter(csvString))
            //    {
            //        csv.Configuration.SkipEmptyRecords = true;
            //        csv.Configuration.WillThrowOnMissingField = false;
            //        csv.Configuration.Delimiter = delimiter;

            //        using (var dt = jsonStringToTable(jsonContent))
            //        {
            //            foreach (DataColumn column in dt.Columns)
            //            {
            //                csv.WriteField(column.ColumnName);
            //            }
            //            csv.NextRecord();

            //            foreach (DataRow row in dt.Rows)
            //            {
            //                for (var i = 0; i < dt.Columns.Count; i++)
            //                {
            //                    csv.WriteField(row[i]);
            //                }
            //                csv.NextRecord();
            //            }
            //        }
            //    }
            //    return csvString.ToString();
            return "";
        }
        public static string ToCsv(this DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                {
                    string s = field.ToString().Replace("\"", "\"\"");
                    if (s.Contains(','))
                        s = string.Concat("\"", s, "\"");
                    return s;
                });
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString().Trim();
        }
        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

    }
}
