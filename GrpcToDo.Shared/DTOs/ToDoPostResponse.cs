using System.Runtime.Serialization;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class ToDoPostResponse
    {
        [DataMember(Order = 1)]
        public string StatusMessage { get; set; }

        [DataMember(Order = 2)]
        public bool Status { get; set; }

        [DataMember(Order = 3)]
        public int StatusCode { get; set; }
    }
}
