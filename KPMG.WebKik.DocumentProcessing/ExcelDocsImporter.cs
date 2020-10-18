using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using KPMG.WebKik.Data;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Registers;
using OfficeOpenXml;
 
namespace KPMG.WebKik.DocumentProcessing
{
    public class ExcelDocsImporter
    {


        public static Dictionary<string, string> UploadRegistersData(byte[] file)
        {
            var dict = new Dictionary<string, string>();
 
            using (var package = new ExcelPackage(new MemoryStream(file)))
            {
                var xlSheet = package.Workbook.Worksheets["Sheet1"];
                var cntRows = 2;
                while (true)
                {
                    if ($"{xlSheet.Cells[cntRows, 1].Value}" == string.Empty) break;
 
                    var kod = xlSheet.Cells[cntRows, 1].Value.ToString();
                    var value = xlSheet.Cells[cntRows, 4].Value ?? "0";
                    dict.Add(kod, value.ToString());
                    cntRows++;
                }
 
            }
            return dict;
        }
 
        //Номер,Название,Полное наименование в латинской транскрипции,Полное наименование в русской транскрипции,Код страны регистрации,Регистрационный номер в стране регистрации,Код налогоплательщика в стране регистрации или аналог,Адрес в стране регистрации,Резидент РФ
 
        /// <summary>
        /// Загрузка иностранных организаций 
        /// </summary>
        /// <param name="file">файл Excel</param>
        /// <returns>Лист организаций. Каждая строка словарь (название поля - значение)</returns>
        public static List<Dictionary<string, string>> GetForeignLigtCompanyList(byte[] file)
        {
            var companyList = new List<Dictionary<string, string>>();
            var cntRows = 2;
            using (var package = new ExcelPackage(new MemoryStream(file)))
            {
                var xlSheet = package.Workbook.Worksheets["Sheet1"];
                while (true)
                {
                    if ($"{xlSheet.Cells[cntRows, 1].Value}" == string.Empty) break;


                    var company = new Dictionary<string, string>
                    {
                        {"Номер", ($"{xlSheet.Cells[cntRows, 1].Value}")},
                        {"Название", ($"{xlSheet.Cells[cntRows, 2].Value}")},
                        {"Полное наименование в латинской транскрипции", ($"{xlSheet.Cells[cntRows, 3].Value}")},
                        {"Полное наименование в русской транскрипции", ($"{xlSheet.Cells[cntRows, 4].Value}")},
                        {"Организационная форма", ($"{xlSheet.Cells[cntRows, 5].Value}")},
                        {"Дата учреждения", ($"{xlSheet.Cells[cntRows, 6].Value}")},
                        {"Наименование и реквизиты документы об учреждении в латинской транскрипции", ($"{xlSheet.Cells[cntRows, 7].Value}")},
                        {"Наименование и реквизиты документы об учреждении в русской транскрипции", ($"{xlSheet.Cells[cntRows, 8].Value}")},
 
                        {"Код страны, в которой учреждена иностранная структура", ($"{xlSheet.Cells[cntRows, 9].Value}")},
                        {"Регистрационный номер иностранной структуры в стране учреждения", ($"{xlSheet.Cells[cntRows, 10].Value}")},
                        {"Иные сведения, характеризующие иностранную структуру", ($"{xlSheet.Cells[cntRows, 11].Value}")},
                        { "Резидент РФ" , $"{xlSheet.Cells[cntRows, 12].Value}"}
                    };
 
                    companyList.Add(company);
                    cntRows++;
                }
            }
            return companyList;
        }
 
