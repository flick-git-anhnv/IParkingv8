using System;
namespace Kztek.LPR
{
	public class ErrorEventArgs : EventArgs
	{
		private string errorString = "";
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
			set
			{
				this.errorString = value;
			}
		}
		public ErrorEventArgs()
		{
		}
		public ErrorEventArgs(string errorString)
		{
			this.errorString = errorString;
		}
	}
}
