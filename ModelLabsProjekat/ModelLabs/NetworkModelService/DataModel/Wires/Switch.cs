using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        private bool normalOpen;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;
        private List<long> switchSchedules = new List<long>();

        public Switch(long globalID) : base(globalID) { }

        public bool NormalOpen { get => normalOpen; set => normalOpen = value; }
        public float RatedCurrent { get => ratedCurrent; set => ratedCurrent = value; }
        public bool Retained { get => retained; set => retained = value; }
        public int SwitchOnCount { get => switchOnCount; set => switchOnCount = value; }
        public DateTime SwitchOnDate { get => switchOnDate; set => switchOnDate = value; }
        public List<long> SwitchSchedules { get => switchSchedules; set => switchSchedules = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return (x.normalOpen == this.normalOpen && x.ratedCurrent == this.ratedCurrent && x.retained == this.retained &&
                    x.switchOnCount == this.switchOnCount && x.switchOnDate == this.switchOnDate &&
                    CompareHelper.CompareLists(x.switchSchedules, this.switchSchedules, true));
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

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.SWITCH_NORMALOPEN:
				case ModelCode.SWITCH_RATEDCURRENT:
				case ModelCode.SWITCH_RETAINED:
				case ModelCode.SWITCH_SWITCHONCOUNT:
				case ModelCode.SWITCH_SWITCHONDATE:
				case ModelCode.SWITCH_SWITCHSCHEDULES:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.SWITCH_NORMALOPEN:
					prop.SetValue(normalOpen);
					break;

				case ModelCode.SWITCH_RATEDCURRENT:
					prop.SetValue(ratedCurrent);
					break;

				case ModelCode.SWITCH_RETAINED:
					prop.SetValue(retained);
					break;

				case ModelCode.SWITCH_SWITCHONCOUNT:
					prop.SetValue(switchOnCount);
					break;

				case ModelCode.SWITCH_SWITCHONDATE:
					prop.SetValue(switchOnDate);
					break;

				case ModelCode.SWITCH_SWITCHSCHEDULES:
					prop.SetValue(switchSchedules);
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
				case ModelCode.SWITCH_NORMALOPEN:
					normalOpen = property.AsBool();
					break;

				case ModelCode.SWITCH_RATEDCURRENT:
					ratedCurrent = property.AsFloat();
					break;

				case ModelCode.SWITCH_RETAINED:
					retained = property.AsBool();
					break;

				case ModelCode.SWITCH_SWITCHONCOUNT:
					switchOnCount = property.AsInt();
					break;

				case ModelCode.SWITCH_SWITCHONDATE:
					switchOnDate = property.AsDateTime();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return switchSchedules.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (switchSchedules != null && switchSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.SWITCH_SWITCHSCHEDULES] = switchSchedules.GetRange(0, switchSchedules.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SWITCHSCHEDULE_SWITCH:
					switchSchedules.Add(globalId);
					break;

				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SWITCHSCHEDULE_SWITCH:

					if (switchSchedules.Contains(globalId))
					{
						switchSchedules.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}

		#endregion IReference implementation	
	}
}
