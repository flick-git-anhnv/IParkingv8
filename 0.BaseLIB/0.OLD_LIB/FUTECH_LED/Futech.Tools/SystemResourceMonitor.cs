using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Futech.Tools
{
    public class SystemResourceMonitor
    {
        // Thong tin he thong CPU ?%, RAM ?MB
        private bool isRunning = false;
        protected PerformanceCounter cpuCounter;
        protected PerformanceCounter ramCounter;

        public void StartMonitoring()
        {
            try
            {
                cpuCounter = new PerformanceCounter();

                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";

                ramCounter = new PerformanceCounter("Memory", "Available MBytes");

                this.isRunning = true;
            }
            catch
            {
                this.isRunning = false;
            }
        }

        public void StopMonitoring()
        {
            this.isRunning = false;
            try
            {
                if (this.cpuCounter != null)
                {
                    this.cpuCounter.Dispose();
                }
                if (this.ramCounter != null)
                {
                    this.ramCounter.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public int CPU_Usage
        {
            get
            {
                if (this.cpuCounter != null)
                {
                    try
                    {
                        return (int)Math.Ceiling((double)this.cpuCounter.NextValue());
                    }
                    catch //(Exception ex)
                    {
                        
                    }
                }
                return 0;
            }
        }

        public int RAM_Available
        {
            get
            {
                if (this.ramCounter != null)
                {
                    try
                    {
                        return (int)Math.Ceiling((double)this.ramCounter.NextValue());
                    }
                    catch// (Exception ex)
                    {
                    }
                }
                return 0;
            }
        }

        public string getCurrentCpuUsage()
        {
            return cpuCounter.NextValue() + "%";
        }

        public string getAvailableRAM()
        {
            return ramCounter.NextValue() + "MB";
        } 

    }
}
