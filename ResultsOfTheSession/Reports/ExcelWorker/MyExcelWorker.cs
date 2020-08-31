﻿using OfficeOpenXml;
using OfficeOpenXml.Style;
using ResultsOfTheSession.PreparationOfReports.Models.ExpelledStudentsReport;
using ResultsOfTheSession.PreparationOfReports.Models.SessionResultForGroupReport;
using ResultsOfTheSession.PreparationOfReports.Models.SessionResultWithGroupMarksReport;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ResultsOfTheSession.ExcelWorker
{
    public static class MyExcelWorker
    {
        private static void SetBorder(ExcelPackage excel, ExcelWorksheet workSheet, string workSheetName)
        {
            workSheet = excel.Workbook.Worksheets[workSheetName];
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Cells[workSheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[workSheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[workSheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[workSheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private static void SetWorkSheetStyle(ExcelWorksheet workSheet)
        {
            workSheet.TabColor = Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.DefaultColWidth = 20;
        }

        private static void SetRowStyle(ExcelRow row)
        {
            row.Height = 20;
            row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            row.Style.Font.Bold = true;
        }

        public static void WriteToExcel(IEnumerable<SessionResultReportData> dataToWrite, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = null;

            foreach (var data in dataToWrite)
            {
                int currentRow = 1;
                workSheet = excel.Workbook.Worksheets.Add(data.GroupName);

                SetWorkSheetStyle(workSheet);
                SetRowStyle(workSheet.Row(currentRow));

                workSheet.Cells[currentRow, currentRow].Value = data.SessionInfo;
                workSheet.Cells[currentRow, currentRow, currentRow, data.Headers.Length].Merge = true;

                SetRowStyle(workSheet.Row(++currentRow));

                workSheet.Cells[currentRow, currentRow].Value = $"Group: {data.GroupName}";
                workSheet.Cells[currentRow, currentRow, currentRow, data.Headers.Length].Merge = true;

                SetRowStyle(workSheet.Row(++currentRow));

                for (int i = 0; i < data.Headers.Length; i++)
                {
                    workSheet.Cells[currentRow, ++i].Value = data.Headers[--i];
                }

                for (int i = ++currentRow, j = 0; j < data.SessionResultReportRawViews.Count(); i++, j++)
                {
                    workSheet.Cells[i, 1].Value = data.SessionResultReportRawViews.ToList()[j].Surname;
                    workSheet.Cells[i, 2].Value = data.SessionResultReportRawViews.ToList()[j].Name;
                    workSheet.Cells[i, 3].Value = data.SessionResultReportRawViews.ToList()[j].Patronymic;
                    workSheet.Cells[i, 4].Value = data.SessionResultReportRawViews.ToList()[j].Subject;
                    workSheet.Cells[i, 5].Value = data.SessionResultReportRawViews.ToList()[j].Form;
                    workSheet.Cells[i, 6].Value = data.SessionResultReportRawViews.ToList()[j].Date;
                    workSheet.Cells[i, 7].Value = data.SessionResultReportRawViews.ToList()[j].Assessment;
                }

                SetBorder(excel, workSheet, data.GroupName);
            }

            FileStream objFileStrm = File.Create(filePath);
            objFileStrm.Close();

            File.WriteAllBytes(filePath, excel.GetAsByteArray());

            excel.Dispose();
            workSheet.Dispose();
        }

        public static void WriteToExcel(IEnumerable<GroupSessionResultReportData> dataToWrite, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = null;

            foreach (var data in dataToWrite)
            {
                int currentRow = 1;
                workSheet = excel.Workbook.Worksheets.Add(data.AcademicYear);

                SetWorkSheetStyle(workSheet);
                SetRowStyle(workSheet.Row(currentRow));

                workSheet.Cells[currentRow, currentRow].Value = data.SessionName;
                workSheet.Cells[currentRow, currentRow, currentRow, data.Headers.Length].Merge = true;

                SetRowStyle(workSheet.Row(++currentRow));

                for (int i = 0; i < data.Headers.Length; i++)
                {
                    workSheet.Cells[currentRow, ++i].Value = data.Headers[--i];
                }

                for (int i = ++currentRow, j = 0; j < data.GroupSessionResultReportRowViews.Count(); i++, j++)
                {
                    workSheet.Cells[i, 1].Value = data.GroupSessionResultReportRowViews.ToList()[j].GroupName;
                    workSheet.Cells[i, 2].Value = data.GroupSessionResultReportRowViews.ToList()[j].MaxAssessment;
                    workSheet.Cells[i, 3].Value = data.GroupSessionResultReportRowViews.ToList()[j].MinAssessment;
                    workSheet.Cells[i, 4].Value = data.GroupSessionResultReportRowViews.ToList()[j].AvgAssessment;
                }

                SetBorder(excel, workSheet, data.AcademicYear);
            }

            using FileStream objFileStrm = File.Create(filePath);
            objFileStrm.Close();

            File.WriteAllBytes(filePath, excel.GetAsByteArray());

            excel.Dispose();
            workSheet.Dispose();
        }

        public static void WriteToExcel(IEnumerable<ExpelledStudentsReportData> dataToWrite, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = null;

            foreach (var data in dataToWrite)
            {
                int currentRow = 1;
                workSheet = excel.Workbook.Worksheets.Add(data.GroupName);

                SetWorkSheetStyle(workSheet);
                SetRowStyle(workSheet.Row(currentRow));

                workSheet.Cells[currentRow, currentRow].Value = $"{data.GroupName} students to be expelled";
                workSheet.Cells[currentRow, currentRow, currentRow, data.Headers.Length].Merge = true;

                SetRowStyle(workSheet.Row(++currentRow));

                for (int i = 0; i < data.Headers.Length; i++)
                {
                    workSheet.Cells[currentRow, ++i].Value = data.Headers[--i];
                }

                for (int i = ++currentRow, j = 0; j < data.ExpelledStudentsReportRawViews.Count(); i++, j++)
                {
                    workSheet.Cells[i, 1].Value = data.ExpelledStudentsReportRawViews.ToList()[j].Surname;
                    workSheet.Cells[i, 2].Value = data.ExpelledStudentsReportRawViews.ToList()[j].Name;
                    workSheet.Cells[i, 3].Value = data.ExpelledStudentsReportRawViews.ToList()[j].Patronymic;
                }

                SetBorder(excel, workSheet, data.GroupName);
            }

            using FileStream objFileStrm = File.Create(filePath);
            objFileStrm.Close();

            File.WriteAllBytes(filePath, excel.GetAsByteArray());

            excel.Dispose();
            workSheet.Dispose();
        }
    }
}