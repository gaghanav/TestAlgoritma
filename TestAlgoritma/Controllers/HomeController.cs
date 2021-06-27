using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAlgoritma.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Deret");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Deret()
        {
            var newDeret = new Models.Deret();
            if (TempData["deret"] != null)
            {
                newDeret.Input = TempData["deret"].ToString();
                    List<String> newArr = new List<String>();
                    for (int i = 0; i < newDeret.Input.Length; i++)
                    {
                        newArr.Add(newDeret.Input.Substring(0, newDeret.Input.Length - i));
                    
                    }
                    newDeret.Output = newArr;
                    TempData["deretOutput"] = newDeret.Output;

            }

            return View(newDeret);
        }

        [HttpGet]
        public ActionResult Sort()
        {
            var newSort = new Models.Sort();
            newSort.Ascending = "Ascending";
            if (TempData["sort"] != null)
            {
                newSort.Ascending = TempData["asc"].ToString();
                newSort.Input = TempData["sort"].ToString().ToUpper();
                char temp;
                char[] res = TempData["sort"].ToString().ToCharArray();
                for(int i = 1; i < res.Length; i++)
                {
                    for(int j= 0; j< res.Length - i; j++)
                    {
                        if (newSort.Ascending == "Ascending")
                        {
                            if (res[j] > res[j + 1])
                            {
                                temp = res[j];
                                res[j] = res[j + 1];
                                res[j + 1] = temp;
                            }
                        }
                        else
                        {
                            if (res[j] < res[j + 1])
                            {
                                temp = res[j];
                                res[j] = res[j + 1];
                                res[j + 1] = temp;
                            }
                        }
                    }
                }
                foreach(var data in res)
                {
                    newSort.Output = newSort.Output + data;
                }
                TempData["sortOutput"] = newSort.Output.ToUpper();
            }
            return View(newSort);
        }

        [HttpGet]
        public ActionResult GanjilGenap()
        {
            var newGanjilGenap = new Models.GanjilGenap();
            if(TempData["ganjilGenap"]!= null)
            {
                newGanjilGenap.Input = Convert.ToInt64(TempData["ganjilGenap"].ToString());
                if ((Convert.ToInt64(newGanjilGenap.Input) % 2) != 0) {
                    newGanjilGenap.Output = "Ganjil";
                }
                else
                {
                    newGanjilGenap.Output = "Genap";
                }
            }
            TempData["ganjilGenapOutput"] = newGanjilGenap.Output;
            return View(newGanjilGenap);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deret(FormCollection form)
        {
            TempData["deret"] = form["Input"];
            return RedirectToAction("Deret");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GanjilGenap(FormCollection form)
        {
            TempData["ganjilGenap"] = form["Input"];
            return RedirectToAction("GanjilGenap");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sort(FormCollection form)
        {
            TempData["sort"] = form["Input"];
            TempData["asc"] = form["Ascending"];
            return RedirectToAction("Sort");
        }
    }
}