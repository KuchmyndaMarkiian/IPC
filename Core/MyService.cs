using System;

namespace Core
{
    public class MyService:IService
    {
        //public Action<string> SendAction { get; set; }
        public Action<string> ListenAction { get; set; }

        public void Send(string a)
        {
            //SendAction?.Invoke(a);
            ListenAction?.Invoke(a);
        }

        /*public string Listen()
        {
            return ListenAction?.Invoke();
        }*/
    }
}
