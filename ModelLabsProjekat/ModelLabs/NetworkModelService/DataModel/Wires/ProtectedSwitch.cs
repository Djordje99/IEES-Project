using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ProtectedSwitch : Switch
    {
        private float breakingCapacity;

        public ProtectedSwitch(long globalID) : base(globalID) { }

        public float BreakingCapacity { get; set; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ProtectedSwitch x = (ProtectedSwitch)obj;
				return (x.breakingCapacity == this.breakingCapacity);
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
				case ModelCode.PROTECTEDSWITCH_BREAKINGCAP:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.PROTECTEDSWITCH_BREAKINGCAP:
					property.SetValue(breakingCapacity);
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
				case ModelCode.PROTECTEDSWITCH_BREAKINGCAP:
					breakingCapacity = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
