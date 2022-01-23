using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulationSchedule : SeasonDayTypeSchedule
    {
        private long regControl;

        public RegulationSchedule(long globalID) : base(globalID) { }

        public long RegControl { get => regControl; set => regControl = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				RegulationSchedule x = (RegulationSchedule)obj;
				return (x.regControl == this.regControl);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.REGSCHEDULE_REGCONTROL:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.REGSCHEDULE_REGCONTROL:
					prop.SetValue(regControl);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.REGSCHEDULE_REGCONTROL:
					regControl = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regControl != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.REGSCHEDULE_REGCONTROL] = new List<long>();
				references[ModelCode.REGSCHEDULE_REGCONTROL].Add(regControl);
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation		
	}
}
