using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfKIK
{
    internal abstract class KikSheetBase
    {
        protected readonly ExcelWorksheet Sheet;
        protected readonly ProjectCompany Company;
        protected readonly Signatory Signatore;
        private int pageNumber;

        protected int PageNumber { get; }
        private string Name => Company.State == State.Domestic
            ? Company.Name
            : string.Join(" ", Company.IndividualCompany.Surname, Company.IndividualCompany.Name, Company.IndividualCompany.MiddleName);
        protected virtual string Inn
        {
            get
            {
                switch (Company.State)
                {
                    case State.Domestic:
                        return Company.DomesticCompany.INN.ToString();
                    case State.Individual:
                        return Company.IndividualCompany.INN.ToString();
                }
                throw new ArgumentException($"Wrong company State. Expected Domestic or Individual. Got {Company.State}");
            }
        }
        protected virtual string Kpp => Company?.DomesticCompany?.KPP;



        protected KikSheetBase(ExcelWorksheet sheet, ProjectCompany company, Signatory signatore, int pageNumber)
        {
            Sheet = sheet;
            Company = company;
            Signatore = signatore;
            PageNumber = pageNumber;
        }

        public KikSheetBase(ExcelWorksheet sheet, ProjectCompany company, int pageNumber)
        {
            Sheet = sheet;
            Company = company;
            this.pageNumber = pageNumber;
        }

        internal abstract void InitValues();

        public void FillCellValue(string key, object value)
        {
            if (value == null) return;

            Sheet.Cells[key].Value = value.ToString();
        }
        public void FillCell(string key, object value)
        {
            if (value == null) return;

            var newVal = value.ToString().Replace(",", ".");

            if (newVal.IndexOf(".") != -1)
            {
                var main = newVal.Split('.')[0];
                var fraction = newVal.Split('.')[1];
                fraction = fraction.Length > 5 ? fraction.Substring(0, 5) : fraction;

                newVal = $"{main}.{fraction}";
            }

            for (int i = Sheet.Cells.Start.Row; i < 70; i++)
                for (int j = Sheet.Cells.Start.Column; j < 150; j++)
                {
                    var val = Sheet.Cells[i, j].Value;

                    if (val != null && val.Equals(key))
                    {
                        for (int k = 0; k < newVal.Length; k++)
                        {
                            Sheet.Cells[i, j, i, j + 2].Value = "'" + newVal[k];
                            j += 3;
                        }

                        return;
                    }
                }
        }
        public void FillLongCell(string key, string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return;

            var splitedVal = SplitByLength(value, length);

            for (int i = 1; i <= splitedVal.Length; i++)
            {
                FillCell(key.Replace("]", $"-{i}]"), splitedVal[i - 1]);
            }
        }
        private string[] SplitByLength(string value, int length)
        {
            int strLength = value.Length;
            int strCount = (strLength + length - 1) / length;
            string[] result = new string[strCount];
            for (int i = 0; i < strCount; ++i)
            {
                result[i] = value.Substring(i * length, Math.Min(length, strLength));
                strLength -= length;
            }
            return result;
        }
        public void ClearCells()
        {
            for (int i = Sheet.Cells.Start.Row; i < 80; i++)
                for (int j = Sheet.Cells.Start.Column; j < 150; j++)
                {
                    var val = Sheet.Cells[i, j].Value;

                    if (val != null)
                    {
                        if (val.ToString().StartsWith("["))
                        {
                            Sheet.Cells[i, j].LoadFromText("");
                        }
                        else if (val.ToString().StartsWith("'"))
                        {
                            Sheet.Cells[i, j].Value = Sheet.Cells[i, j].Value.ToString().Replace("'", "");
                        }
                    }
                }
        }
    }
}
