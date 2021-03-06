using SV18T1021193.BusinessLayer;
using SV18T1021193.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021193.Web.Controllers
{
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(page,
                pageSize,
                searchValue,
                out rowCount);
            Models.BasePaginationResult model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = data
            };
            return View(model);
        }
        public ActionResult Create()
        {
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };

            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("edit/{supplierID?}")]
        public ActionResult Edit(string supplierID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            Supplier model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View("Create", model);
        }
        public ActionResult Save(Supplier model)
        {
            if (string.IsNullOrWhiteSpace(model.SupplierName))
            {
                ModelState.AddModelError("SupplierName", "Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.ContactName))
            {
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.City))
            {
                ModelState.AddModelError("City", "Thành phố không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.Country))
            {
                ModelState.AddModelError("Country", "Country không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.PostalCode))
            {
                ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                return View("Create", model);
            }

            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// 
        [Route("delete/{supplierID?}")]
        public ActionResult Delete(string supplierID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}