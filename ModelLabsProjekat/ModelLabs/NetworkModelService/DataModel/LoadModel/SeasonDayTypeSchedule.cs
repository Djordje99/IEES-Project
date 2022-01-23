using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class SeasonDayTypeSchedule : RegularIntervalSchedule
    {
        private long dayType;
        private long season;

        public SeasonDayTypeSchedule(long globalID) : base(globalID) { }

        public long DayType { get => dayType; set => dayType = value; }
        public long Season { get => season; set => season = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SeasonDayTypeSchedule x = (SeasonDayTypeSchedule)obj;
				return (x.dayType == this.dayType && x.season == this.season);
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
				case ModelCode.SDTS_DAYTYPE:
				case ModelCode.SDTS_SEASON:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.SDTS_DAYTYPE:
					prop.SetValue(dayType);
					break;

				case ModelCode.SDTS_SEASON:
					prop.SetValue(season);
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
				case ModelCode.SDTS_DAYTYPE:
					dayType = property.AsReference();
					break;

				case ModelCode.SDTS_SEASON:
					season = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (dayType != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.SDTS_DAYTYPE] = new List<long>();
				references[ModelCode.SDTS_DAYTYPE].Add(dayType);
			}

			if (season != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.SDTS_SEASON] = new List<long>();
				references[ModelCode.SDTS_SEASON].Add(season);
			}

			base.GetReferences(references, refType);
		}
	}
}
