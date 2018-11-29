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

        public virtual async Task<IActionResult> EditAsync(Tdto dto, List<string> noUpdateProperties = null)
        {

            if (!ModelState.IsValid)
            {
                return JsonFaild(GetModelStateError());
            }

            var model = await _service.GetAsync(dto.Id);
            if (model == null)
            {
                await _service.InsertAsync(dto);
                return JsonSuccess(string.Empty);
            }
            else
            {

                await _service.UpdateAsync(dto, true, noUpdateProperties);
                return JsonSuccess(string.Empty);
            }
        }

        public virtual async Task<IActionResult> DeleteMutiAsync(string ids)
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


        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {

            await _service.DeleteAsync(id);
            return JsonSuccess();

        }

        public virtual async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return Json(dto);
        }
        /// <summary>
        /// 返回第一个ModelStateError
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }

    }
}
