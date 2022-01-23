using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Breaker : ProtectedSwitch
    {
        private float inTransitTime;

        public Breaker(long globalID) : base(globalID) { }

        public float InTransitTime { get => InTransitTime; set => inTransitTime = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Breaker x = (Breaker)obj;
				return (x.inTransitTime == this.inTransitTime);
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
				case ModelCode.BREAKER_INTRANSISTTIME:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.BREAKER_INTRANSISTTIME:
					property.SetValue(inTransitTime);
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
				case ModelCode.BREAKER_INTRANSISTTIME:
					inTransitTime = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
