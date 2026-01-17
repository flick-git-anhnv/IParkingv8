using System;
using System.Collections.Generic;
using System.Text;

namespace Kztek.Scale
{
	public class ScaleEventArgs : EventArgs
	{
		private int gross;
		private int tare;
		private bool isMinusValue;
		private int decimalValue;
		private bool isStable = true;
		public int Gross
		{
			get
			{
				return this.gross;
			}
			set
			{
				this.gross = value;
			}
		}
		public int Tare
		{
			get
			{
				return this.tare;
			}
			set
			{
				this.tare = value;
			}
		}
		public bool IsMinusValue
		{
			get
			{
				return this.isMinusValue;
			}
			set
			{
				this.isMinusValue = value;
			}
		}
		public int DecimalValue
		{
			get
			{
				return this.decimalValue;
			}
			set
			{
				this.decimalValue = value;
			}
		}
		public bool IsStable
		{
			get { return isStable; }
			set { isStable = value; }
		}

	}
}
