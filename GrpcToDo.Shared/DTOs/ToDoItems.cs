using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GrpcToDo.Shared.DTOs
{
    [DataContract]
    public class ToDoItems
    {
        [DataMember(Order = 1)]
        public List<ToDoData> ToDoItemList { get; set; }
    }
}
