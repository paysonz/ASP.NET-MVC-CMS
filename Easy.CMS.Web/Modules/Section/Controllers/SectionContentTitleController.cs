﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Data;
using Easy.Web.Attribute;
using Easy.Web.Authorize;

namespace Easy.CMS.Section.Controllers
{
    [PopUp, DefaultAuthorize]
    public class SectionContentTitleController : Controller
    {
        private readonly ISectionContentProviderService _sectionContentProviderService;

        public SectionContentTitleController(ISectionContentProviderService sectionContentProviderService)
        {
            _sectionContentProviderService = sectionContentProviderService;
        }

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentTitle
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                TitleLevel = SectionContentTitle.H3,
                ActionType = ActionType.Create
            });
        }

        public ActionResult Edit(int Id)
        {
            var content = _sectionContentProviderService.Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [HttpPost]
        public ActionResult Save(SectionContentTitle content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                _sectionContentProviderService.Add(content);
            }
            else
            {
                _sectionContentProviderService.Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }

        public JsonResult Delete(int Id)
        {
            _sectionContentProviderService.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
