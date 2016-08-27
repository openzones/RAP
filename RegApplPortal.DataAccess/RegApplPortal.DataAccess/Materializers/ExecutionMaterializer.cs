using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class ExecutionMaterializer : IMaterializer<Execution>
    {
        private static readonly ExecutionMaterializer _instance = new ExecutionMaterializer();

        public static ExecutionMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public Execution Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<Execution> Materialize_List(DataReaderAdapter reader)
        {
            List<Execution> items = new List<Execution>();

            while (reader.Read())
            {
                Execution obj = ReadItemFields(reader);
                items.Add(obj);
            }

            return items;
        }

        public Execution ReadItemFields(DataReaderAdapter dataReader, Execution item = null)
        {
            if (item == null)
            {
                item = new Execution();
            }
            item.Id = dataReader.GetInt64("ID");
            item.StatementID = dataReader.GetInt64("StatementID");
            item.Validity = dataReader.GetBoolean("Validity");
            item.Judicial = dataReader.GetBoolean("Judicial");
            item.ExpertiseDate = dataReader.GetDateTimeNull("ExpertiseDate");
            item.FinancialSanctions = dataReader.GetFloatNull("FinancialSanctions");
            item.Straf = dataReader.GetFloatNull("Straf");
            item.DescriptionExecution = dataReader.GetString("DescriptionExecution");
            item.LPU_Code = dataReader.GetString("LPU_Code");
            item.LPU_Name = dataReader.GetString("LPU_Name");
            return item;
        }
    }
}
