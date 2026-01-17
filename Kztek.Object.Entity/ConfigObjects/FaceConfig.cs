using System;
using System.Collections.Generic;
using System.Text;
using static Kztek.Object.LprDetecter;

namespace Kztek.Object.Entity.ConfigObjects
{
    public class FaceConfig
    {
        public EmFaceDetecter Type { get; set; } = EmFaceDetecter.KztekFace;
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public enum EmFaceDetecter
    {
        KztekFace
    }
}
