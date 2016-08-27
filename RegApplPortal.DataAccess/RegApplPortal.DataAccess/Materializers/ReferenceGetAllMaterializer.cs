using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    class ReferenceGetAllMaterializer : IMaterializer<ReferenceUniversalItem>
    {
        private static readonly ReferenceGetAllMaterializer _instance = new ReferenceGetAllMaterializer();

        public static ReferenceGetAllMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public ReferenceUniversalItem Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<ReferenceUniversalItem> Materialize_List(DataReaderAdapter reader)
        {
            List<ReferenceUniversalItem> items = new List<ReferenceUniversalItem>();

            while (reader.Read())
            {
                ReferenceUniversalItem obj = ReadItemFields(reader);
                items.Add(obj);
            }
            return items;
        }

        public ReferenceUniversalItem ReadItemFields(DataReaderAdapter reader, ReferenceUniversalItem item = null)
        {
            if (item == null)
            {
                item = new ReferenceUniversalItem();
            }
            item.ReferenceName = reader.GetString("ReferenceName");
            item.Id = reader.GetInt64("ID");
            item.Name = reader.GetString("Name");
            item.Code = reader.GetString("Code");
            return item;
        }
    }
}
