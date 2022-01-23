using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}

	public enum RegulatingControlModelKind : short
    {
		ActivePower = 1,
		Admittance = 2,
		CurrentFlow = 3,
		Fixed = 4,
		PowerFactor = 5,
		ReactivePower = 6,
		Temperature = 7,
		TimeScheduled = 8,
		Voltege = 9,
		Unknown = 10
	}

	public enum UnitSymbol : short
    {
		A = 1,
		F = 2,
		H = 3,
		Hz = 4,
		J = 5,
		N = 6,
		Pa = 7,
		S = 8,
		V = 9,
		VA = 10,
		VAh = 11,
		VAr = 12,
		VArh = 13,
		W = 14,
		Wh = 15,
		deg = 16,
		degC = 17,
		g = 18,
		h = 19,
		m = 20,
		m2 = 21,
		m3 = 22,
		min = 23,
		none = 24,
		ohm = 25,
		rad = 26,
		s = 27,
		Unknown = 28
	}		
}
