using Form_CSGroup.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;



namespace Form_CSGroup.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Check(Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                // Сохраняем данные в Excel файл
                SaveToExcel(contacts);

                // Возвращаем представление с сообщением "Спасибо"
                return View("ThankYou");
            }

            return View("Index");
        }
        private void SaveToExcel(Contacts contacts)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Contacts.xlsx");

            IWorkbook workbook;
            ISheet sheet;

            if (System.IO.File.Exists(filePath))
            {
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }
                sheet = workbook.GetSheet("Contacts") ?? workbook.CreateSheet("Contacts");
            }
            else
            {
                workbook = new XSSFWorkbook();
                sheet = workbook.CreateSheet("Contacts");

                // Создаем заголовки
                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("Name");
                headerRow.CreateCell(1).SetCellValue("Organization");
                headerRow.CreateCell(2).SetCellValue("Phone");
                headerRow.CreateCell(3).SetCellValue("Email");
                headerRow.CreateCell(4).SetCellValue("Position");
                headerRow.CreateCell(5).SetCellValue("Message");
            }

            // Определяем следующую пустую строку
            int rowIndex = sheet.LastRowNum + 1;
            var newRow = sheet.CreateRow(rowIndex);

            // Заполняем строку данными
            newRow.CreateCell(0).SetCellValue(contacts.Name);
            newRow.CreateCell(1).SetCellValue(contacts.Organization);
            newRow.CreateCell(2).SetCellValue(contacts.Phone);
            newRow.CreateCell(3).SetCellValue(contacts.Email);
            newRow.CreateCell(4).SetCellValue(contacts.Position);
            newRow.CreateCell(5).SetCellValue(contacts.Message);

            // Сохраняем изменения в файл
            using (var file = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(file);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
