using CybageTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;

namespace CybageTest.Controllers
{
    public class CustomerController : DefaultController
    {
        private IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            try
            {
                var URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerList:URL");
                var Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerList:Type");
                //result
                var result = await MakeHttpRequestAsync<List<CustomerList>>(URL, Type == HttpMethods.Get.ToUpper() ? HttpMethod.Get : HttpMethod.Post, null, true);
                var model = new List<CustomerList>();
                model = (List<CustomerList>)result.Data;
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["ErrMsg"] = ex.Message;
                return RedirectToAction("Index","Home");
            }
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerList model)
        {
            try
            {
                Random rnd = new Random();
                var URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerCreate:URL");
                var Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerCreate:Type");
                model.rowKey = rnd.Next(500,1000).ToString();
                model.partitionKey = "US";
                model.eTag = new ETag();
                model.timestamp = DateTime.Now;
                //result
                var result = await MakeHttpRequestAsync<CustomerList>(URL, Type == HttpMethods.Get.ToUpper() ? HttpMethod.Get : HttpMethod.Post, model, true);
                if (result.IsSuccess)
                    TempData["SuccMsg"] = "Successfully created";
                else
                {
                    TempData["ErrMsg"] = $"Getting {result.StatusCode}";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrMsg"] = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerGet:URL");
                var Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerGet:Type");
                URL = string.Format(URL, id);
                //result
                var result = await MakeHttpRequestAsync<CustomerList>(URL, Type == HttpMethods.Get.ToUpper() ? HttpMethod.Get : HttpMethod.Post, null, true);
                var model = new CustomerList();
                model = (CustomerList)result.Data;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrMsg"] = ex.Message;
                return View();
            }
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CustomerList model)
        {
            try
            {
                var URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerUpdate:URL");
                var Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerUpdate:Type");
                URL = string.Format(URL, id);
                model.eTag = new ETag();
                model.timestamp = DateTime.Now;
                //result
                var result = await MakeHttpRequestAsync<CustomerList>(URL, Type == HttpMethods.Get.ToUpper() ? HttpMethod.Get : HttpMethod.Post, model, true);
                if (result.IsSuccess)
                    TempData["SuccMsg"] = "Successfully updated";
                else
                {
                    TempData["ErrMsg"]= $"Getting {result.StatusCode}";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrMsg"]= ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerGet:URL");
                var Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerGet:Type");
                URL = string.Format(URL, id);
                //Get customer details result
                var result = await MakeHttpRequestAsync<CustomerList>(URL, Type == HttpMethods.Get.ToUpper() ? HttpMethod.Get : HttpMethod.Post, null, true);

                URL = _configuration.GetValue<string>("CustomerAPIConfig:CustomerDelete:URL");
                Type = _configuration.GetValue<string>("CustomerAPIConfig:CustomerDelete:Type");
                URL = string.Format(URL, id);

                //Delete result
                result = await MakeHttpRequestAsync<CustomerList>(URL, Type == HttpMethods.Delete.ToUpper() ? HttpMethod.Delete : HttpMethod.Get, result.Data, true);
                if (result.IsSuccess)
                    TempData["SuccMsg"] = "Successfully deleted";
                else
                    TempData["ErrMsg"]= $"Getting {result.StatusCode}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrMsg"]= ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        public static T ConvertJsonToObject<T>(object obj)
        {
            return (T)JsonConvert.DeserializeObject(obj.ToString(), typeof(T));
        }
    }
}
