using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModelKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private List<long> regulationSchedule = new List<long>();

        public RegulatingControl(long globalID) : base(globalID) { }

        public bool Discrete { get => discrete; set => discrete = value; }
        public RegulatingControlModelKind Mode { get => mode; set => mode = value; }
        public PhaseCode MonitoredPhase { get => monitoredPhase; set => monitoredPhase = value; }
        public float TargetRange { get => targetRange; set =>targetRange = value; }
        public float TargetValue { get => targetValue; set => targetValue = value; }
        public List<long> RegulationSchedule { get => regulationSchedule; set => regulationSchedule = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return (x.discrete == this.discrete && x.mode == this.mode && x.monitoredPhase == this.monitoredPhase &&
                    x.targetRange == this.targetRange && x.targetValue == this.targetValue &&
                    CompareHelper.CompareLists(x.regulationSchedule, this.regulationSchedule));
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
				case ModelCode.REGCONTROL_DISCRETE:
				case ModelCode.REGCONTROL_MODE:
				case ModelCode.REGCONTROL_MONITOREDPHASE:
				case ModelCode.REGCONTROL_TARGETRANGE:
				case ModelCode.REGCONTROL_TARGETVALUE:
				case ModelCode.REGCONTROL_REGSCHEDULE:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.REGCONTROL_DISCRETE:
					prop.SetValue(discrete);
					break;

				case ModelCode.REGCONTROL_MODE:
					prop.SetValue((short)mode);
					break;

				case ModelCode.REGCONTROL_MONITOREDPHASE:
					prop.SetValue((short)monitoredPhase);
					break;

				case ModelCode.REGCONTROL_TARGETRANGE:
					prop.SetValue(targetRange);
					break;

				case ModelCode.REGCONTROL_TARGETVALUE:
					prop.SetValue(targetValue);
					break;

				case ModelCode.REGCONTROL_REGSCHEDULE:
					prop.SetValue(regulationSchedule);
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
				case ModelCode.REGCONTROL_DISCRETE:
					discrete = property.AsBool();
					break;

				case ModelCode.REGCONTROL_MODE:
					mode = (RegulatingControlModelKind)property.AsEnum();
					break;

				case ModelCode.REGCONTROL_MONITOREDPHASE:
					monitoredPhase = (PhaseCode)property.AsEnum();
					break;

				case ModelCode.REGCONTROL_TARGETRANGE:
					targetRange = property.AsFloat();
					break;

				case ModelCode.REGCONTROL_TARGETVALUE:
					targetValue = property.AsFloat();
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
				return regulationSchedule.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regulationSchedule != null && regulationSchedule.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.REGCONTROL_REGSCHEDULE] = regulationSchedule.GetRange(0, regulationSchedule.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.REGSCHEDULE_REGCONTROL:
					regulationSchedule.Add(globalId);
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
				case ModelCode.REGSCHEDULE_REGCONTROL:

					if (regulationSchedule.Contains(globalId))
					{
						regulationSchedule.Remove(globalId);
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
