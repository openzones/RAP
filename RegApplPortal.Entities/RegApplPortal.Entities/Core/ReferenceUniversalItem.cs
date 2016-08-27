using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegApplPortal.Entities.Core
{
    public class ReferenceUniversalItem : ReferenceItem
    {
        public string DisplayName { get; set; }


        /// <summary>
        /// Название справочника
        /// </summary>
        public string ReferenceName { get; set; }
    }
}
