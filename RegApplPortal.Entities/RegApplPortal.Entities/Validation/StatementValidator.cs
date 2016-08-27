using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using RegApplPortal.Entities.Validation;
//бизнес-логику можно передавать через контекст

namespace RegApplPortal.Entities.Validation
{
    public class StatementValidator : BaseValidator<Statement>
    {
        public StatementValidator()
        {
        }

        public override void Validate(Statement statement, ModelValidationContext context)
        {
            ValidateInternalFields(statement, context);
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && isValid;
            }
        }

        public override bool IsValidNotCritical
        {
            get
            {
                return base.IsValidNotCritical && isValidNotCritical;
            }
        }

        bool isValid;
        bool isValidNotCritical;

        private void ValidateInternalFields(Statement statement, ModelValidationContext context)
        {
            isValid = true;
            isValidNotCritical = true;

            ValidateDate(statement);
        }

        private void ValidateDate(Statement statement)
        {
            DateTime DateMinimumForDocuments = new DateTime(1991, 1, 1);
            DateTime DateMinimumForPeople = new DateTime(1900, 1, 1);

            //Дата инцидента
            if (statement.IncidentDate != null && statement.IncidentDate < DateMinimumForDocuments)
            {
                isValid = false;
                this.Messages.Add("Общая информация по обращению: " +
                    string.Format("Дата инцидента {0} не может быть меньше {1}. Введите правильную дату.",
                    ((DateTime)statement.IncidentDate).ToShortDateString(), DateMinimumForDocuments.ToShortDateString()));
            }
        }
    }
}
