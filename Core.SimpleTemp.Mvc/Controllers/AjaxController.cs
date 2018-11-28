using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    public class AjaxController<Tdto, TEntity, TService> : CoreController where TService : IBaseAppService<Tdto, TEntity> where Tdto : Dto
    {
        TService _service;
        public AjaxController(TService service)
        {
            _service = service;
        }

        public virtual IActionResult Index()
        {
            return View("Index");
        }

        public virtual async Task<IActionResult> EditAsync(Tdto dto)
        {
            try
            {
                var model = await _service.GetAsync(dto.Id);
                if (model == null)
                {
                    await _service.InsertAsync(dto);
                    return JsonSuccess(string.Empty);
                }
                else
                {

                    await _service.UpdateAsync(dto);
                    return JsonSuccess(string.Empty);
                }
            }
            catch (Exception ex)
            {
                return JsonFaild(ex.Message);

            }
        }

        public virtual async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }

                await _service.DeleteBatchAsync(delIds);
                return JsonSuccess();
            }
            catch (Exception ex)
            {
                return JsonFaild(ex.Message);
            }
        }


        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return JsonSuccess();
            }
            catch (Exception ex)
            {
                return JsonFaild(ex.Message);
            }
        }

        public virtual async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return Json(dto);
        }
    }
}
