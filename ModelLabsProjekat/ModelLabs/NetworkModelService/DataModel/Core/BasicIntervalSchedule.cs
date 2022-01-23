using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitSymbol value1Unit;

        public BasicIntervalSchedule(long globalID) : base(globalID) { }

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public UnitSymbol Value1Unit { get => value1Unit; set => value1Unit = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
				return (x.startTime == this.startTime && x.value1Unit == this.value1Unit);
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
				case ModelCode.BIS_STARTTIME:
				case ModelCode.BIS_VALUE1UNIT:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.BIS_STARTTIME:
					property.SetValue(startTime);
					break;

				case ModelCode.BIS_VALUE1UNIT:
					property.SetValue((short)value1Unit);
					break;

				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.BIS_STARTTIME:
					startTime = property.AsDateTime();
					break;

				case ModelCode.BIS_VALUE1UNIT:
					value1Unit = (UnitSymbol)property.AsEnum();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
