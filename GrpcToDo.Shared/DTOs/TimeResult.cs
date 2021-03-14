using System.Runtime.Serialization;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class TimeResult
    {
        [DataMember(Order = 1)]
        public string Time { get; set; }
    }
}