        /// <summary>
        /// Парсинг иностранных организаций без ЮЛ
        /// </summary>
        /// <param name="file">файл Excel</param>
        /// <returns>Лист организаций. Каждая строка словарь (название поля - значение)</returns>
        public static List<Dictionary<string, string>> GetForeignCompanyList(byte[] file)
        {
            var companyList = new List<Dictionary<string, string>>();
            var cntRows = 2;
            using (var package = new ExcelPackage(new MemoryStream(file)))
            {
                var xlSheet = package.Workbook.Worksheets["Sheet1"];
                while (true)
                {
                    if ($"{xlSheet.Cells[cntRows, 1].Value}" == string.Empty) break;
                    var company = new Dictionary<string, string>
                    {
                        {"Номер", ($"{xlSheet.Cells[cntRows, 1].Value}")},
                        {"Название", ($"{xlSheet.Cells[cntRows, 2].Value}")},
                        {"Полное наименование в латинской транскрипции", ($"{xlSheet.Cells[cntRows, 3].Value}")},
                        {"Полное наименование в русской транскрипции", ($"{xlSheet.Cells[cntRows, 4].Value}")},
                        {"Код страны регистрации", ($"{xlSheet.Cells[cntRows, 5].Value}")},
                        {"Регистрационный номер в стране регистрации", ($"{xlSheet.Cells[cntRows, 6].Value}")},
                        {"Код налогоплательщика в стране регистрации или аналог", ($"{xlSheet.Cells[cntRows, 7].Value}")},
                        { "Адрес в стране регистрации", ($"{xlSheet.Cells[cntRows, 8].Value}")},
                        {"Дата регистрации",  ($"{xlSheet.Cells[cntRows, 9].Value}")},
                        {"Резидент РФ", ($"{xlSheet.Cells[cntRows, 10].Value}")}
                    };
 
                    companyList.Add(company);
                    cntRows++;
                }
            }
            return companyList;
        }
 
 
        /// <summary>
        /// Парсинг Российских организаций без ЮЛ
        /// </summary>
        /// <param name="file">файл Excel</param>
        /// <returns>Лист организаций. Каждая строка словарь (название поля - значение)</returns>
        public static List<Dictionary<string, string>> GetDomesticCompanyList(byte[] file)
        {
            var companyList = new List<Dictionary<string, string>>();
            var cntRows = 2;
            using (var package = new ExcelPackage(new MemoryStream(file)))
            {
                var xlSheet = package.Workbook.Worksheets["Sheet1"];
                while (true)
                {
                    if ($"{xlSheet.Cells[cntRows, 1].Value}" == string.Empty) break;
                    var company = new Dictionary<string, string>
                    {
                        {"Номер", ($"{xlSheet.Cells[cntRows, 1].Value}")},
                        {"Название", ($"{xlSheet.Cells[cntRows, 2].Value}")},
                        {"Полное наименование", ($"{xlSheet.Cells[cntRows, 3].Value}")},
                        {"ИНН", ($"{xlSheet.Cells[cntRows, 4].Value}")},
                        {"ОГРН", ($"{xlSheet.Cells[cntRows, 5].Value}")},
                        {"КПП", ($"{xlSheet.Cells[cntRows, 6].Value}")},
                        {"Резидент РФ", ($"{xlSheet.Cells[cntRows, 7].Value}")},
                        {"ПАО", ($"{xlSheet.Cells[cntRows, 8].Value}")}
                    };
 
                    companyList.Add(company);
                    cntRows++;
                }
            }
            return companyList;
        }
 
 
        /// <summary>
        /// Прсинг физических лиц
        /// </summary>
        /// <param name="file">файл Excel</param>
        /// <returns>Лист лиц. Каждая строка словарь (название поля - значение)</returns>
        public static List<Dictionary<string, string>> GetIndividualCompanyList(byte[] file)
        {
            var companyList = new List<Dictionary<string, string>>();
            var cntRows = 2;
            using (var package = new ExcelPackage(new MemoryStream(file)))
            {
                var xlSheet = package.Workbook.Worksheets["Sheet1"];
                while (true)
                {
                    if ($"{xlSheet.Cells[cntRows, 1].Value}" == string.Empty) break;
                    var company = new Dictionary<string, string>
                    {
                        {"Резидент РФ", ($"{xlSheet.Cells[cntRows, 1].Value}")},
                        {"ИНН", ($"{xlSheet.Cells[cntRows, 2].Value}")},
                        {"Фамилия", ($"{xlSheet.Cells[cntRows, 3].Value}")},
                        {"Имя", ($"{xlSheet.Cells[cntRows, 4].Value}")},
                        {"Отчество", ($"{xlSheet.Cells[cntRows, 5].Value}")},
                        {"Дата рождения", ($"{xlSheet.Cells[cntRows, 6].Value}")},
                        {"Пол", ($"{xlSheet.Cells[cntRows, 7].Value}")},
                        {"Место рождения", ($"{xlSheet.Cells[cntRows, 8].Value}")},
                        {"Гражданство", ($"{xlSheet.Cells[cntRows, 9].Value}")},
                        {"Код вида документа УЛ", ($"{xlSheet.Cells[cntRows, 10].Value}")},
                        {"Серия и номер документа УЛ", ($"{xlSheet.Cells[cntRows, 11].Value}")},
                        {"Дата выдачи документа УЛ", ($"{xlSheet.Cells[cntRows, 12].Value}")},
                        {"Кем выдан документ УЛ", ($"{xlSheet.Cells[cntRows, 13].Value}")},
                        {"Код вида документа ПР", ($"{xlSheet.Cells[cntRows, 14].Value}")},
                        {"Серия и номер документа ПР", ($"{xlSheet.Cells[cntRows, 15].Value}")},
                        {"Дата выдачи документа ПР", ($"{xlSheet.Cells[cntRows, 16].Value}")},
                        {"Кем выдан документ ПР", ($"{xlSheet.Cells[cntRows, 17].Value}")},
                        {"Код адреса", ($"{xlSheet.Cells[cntRows, 18].Value}")},
                        {"Код региона", ($"{xlSheet.Cells[cntRows, 19].Value}")},
                        {"Почтовый индекс", ($"{xlSheet.Cells[cntRows, 20].Value}")},
                        {"Район", ($"{xlSheet.Cells[cntRows, 21].Value}")},
                        {"Город", ($"{xlSheet.Cells[cntRows, 22].Value}")},
                        {"Населенный пункт", ($"{xlSheet.Cells[cntRows, 23].Value}")},
                        {"Улица", ($"{xlSheet.Cells[cntRows, 24].Value}")},
                        {"Номер дома", ($"{xlSheet.Cells[cntRows, 25].Value}")},
                        {"Номер корпуса", ($"{xlSheet.Cells[cntRows, 26].Value}")},
                        {"Номер квартиры", ($"{xlSheet.Cells[cntRows, 27].Value}")},
                        {"Код страны", ($"{xlSheet.Cells[cntRows, 28].Value}")},
                        {"Адрес", ($"{xlSheet.Cells[cntRows, 29].Value}")}
                    };
 
                    companyList.Add(company);
                    cntRows++;
                }
            }
            return companyList;
        }
 
