using System.IO;
using System.Data;
using UnityEngine;
using Excel;

public class ExcelTools  {
    public static DataSet LoadExcel(string filePath) {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        return excelReader.AsDataSet();
    }
}
