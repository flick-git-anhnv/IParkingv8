using System;
using System.Collections.Generic;
using System.Text;

namespace Kztek.Api
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public int ApiStatusCode { get; set; }
        public int ResponseStatusCode { get; set; }
    }
}
