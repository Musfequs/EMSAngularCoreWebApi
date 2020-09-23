using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AngularCoreApi.Model
{
    public class EmployeeDetails
    {
        [JsonProperty("empId")]
        public string EmpId { get; set; }

        [JsonProperty("empName")]
        public string EmpName { get; set; }

        [JsonProperty("empAddress")]
        public string EmpAddress { get; set; }
    }
}
