using System;
using System.Collections.Generic;
namespace Kztek.LPR
{
	public interface ILPRProcessing
	{
		event LPRCompletedEventHandler LPRCompleted;
		event ErrorEventHandler NewError;
		string LPREngineProductKey
		{
			get;
			set;
		}
		bool UsingGPU
		{
			get;
			set;
		}
		int CudaDeviceID
		{
			get;
			set;
		}
		int NumOfProcessor
		{
			get;
			set;
		}
		int NumOfProcessorPending
		{
			get;
		}
		bool IsBusy
		{
			get;
		}
		List<float> ContrastSensitivityFactor
		{
			set;
		}
		void CreateLPREngine();
		void CloseLPREngine();
		bool Analyze(ref LPRObject lprResult);
		void AnalyzeAsync(ref LPRObject lprResult);
	}
}
