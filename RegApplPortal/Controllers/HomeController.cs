using RegApplPortal.BusinessLogic;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Interfaces;
using RegApplPortal.WebApps.Models;
using RegApplPortal.WebApps.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using RegApplPortal.Configuration;
using System.Web;
using System.Threading.Tasks;
using RegApplPortal.WebApps.Models.ViewModels;
using AutoMapper;
using RegApplPortal.Entities.Core.Exceptions;
using System.Net;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Validation;

namespace RegApplPortal.WebApps.Controllers
{
	[AuthorizeUser]
	public class HomeController : RegApplPortalController
	{
		[HttpGet]
		public async Task<ActionResult> Index()
		{
			try
			{
                var directory = await GetDirectoryAsync();
				var model = new JournalStatements(directory);
				model.Users = userBusinessLogic.Find(" ");
				model.Journal = statementBusinessLogic.Statement_Find(new StatementSearchCriteria());

                return View(model);
			}
			catch(Exception ex)
			{
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при загрузке журнала.");
				var model = new JournalStatements(exceptionDetails);
				return View(model);
			}
		}
		[HttpPost]
		public async Task<JsonResult> SearchJournal(StatementSearchCriteria filter)
		{
			try
			{
				var journal = statementBusinessLogic.Statement_Find(filter);

				return Json(journal);
			}
			catch (Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при получении списка обращений.");
				return Json(exceptionDetails);
			}
		}
		[HttpGet]
		public async Task<ActionResult> Statement(string id)
		{
			try
			{
				var directory = await GetDirectoryAsync();
				var model = new StatementList(directory);
				model.Users = userBusinessLogic.Find(" ");
				model.CurrentUser = CurrentUser;
				model.Roles = Role.Roles; // userBusinessLogic.Role_GetList();

				if (!string.IsNullOrEmpty(id))
				{
					model.Statement = statementBusinessLogic.Statement_Get(Convert.ToInt64(id));
					model.Status = new StatementStatus();					
					model.File = new Entities.File();
					model.Execution = new Execution();
				}
				else
				{
					model.Statement = new Statement();
					model.Status = new StatementStatus();
					model.File = new Entities.File();
					model.Execution = new Execution();
				}

				return View(model);
			}
			catch (Exception ex)
			{
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при загрузке обращения.");
				var model = new StatementList(exceptionDetails);
				return View(model);
			}
		}
		
		[HttpPost]
		public JsonResult SaveStatement(Statement statement)
		{
            try
            {
				var result = statementBusinessLogic.Statement_Save(statement);
				return Json(result);
			}
			catch (Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при сохранении обращения.");
				return Json(exceptionDetails);
			}
		}

		[HttpPost]
		public JsonResult SaveStatus(StatementStatus status)
		{
			try
			{
				List<StatementStatus> list = new List<StatementStatus>() { status };
				var result = statementBusinessLogic.StatementStatus_Save(list);
                Notification.SendEmailChangeStatus(status);
				return Json(new { StatementID = status.StatementID } );
			}
			catch(Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при сохранении статуса.");
				return Json(exceptionDetails);
			}
		}

		[HttpPost]
		public JsonResult SaveStatementAll(Statement item)
		{
            ////Для примера
            //item.Validate(new ModelValidationContext());
            //if (item.IsValid())
            //{
            //    //если все нормально - то сохраняем

            //}
            //else
            //{
            //    var listError = item.Messages;
            //    //return Json(listError);
            //}

            try
			{
				var result = statementBusinessLogic.Statement_SaveAll(item);
                item.Id = result;
                Notification.SendEmailChangeStatus(item);
				return Json(new { StatementID = result });
			}
			catch (Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при сохранении обращения.");
				return Json(exceptionDetails);
			}
		}
		[HttpPost]
		public JsonResult GetStatement(long id)
		{
			try
			{
				var result = statementBusinessLogic.Statement_Get(id);

				return Json(result);
			}
			catch(Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при обновлении обращения.");
				return Json(exceptionDetails);
			}
		}

		[HttpPost]
		public JsonResult SaveExecution(Execution item)
		{
			try
			{
				var result = statementBusinessLogic.Execution_Save(item);

				return Json(new { StatementID = item.StatementID });
			}
			catch (Exception ex)
			{
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var exceptionDetails = new ExceptionDetails(ex, "Ошибка при обновлении обращения.");
				return Json(exceptionDetails);
			}
		}

		[HttpPost]
        public JsonResult SaveFile(Entities.File item)
        {
			if (Request.Files == null || Request.Files.Count < 1)
            {
                throw new Exception("Выберите файл для загрузки");
            }
            try
            {
                //сохраняем на диск
                HttpPostedFileBase upload = Request.Files[0];
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                string path = Path.Combine(ConfiguraionProvider.FileStorageFolder, filename);
                upload.SaveAs(path);

				//дозаполняем данными
				item.AttachmentDate = DateTime.Now;
				item.UserID = CurrentUser.Id;
				item.FileName = Request.Files[0].FileName; //оригинальное название файла
				item.FIleUrl = Path.Combine(filename); //имя, как мы его сохраняем

				//сохраняем в БД
				List<Entities.File> files = new List<Entities.File>() { item };
                var result = statementBusinessLogic.File_Save(files);
                return Json(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var exceptionDetails = new ExceptionDetails(ex, "Ошибка при сохранении файла(-ов).");
                return Json(exceptionDetails);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFile(long? fileId)
        {
            Entities.File file = statementBusinessLogic.File_Get(fileId.Value);

            try
            {
                var filePath = Path.Combine(ConfiguraionProvider.FileStorageFolder, file.FIleUrl);
                byte[] fileContents;
                using (var fs = System.IO.File.OpenRead(filePath))
                {
                    fileContents = new byte[fs.Length];
                    await fs.ReadAsync(fileContents, 0, fileContents.Length);
                }
                return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
            }
            catch (Exception ex)
            {
                var exceptionDetails = new ExceptionDetails(ex, "Ошибка при получении файла.");
				var model = new StatementList(exceptionDetails);
				return View("Statement", model);
            }
        }

    }
}