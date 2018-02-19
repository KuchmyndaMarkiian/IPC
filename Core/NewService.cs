using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [ServiceContract]
    public interface INewService
    {
        Action SendAction { get; set; }
        Action<string> ListenAction { get; set; }

        [OperationContract]
        void Send(string a);

        [OperationContract]
        string Listen();
    }
    public class NewService : INewService
    {
        public static Uri BaseAddress = new Uri("net.pipe://localhost/MyPipe");
        public static string Address = "testPipe";


        public Action SendAction { get; set; }
        public Action<string> ListenAction { get; set; }
        public void Send(string a)
        {
           ListenAction?.Invoke(a);
        }

        public string Listen()
        {
            throw new NotImplementedException();
        }
    }
}
