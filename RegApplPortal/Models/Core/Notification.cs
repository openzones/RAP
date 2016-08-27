using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RegApplPortal.WebApps
{
    public static class Notification
    {
        public static bool SendEmailChangeStatus(StatementStatus status)
        {
            if (status == null) return false;
			User AssignedToUser = new User();
            if (status.StatusID != 1)
				AssignedToUser = ReferencesProvider.GetUser(status.AssignedToUserID.Value);

			List<SelectListItem> coordinators = ReferencesProvider.GetCoordinators();
			
			Statement statement = ReferencesProvider.GetStatement(status.StatementID);
			if (statement == null) return false;
			List<ReferenceItem> listStatus = ReferencesProvider.GetReferenceItems(Constants.RefStatus);

			MailDefinition md = new MailDefinition();
			md.From = "complaint@uralsibins.ru";
			md.IsBodyHtml = true;
			md.Subject = "Портал регистрации обращений";
			ListDictionary replacements = new ListDictionary();
			replacements.Add("{fio}", AssignedToUser?.Fullname);
			replacements.Add("{StatementID}", status.StatementID.ToString());
			replacements.Add("{status}", listStatus.Where(a => a.Id == status.StatusID).Select(b => b.Name).FirstOrDefault().ToString());
			replacements.Add("{ExecuteToDate}", status.ExecuteToDate == null ? string.Empty : status.ExecuteToDate.Value.ToShortDateString());
			replacements.Add("{CreateDate}", statement.CreateDate.ToShortDateString());
			replacements.Add("{fioClient}", statement.Lastname + " " + statement.Firstname + " " + statement.Secondname);

			string body = string.Empty;
			body = body + "<div>Уважаемый сотрудник, {fio}.</div>" + Environment.NewLine;
			body = body + "<div>&nbsp;</div>" + Environment.NewLine;
			body = body + "<div>В системе регистрации обращений клиентов на Вас назначена заявка <a href=\" dev_complaint.uralsibins.ru/Home/Statement?id={StatementID}\">№{StatementID}</a></div>" + Environment.NewLine;
			body = body + "<div>Установлен новый статус: [{status}]</div>" + Environment.NewLine;
			body = body + "<div><b>Необходимо рассмотреть до {ExecuteToDate}</b></div>" + Environment.NewLine;
			body = body + "<div>Дата создания заявки {CreateDate}</div>" + Environment.NewLine;
			body = body + "<div>Клиент: {fioClient}</div>" + Environment.NewLine;
			body = body + "<div>&nbsp;</div>" + Environment.NewLine;
			body = body + "<div>С уважением,</div>" + Environment.NewLine;
			body = body + "<div>Система регистрации заявок.</div>" + Environment.NewLine;

			//smtp.uralsibins.ru - можно отправлять без авторизации
			SmtpClient Smtp = new SmtpClient("smtp.uralsibins.ru", 25);

			if (!string.IsNullOrEmpty(AssignedToUser.Email))
			{
				MailMessage msg = md.CreateMailMessage(AssignedToUser.Email.Trim(' ', ','), replacements, body, new System.Web.UI.Control());				
				Smtp.Send(msg);
			}			

			if (status.StatusID == 1)
			{
				foreach (var item in coordinators)
				{
					if (!string.IsNullOrEmpty(item.Value))
					{
						MailMessage msgs = md.CreateMailMessage(item.Value.Trim(' ', ','), replacements, body, new System.Web.UI.Control());
						Smtp.Send(msgs);
					}
				}
			}
			return true;
		}

		public static bool SendEmailChangeStatus(Statement statement)
		{
			if (statement == null) return false;
			var status = statement.StatementStatuses.LastOrDefault();
			if (status == null) return false;
			User AssignedToUser = new User();
			if (status.StatusID != 1)
				AssignedToUser = ReferencesProvider.GetUser(status.AssignedToUserID.Value);

			List<SelectListItem> coordinators = ReferencesProvider.GetCoordinators();
			List<ReferenceItem> listStatus = ReferencesProvider.GetReferenceItems(Constants.RefStatus);

			MailDefinition md = new MailDefinition();
			md.From = "complaint@uralsibins.ru";
			md.IsBodyHtml = true;
			md.Subject = "Портал регистрации обращений";
			ListDictionary replacements = new ListDictionary();
			replacements.Add("{fio}", AssignedToUser?.Fullname);
			replacements.Add("{StatementID}", statement.Id.ToString());
			replacements.Add("{status}", listStatus.Where(a => a.Id == status.StatusID).Select(b => b.Name).FirstOrDefault().ToString());
			replacements.Add("{ExecuteToDate}", status.ExecuteToDate == null ? string.Empty : status.ExecuteToDate.Value.ToShortDateString());
			replacements.Add("{CreateDate}", statement.CreateDate.ToShortDateString());
			replacements.Add("{fioClient}", statement.Lastname + " " + statement.Firstname + " " + statement.Secondname);

			string body = string.Empty;
			body = body + "<div>Уважаемый сотрудник, {fio}.</div>" + Environment.NewLine;
			body = body + "<div>&nbsp;</div>" + Environment.NewLine;
			body = body + "<div>В системе регистрации обращений клиентов на Вас назначена заявка <a href=\" dev_complaint.uralsibins.ru/Home/Statement?id={StatementID}\">№{StatementID}</a></div>" + Environment.NewLine;
			body = body + "<div>Установлен новый статус: [{status}]</div>" + Environment.NewLine;
			body = body + "<div><b>Необходимо рассмотреть до {ExecuteToDate}</b></div>" + Environment.NewLine;
			body = body + "<div>Дата создания заявки {CreateDate}</div>" + Environment.NewLine;
			body = body + "<div>Клиент: {fioClient}</div>" + Environment.NewLine;
			body = body + "<div>&nbsp;</div>" + Environment.NewLine;
			body = body + "<div>С уважением,</div>" + Environment.NewLine;
			body = body + "<div>Система регистрации заявок.</div>" + Environment.NewLine;

			//smtp.uralsibins.ru - можно отправлять без авторизации
			SmtpClient Smtp = new SmtpClient("smtp.uralsibins.ru", 25);
			if (!string.IsNullOrEmpty(AssignedToUser.Email))
			{
				MailMessage msg = md.CreateMailMessage(AssignedToUser.Email.Trim(' ', ','), replacements, body, new System.Web.UI.Control());
				Smtp.Send(msg);
			}

			if (status.StatusID == 1)
			{
				foreach (var item in coordinators)
				{
					if (!string.IsNullOrEmpty(item.Value))
					{
						MailMessage msgs = md.CreateMailMessage(item.Value.Trim(' ', ','), replacements, body, new System.Web.UI.Control());
						Smtp.Send(msgs);
					}
				}
			}

			return true;
		}
	}
}