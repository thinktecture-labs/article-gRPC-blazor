using System.Runtime.Serialization;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class ToDoQuery
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
