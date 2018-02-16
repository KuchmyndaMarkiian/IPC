using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [ServiceContract]
    public interface IService
    {
        //Action<string> SendAction { get; set; }
        Action<string> ListenAction { get; set; }

        [OperationContract]
        void Send(string a);

        //[OperationContract]
        //string Listen();
    }
}
