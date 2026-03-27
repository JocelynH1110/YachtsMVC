using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;
using Yachts.Repositories;
using Yachts.ViewModels.Yacht;

namespace Yachts.Controllers
{
    public class YachtsController : Controller
    {
        private readonly YachtRepository _repo;

        public YachtsController()
        {
            _repo=new YachtRepository(new DBModelContext());
        }

        // GET: Yachts
        public ActionResult Index(int? id)
        {
            var yachts=_repo.GetProducts();

            if(yachts == null || !yachts.Any())
            {
                return HttpNotFound();
            }

            var selected=id.HasValue?_repo.GetProductByProductId(id.Value):yachts.FirstOrDefault();

            if (selected == null) { 
            return HttpNotFound();
            }

            var vm = new YachtPageViewModel
            {
                Yachts = yachts.Select(y => new YachtListViewModel
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList(),

                SelectedYacht = new YachtDetailViewModel
                {
                    Id= selected.Id,
                    Name = selected.Name,
                    Specification = selected.Specification,
                    Structual = selected.Structual,

                    Sizes = selected.Sizes.Select(s => new YachtSizeViewModel
                    {
                        DimensionName = s.DimensionName,
                        DimensionValue = s.DimensionValue,
                    }).ToList()
                }
            };
            return View(vm);
        }

        public ActionResult DeskPlan(int? id)
        {
            var yachts = _repo.GetProducts();

            if (yachts == null || !yachts.Any())
            {
                return HttpNotFound();
            }

            var selected = id.HasValue ? _repo.GetProductByProductId(id.Value) : yachts.FirstOrDefault();

            if (selected == null)
            {
                return HttpNotFound();
            }

            var yacht=_repo.GetProductByProductId(id.Value);

            if (yacht == null)
            { 
            return HttpNotFound();
            }

            var vm = new YachtPageViewModel
            {
                Yachts = yachts.Select(y => new YachtListViewModel
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList(),

                SelectedYacht = new YachtDetailViewModel
                {
                    Id = selected.Id,
                    Name = selected.Name,
                    Specification = selected.Specification,
                    Structual = selected.Structual,

                    Sizes = selected.Sizes.Select(s => new YachtSizeViewModel
                    {
                        DimensionName = s.DimensionName,
                        DimensionValue = s.DimensionValue,
                    }).ToList(),
                }
            };

            return View(vm);
        }

        public ActionResult Specification(int? id)
        {
            var yachts = _repo.GetProducts();

            if (yachts == null || !yachts.Any())
            {
                return HttpNotFound();
            }

            var selected = id.HasValue ? _repo.GetProductByProductId(id.Value) : yachts.FirstOrDefault();

            if (selected == null)
            {
                return HttpNotFound();
            }

            var yacht = _repo.GetProductByProductId(id.Value);

            if (yacht == null)
            {
                return HttpNotFound();
            }

            var vm = new YachtPageViewModel
            {
                Yachts = yachts.Select(y => new YachtListViewModel
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList(),

                SelectedYacht = new YachtDetailViewModel
                {
                    Id = selected.Id,
                    Name = selected.Name,
                    Specification = selected.Specification,
                    Structual = selected.Structual,

                    Sizes = selected.Sizes.Select(s => new YachtSizeViewModel
                    {
                        DimensionName = s.DimensionName,
                        DimensionValue = s.DimensionValue,
                    }).ToList(),
                }
            };
            return View(vm);
        }
    }
}