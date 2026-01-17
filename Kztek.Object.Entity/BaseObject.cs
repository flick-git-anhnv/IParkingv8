using System;

namespace Kztek.Object
{
    public class BaseObject
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public object Description { get; set; } = string.Empty;
        public string CreatedUtc { get; set; } = string.Empty;
        public string UpdatedUtc { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public bool Deleted { get; set; } = false;

        public DateTime? CreatedTime
        {
            get
            {
                try
                {
                    if (CreatedUtc.Contains("T"))
                    {
                        return DateTime.ParseExact(CreatedUtc.Substring(0, "yyyy-MM-ddTHH:mm:ss".Length), "yyyy-MM-ddTHH:mm:ss", null).AddHours(7);
                    }
                    else
                    {
                        return DateTime.Parse(CreatedUtc).AddHours(7);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
        public DateTime? UpdatedTime
        {
            get
            {
                try
                {
                    if (UpdatedUtc.Contains("T"))
                    {
                        return DateTime.ParseExact(UpdatedUtc.Substring(0, "yyyy-MM-ddTHH:mm:ss".Length), "yyyy-MM-ddTHH:mm:ss", null).AddHours(7);
                    }
                    else
                    {
                        return DateTime.Parse(UpdatedUtc).AddHours(7);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
