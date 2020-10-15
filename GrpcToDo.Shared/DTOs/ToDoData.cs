using System;
using System.Runtime.Serialization;
using System.Text;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class ToDoData
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string Title { get; set; }
        [DataMember(Order = 3)]
        public string Description { get; set; }
        [DataMember(Order = 4)]
        public bool Status { get; set; }
    }
}
