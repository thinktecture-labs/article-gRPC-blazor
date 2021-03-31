using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GrpcToDo.Web
{
    public partial class App
    {
        [JSInvokable("NotifyError")]
        public static void NotifyError(string message)
        {
            Console.WriteLine($"Error occured from JavaScript Code: {message}");
        }
    }
}