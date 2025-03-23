using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessangerClient.ServiceReference;

namespace MessangerClient
{
    public class ClientResponse : IMSGServiceCallback
    {
        public delegate void ResponseDelegate(Message msg);
        public event ResponseDelegate NewMessageCameOut;

        public delegate void ResponseStreamDel(byte[] data, bool isend);
        public event ResponseStreamDel NewAudioCameOut;
        public event ResponseStreamDel NewImageCameOut;

        ResultCodes IMSGServiceCallback.HasConnectionToClient()
        {
            return ResultCodes.Success;
        }

        void IMSGServiceCallback.ReceiveMessage(Message msg)
        {
            if(NewMessageCameOut != null)
            {
                NewMessageCameOut(msg);
            }
        }

        public void ReceiveAudio(byte[] data)
        {
            if (NewAudioCameOut != null) NewAudioCameOut(data, false);
        }

        public void ReceiveImage(byte[] data, bool isend)
        {
            if (NewImageCameOut != null) NewImageCameOut(data, isend);
        }
    }
}
