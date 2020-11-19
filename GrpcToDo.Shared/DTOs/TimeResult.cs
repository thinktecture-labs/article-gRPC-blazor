using System;
using System.Runtime.Serialization;
using System.Text;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class TimeResult
    {
        [DataMember(Order = 1)]
        public string Time { get; set; }
    }
}
