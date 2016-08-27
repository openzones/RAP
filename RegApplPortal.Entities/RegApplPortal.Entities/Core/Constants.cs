namespace RegApplPortal.Entities.Core
{
    public static class Constants
    {
        #region Text Length Limitations

        /// <summary>
        /// Maximal length that string containing a code may have.
        /// </summary>
        public const int CodeMaxLength = 16;

        /// <summary>
        /// Maximal length that string containing a short name may have.
        /// </summary>
        public const int ShortNameMaxLength = 32;

        /// <summary>
        /// Maximal length that string containing a name may have.
        /// </summary>
        public const int NameMaxLength = 255;

        /// <summary>
        /// Maximal length that string containing a middle name may have.
        /// </summary>
        public const int MiddleNameMaxLength = 128;

        /// <summary>
        /// Maximal length that string containing a long name may have.
        /// </summary>
        public const int LongNameMaxLength = 1024;

        /// <summary>
        /// Maximal length that string containing a big name may have.
        /// </summary>
        public const int BigNameMaxLength = 450;

        /// <summary>
        /// Maximal length that string containing some textual description may have.
        /// </summary>
        public const int DescriptionMaxLength = 4000;

        public const int ExperimentalPlanDescriptionMaxLength = 1073741824;

        #endregion Text Length Limitations

        #region References names
        /// <summary>
        /// Справочник со списком справочников
        /// </summary>
        public const string ReferenceRef = "ReferenceRef";
        /// <summary>
        /// Отделение/пункт пользователя
        /// </summary>
        public const string DeliveryCenterRef = "DeliveryCenterRef";

        /// <summary>
        /// Точка
        /// </summary>
        public const string DeliveryPointRef = "DeliveryPointRef";

        public const string RefExpertise = "RefExpertise";
        public const string RefIncomingChannel = "RefIncomingChannel";
        public const string RefLocality = "RefLocality";
        public const string RefMedDocumentType = "RefMedDocumentType";
        public const string RefNomination = "RefNomination";
        public const string RefReason = "RefReason";
        public const string RefStatementType = "RefStatementType";
        public const string RefStatus = "RefStatus";
        public const string RefSubjectInsurance = "RefSubjectInsurance";

        #endregion

        #region Default values
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        #endregion

        public const string FilesRegApplPortal = "FilesRegApplPortal";

        #region Regex
        public const string EmailRegex = @"^(([^<>()[\]\\.,;:\s@\""]+"
                               + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                               + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                               + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                               + @"[a-zA-Z]{2,}))$";
        public const string PhoneRegex = @"^\(([0-9]{3})\)([0-9]{3})-([0-9]{2})-([0-9]{2})$";
        public const string LatinCyrillicNumber = @"^[а-яА-ЯёЁa-zA-Z0-9\\\/]+$";
        public const string Number = @"^[0-9]+$";
        #endregion
    }
}
