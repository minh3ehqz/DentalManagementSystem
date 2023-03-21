using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections;

namespace DentalManagementSystem.Utils
{
    public class FileHelper
    {
        public static FileStream ExportToExcel(List<string> Headers, List<string>Contents)
        {
			try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Dental Management System";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo thống kê";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("Sheet 1");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    // đặt tên cho sheet
                    ws.Name = "Kteam sheet";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 13;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";
                    
                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = Headers.Count();
                    
                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in Headers)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        
                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    foreach (var content in Contents)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;
                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in content.Split('|'))
                        {
                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item;
                        }
                    }

                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    FileStream fs = null;
                    using (fs = new FileStream($"Export{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}", FileMode.Create))
                    {
                        fs.Write(bin, 0, bin.Length);
                    }
                    return fs;
                }
            }
			catch (Exception)
			{

			}
            return null;
        }
    }
}
