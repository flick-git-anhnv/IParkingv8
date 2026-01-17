using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Futech.LPR
{
    [DataContract]
    public class Box
    {

        [DataMember(Name = "xmin")]
        public int Xmin { get; set; }

        [DataMember(Name = "ymin")]
        public int Ymin { get; set; }

        [DataMember(Name = "ymax")]
        public int Ymax { get; set; }

        [DataMember(Name = "xmax")]
        public int Xmax { get; set; }
    }

    [DataContract]
    public class Result
    {

        [DataMember(Name = "box")]  //the bounding Box of Plate
        public Box Box { get; set; }

        [DataMember(Name = "plate")] //the plate Number
        public string Plate { get; set; }

        [DataMember(Name = "score")] //Confidence level for reading the license plate text.
        public double Score { get; set; }

        [DataMember(Name = "dscore")] //Confidence level for plate detection.
        public double Dscore { get; set; }
    }

    [DataContract]
    public class PlateReaderResult
    {

        [DataMember(Name = "processing_time")]
        public double ProcessingTime { get; set; }

        [DataMember(Name = "version")]
        public int Version { get; set; }

        [DataMember(Name = "results")]
        public IList<Result> Results { get; set; }

        [DataMember(Name = "filename")]
        public string Filename { get; set; }

        [DataMember(Name = "usage")]
        public Usage usage { get; set; }
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }

    [DataContract]
    public class Usage
    {
        [DataMember(Name = "max_calls")]
        public int Max_calls { get; set; }

        [DataMember(Name = "calls")]
        public int Calls { get; set; }
    }

}
