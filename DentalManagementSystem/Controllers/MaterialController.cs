using System.Net;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
	public class MaterialController : AuthController
	{
		MaterialDBContext DB = new MaterialDBContext();
		ExportMaterialDBContext DBExport = new ExportMaterialDBContext();

		// GET: ExportMaterial
		//public IActionResult Index(long? id, string name, string unit, int? amount, int? price)
		//{
		//	TempData["name"] = (name ?? "");
		//	TempData["unit"] = (unit ?? "");
		//	if (id != null || name != null || unit != null || amount != null || price != null)
		//	{
		//		var result = DB.Materials.Where(x => (!id.HasValue || x.Id == id.Value)
		//		&& x.Name.ToString().Contains((name ?? ""))
		//		&& x.Id == id).ToList();
		//		return View(result);
		//	}
		//	var materialList = DB.ListAll();
		//	return View(materialList);
		//}

		public IActionResult Index(string textSearch, int page = 1)
		{
			if (!isAuth("/Material",out User user))
			{
				return NotFound();
			}
			ViewData["searchContent"] = textSearch;
			int count = DB.ListAll((string)ViewData["searchContent"]).Count();
			ViewData["thisPage"] = page;
			ViewData["stt"] = page - 1;
			ViewData["numberOfPage"] = count % 10 == 0 ? (count / 10) : (count / 10) + 1;
			var list = DB.ListInPage(page, (string)ViewData["searchContent"]);
			return View(list);
		}

		// Details information of a record
		public IActionResult Details(long id)
		{
			var material = DB.Get(id);
			return View(material);
		}

		// get: Add 1 record
		public IActionResult Create()
		{
			return View();
		}

		// POST: Add 1 record 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id, Name, Unit, Amount, Price")] Material material)
		{
			TempData["addsuccess"] = "thêm mới thành công";
			DB.Add(material);
			return RedirectToAction(nameof(Index));
		}

		// GET: Change information of a record
		public IActionResult Edit(long id)
		{
			var material = DB.Get(id);
			if (material == null)
			{
				return NotFound();
			}
			else
				return View(material);
		}

		// POST: Change information of a record
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(long id, [Bind("Id, Name, Unit, Amount, Price")] Material material)
		{
			DB.Update(material);
			TempData["editsuccess"] = "edit thành công";
			return RedirectToAction("Details", new { id = material.Id });
		}


		// Delete a record 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(long[] selectedValues)
		{
			TempData["Delete messenger"] = "xóa thành công";
			foreach (long id in selectedValues)
			{
				DB.Delete(id);
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult checkName(string name)
		{
			var checkName = DB.getName(name);
			if (checkName != null)
			{
				return Ok("Tên vật tư đã tồn tại");
			}
			else
			{
				return Ok("Valid");
			}
		}

		public IActionResult Export(long id, int amount)
		{
			var checkAmount = DB.getAmount(id);

			if (amount <= checkAmount)
			{
				return Ok("");
			}
			else
			{
				return Ok("Số lượng phải nhỏ hơn hoặc bằng số lượng có trong kho");
			}
		}
	}
}