        /*  public static void TestImportKIK()
          {
              byte[] file = File.ReadAllBytes(@"C:\projects\file.xlsx");
              using (var ms = new MemoryStream(file))
              using (var package = new ExcelPackage(ms))
              {
                  var xlSheet = package.Workbook.Worksheets["Sheet1"];
                  var isContinue = true;
                  var cntRows = 2;
  
                  do
                  {
                      var kod = xlSheet.Cells[cntRows, 1].Value.ToString();
                      var value = xlSheet.Cells[cntRows, 4].Value.ToString();
                      switch (kod)
                      {
                          case "ID001":
                              КИК01.стр1 = value;
                              break;
                          case "ID003":
                              КИК01.стр2.1 = value;
                              break;
                          case "ID004":
                              КИК01.стр2.2 = value;
                              break;
                          case "ID005":
                              КИК01.стр2.3 = value;
                              break;
                          case "ID006":
                              КИК01.стр2.4 = value;
                              break;
                          case "ID007":
                              КИК01.стр2.5 = value;
                              break;
                          case "ID008":
                              КИК01.стр2.6 = value;
                              КИК01 - 01.стр.12,гр.6 = value;
                              break;
                          //                           ....
                          case String.IsNullOrEmpty:
                              isContinue = false;
                              break;
                      }
                      cntRows++;
  
                  } while (isContinue);
              }
          }*/
    }
}