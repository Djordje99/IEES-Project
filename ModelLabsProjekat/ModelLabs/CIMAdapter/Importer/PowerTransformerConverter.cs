namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulateSeasonProperties(FTN.Season cimSeason, ResourceDescription rd)
        {
			if ((cimSeason != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);

                if (cimSeason.EndDateHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SEASON_ENDDATE, cimSeason.EndDate));
                }
                if (cimSeason.StartDateHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SEASON_STARTDATE, cimSeason.StartDate));
                }
			}
		}

		public static void PopulateDayTypeProperties(FTN.DayType cimDayType, ResourceDescription rd)
		{
			if ((cimDayType != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
			}
		}

		public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegularTimePoint != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

				if (cimRegularTimePoint.SequenceNumberHasValue)
                {
					rd.AddProperty(new Property(ModelCode.RTP_SEQNUM, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
					rd.AddProperty(new Property(ModelCode.RTP_VALUE1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
					rd.AddProperty(new Property(ModelCode.RTP_VALUE2, cimRegularTimePoint.Value2));
                }
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
					long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
						report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.RTP_INTERVALSCHEDULE, gid));
				}
			}
		}

		public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
					rd.AddProperty(new Property(ModelCode.BIS_STARTTIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
					rd.AddProperty(new Property(ModelCode.BIS_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
			}
		}

		public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
		{
			if ((cimRegularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);
			}
		}

		public static void PopulateSeasonDayTypeScheduleProperties(FTN.SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeasonDayTypeSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd);

                if (cimSeasonDayTypeSchedule.DayTypeHasValue)
                {
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
						report.Report.Append("\" - Failed to set reference to DayType: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SDTS_DAYTYPE, gid));
				}
				if (cimSeasonDayTypeSchedule.SeasonHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
						report.Report.Append("\" - Failed to set reference to Season: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SDTS_SEASON, gid));
				}
			}
		}

		public static void PopulateSwitchScheduleProperties(FTN.SwitchSchedule cimSwitchSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSwitchSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimSwitchSchedule, rd, importHelper, report);

                if (cimSwitchSchedule.SwitchHasValue)
                {
					long gid = importHelper.GetMappedGID(cimSwitchSchedule.Switch.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSwitchSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchSchedule.ID);
						report.Report.Append("\" - Failed to set reference to Switch: rdfID \"").Append(cimSwitchSchedule.Switch.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SWITCHSCHEDULE_SWITCH, gid));
				}
			}
		}

		public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimRegulationSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegulationSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimRegulationSchedule, rd, importHelper, report);

                if (cimRegulationSchedule.RegulatingControlHasValue)
                {
					long gid = importHelper.GetMappedGID(cimRegulationSchedule.RegulatingControl.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegulationSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationSchedule.ID);
						report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulationSchedule.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGSCHEDULE_REGCONTROL, gid));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
		{
			if ((cimSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);

                if (cimSwitch.NormalOpenHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
                }
                if (cimSwitch.RatedCurrentHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SWITCH_RATEDCURRENT, cimSwitch.RatedCurrent));
                }
                if (cimSwitch.RetainedHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));
                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
                }
			}
		}

		public static void PopulateProtectedSwitchProperties(FTN.ProtectedSwitch cimProtectedSwitch, ResourceDescription rd)
		{
			if ((cimProtectedSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSwitchProperties(cimProtectedSwitch, rd);

                if (cimProtectedSwitch.BreakingCapacityHasValue)
                {
					rd.AddProperty(new Property(ModelCode.PROTECTEDSWITCH_BREAKINGCAP, cimProtectedSwitch.BreakingCapacity));
                }
			}
		}

		public static void PopulateBreakerProperties(FTN.Breaker cimBreaker, ResourceDescription rd)
		{
			if ((cimBreaker != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateProtectedSwitchProperties(cimBreaker, rd);

				if (cimBreaker.InTransitTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BREAKER_INTRANSISTTIME, cimBreaker.InTransitTime));
				}
			}
		}

		public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd)
		{
			if ((cimRegulatingControl != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegulatingControl, rd);

				if (cimRegulatingControl.DiscreteHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
				}
                if (cimRegulatingControl.ModeHasValue)
                {
					rd.AddProperty(new Property(ModelCode.REGCONTROL_MODE, (short)GetDMSRegulatingControlModelKind(cimRegulatingControl.Mode)));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
					rd.AddProperty(new Property(ModelCode.REGCONTROL_MONITOREDPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
					rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGETRANGE, cimRegulatingControl.TargetRange));
                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
					rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGETVALUE, cimRegulatingControl.TargetValue));
                }
			}
		}
		#endregion Populate ResourceDescription

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}
		
		public static RegulatingControlModelKind GetDMSRegulatingControlModelKind(FTN.RegulatingControlModeKind mode)
        {
            switch (mode)
            {
                case FTN.RegulatingControlModeKind.activePower:
					return RegulatingControlModelKind.ActivePower;
                case FTN.RegulatingControlModeKind.admittance:
					return RegulatingControlModelKind.Admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
					return RegulatingControlModelKind.CurrentFlow;
                case FTN.RegulatingControlModeKind.@fixed:
					return RegulatingControlModelKind.Fixed;
                case FTN.RegulatingControlModeKind.powerFactor:
					return RegulatingControlModelKind.PowerFactor;
                case FTN.RegulatingControlModeKind.reactivePower:
					return RegulatingControlModelKind.ReactivePower;
                case FTN.RegulatingControlModeKind.temperature:
					return RegulatingControlModelKind.Temperature;
                case FTN.RegulatingControlModeKind.timeScheduled:
					return RegulatingControlModelKind.TimeScheduled;
                case FTN.RegulatingControlModeKind.voltage:
					return RegulatingControlModelKind.Voltege;
                default:
					return RegulatingControlModelKind.Unknown;
            }
        }

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
					return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
					return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
					return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
					return UnitSymbol.F;
                case FTN.UnitSymbol.g:
					return UnitSymbol.g;
                case FTN.UnitSymbol.h:
					return UnitSymbol.h;
                case FTN.UnitSymbol.H:
					return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
					return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
					return UnitSymbol.J;
                case FTN.UnitSymbol.m:
					return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
					return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
					return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
					return UnitSymbol.min;
                case FTN.UnitSymbol.N:
					return UnitSymbol.N;
                case FTN.UnitSymbol.none:
					return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
					return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
					return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
					return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
					return UnitSymbol.s;
                case FTN.UnitSymbol.S:
					return UnitSymbol.S;
                case FTN.UnitSymbol.V:
					return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
					return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
					return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
					return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
					return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
					return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
					return UnitSymbol.Wh;
				default:
					return UnitSymbol.Unknown;
            }
            #endregion Enums convert
        }
    }
}