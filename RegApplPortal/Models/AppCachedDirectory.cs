using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Core.Exceptions;
using RegApplPortal.WebApps.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.WebApps.Models
{
	public class AppCachedDirectory
	{
		public AppCachedDirectory()
		{

		}

		public CustomObject LoadDirectory(List<ReferenceUniversalItem> result)
		{			
			var dictionary = new CustomObject();

			foreach (var t in result)
			{
				if (t.ReferenceName.Equals(Constants.DeliveryCenterRef, StringComparison.OrdinalIgnoreCase)) this.DeliveryCenterRef.Add(t); 
				if (t.ReferenceName.Equals(Constants.DeliveryPointRef, StringComparison.OrdinalIgnoreCase)) this.DeliveryPointRef.Add(t);
				if (t.ReferenceName.Equals(Constants.RefExpertise, StringComparison.OrdinalIgnoreCase)) this.RefExpertise.Add(t);
				if (t.ReferenceName.Equals(Constants.RefIncomingChannel, StringComparison.OrdinalIgnoreCase)) this.RefIncomingChannel.Add(t);
				if (t.ReferenceName.Equals(Constants.RefLocality, StringComparison.OrdinalIgnoreCase)) this.RefLocality.Add(t);
				if (t.ReferenceName.Equals(Constants.RefMedDocumentType, StringComparison.OrdinalIgnoreCase)) this.RefMedDocumentType.Add(t); 
				if (t.ReferenceName.Equals(Constants.RefNomination, StringComparison.OrdinalIgnoreCase)) this.RefNomination.Add(t);
				if (t.ReferenceName.Equals(Constants.RefReason, StringComparison.OrdinalIgnoreCase)) this.RefReason.Add(t);
				if (t.ReferenceName.Equals(Constants.RefStatementType, StringComparison.OrdinalIgnoreCase)) this.RefStatementType.Add(t);
				if (t.ReferenceName.Equals(Constants.RefStatus, StringComparison.OrdinalIgnoreCase)) this.RefStatus.Add(t);
				if (t.ReferenceName.Equals(Constants.RefSubjectInsurance, StringComparison.OrdinalIgnoreCase)) this.RefSubjectInsurance.Add(t);
			}

			dictionary.Add(Constants.DeliveryCenterRef, this.DeliveryCenterRef);
			dictionary.Add(Constants.DeliveryPointRef, this.DeliveryPointRef);
			dictionary.Add(Constants.RefExpertise, this.RefExpertise);
			dictionary.Add(Constants.RefIncomingChannel, this.RefIncomingChannel);
			dictionary.Add(Constants.RefLocality, this.RefLocality);
			dictionary.Add(Constants.RefMedDocumentType, this.RefMedDocumentType);
			dictionary.Add(Constants.RefNomination, this.RefNomination);
			dictionary.Add(Constants.RefStatementType, this.RefStatementType);
			dictionary.Add(Constants.RefReason, this.RefReason);
			dictionary.Add(Constants.RefStatus, this.RefStatus);
			dictionary.Add(Constants.RefSubjectInsurance, this.RefSubjectInsurance);

			return dictionary;
        }

		List<ReferenceUniversalItem> DeliveryCenterRef { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> DeliveryPointRef { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefExpertise { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefIncomingChannel { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefLocality { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefMedDocumentType { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefNomination { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefReason { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefStatementType { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefStatus { get; set; } = new List<ReferenceUniversalItem>();
		List<ReferenceUniversalItem> RefSubjectInsurance { get; set; } = new List<ReferenceUniversalItem>();

		//Dictionary<string, List<ReferenceUniversalItem>> Dictionary { get; set; }
	}
}