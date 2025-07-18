using MessangerClient.Controls;
using MessangerClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MessangerClient
{
    public class MessageSerializer
    {
        int prcount = 0;

        Type type;
        PropertyInfo[] properties = null;

        public enum ServiceType
        { 
          UpdateUserStatus= 0,
          DeleteChannel=1,
          Kicked=2,
          None=3,
          Stream=4
        }



        public MessageSerializer()
        {
            type = typeof(Message);
            properties = type.GetProperties();
            prcount = properties.Length + 1;
        }

        public void Serialize(Stream stream, Message msg)
        {
            string str = "<Message>\r\n";
            foreach (PropertyInfo p in properties)
            {
                if (p.Name != "ExtensionData")
                {
                    str += String.Format("<{0}>{1}</{0}>\r\n", p.Name, p.GetValue(msg).ToString());
                }
            }
            str += "</Message>\r\n";
            byte[] data = Encoding.UTF8.GetBytes(str);
            stream.Write(data, 0, data.Length);
        }

        public Message GetMessageById(string path, long id)
        {
            long position = prcount * id;
            string[] msgprop = File.ReadLines(path).Skip((int)position).Take(prcount).ToArray();

            string username = msgprop.Where(x => x.Contains("<Username>")).First();
            username = username.Substring(username.IndexOf('>') + 1, username.IndexOf('/') - 2 - username.IndexOf('>'));

            string address = msgprop.Where(x => x.Contains("<Address>")).First();
            address = address.Substring(address.IndexOf('>') + 1, address.IndexOf('/') - 2 - address.IndexOf('>'));

            string text = msgprop.Where(x => x.Contains("<Text>")).First();
            text = text.Substring(text.IndexOf('>') + 1, text.IndexOf('/') - 2 - text.IndexOf('>'));

            string time = msgprop.Where(x => x.Contains("<SendTime>")).First();
            time = time.Substring(time.IndexOf('>') + 1, time.IndexOf('/') - 2 - time.IndexOf('>'));

            string idread = msgprop.Where(x => x.Contains("<Id>")).First();
            int msgid = Convert.ToInt32(idread.Substring(idread.IndexOf('>') + 1, idread.IndexOf('/') - 2 - idread.IndexOf('>')));

            Message msg = new Message() {
              Address = address,
              Username = username,
              Text = text,
              SendTime = time,
              Id = msgid
            }; 
            return msg;
        }

        public static string GetTextValue(string textvalue)
        {
            string value = textvalue.Substring(textvalue.IndexOf(']') + 1);
            return value;
        }

        public static Control GetMessageControl(Message msg, Channel channel = null)
        {
            if (msg.Text.StartsWith("[text]"))
            {

                MessageControl mc = new MessageControl();
                if (msg.Username == App.CurrentUser.Username)
                {
                    mc.IsMine = true;
                    mc.Header = "You";
                }
                else
                {
                    mc.IsMine = false;
                    mc.Header = msg.Username;
                }
                mc.Text = MessageSerializer.GetTextValue(msg.Text);
                mc.SendTime = Convert.ToDateTime(msg.SendTime);
                return mc;
            }
            else if(msg.Text.StartsWith("[file"))
            {
                FileControl fc = new FileControl();
                if (msg.Username == App.CurrentUser.Username)
                {
                    fc.IsMine = true;
                    fc.Header = "You";
                }
                else
                {
                    fc.IsMine = false;
                    fc.Header = msg.Username;
                }
                fc.SendTime = Convert.ToDateTime(msg.SendTime);

                string attributes = msg.Text.Substring(msg.Text.IndexOf("[file "), msg.Text.IndexOf("]") - msg.Text.IndexOf("[file ")+1);
                string link = attributes.Substring(attributes.IndexOf("link=")+5, attributes.IndexOf(",") - attributes.IndexOf("link=")-5);
                string len = attributes.Substring(attributes.IndexOf("length=")+7, attributes.IndexOf("]") - attributes.IndexOf("length=")-7);
                long length = Int64.Parse(len);
                string filename = GetTextValue(msg.Text);
                string mode = "Chats";
                if (!msg.Address.Contains("_")) mode = "Groups";
                fc.FileLink = link;
                fc.FileLength = length;
                if (!fc.IsMine)
                {
                    filename = Path.GetFileName(filename);
                    fc.FileInfo = String.Format("ClientData/{0}/{1}/files/{2}", mode, msg.Address, filename);
                }
                else fc.FileInfo = filename;
                fc.Channel = channel;
                fc.InitializeType();
                return fc;
            }
            else if(msg.Text.StartsWith("[service type=ivitation") || msg.Text.StartsWith("[service type=chupdate"))
            {
                ServiceControl sc = new ServiceControl();
                sc.Text = GetTextValue(msg.Text).ToUpper();
                sc.SendTime = Convert.ToDateTime(msg.SendTime);
                return sc;
            }

            return null;
        }

        public static bool IsCorrectServiceMessage(string text)
        {
            if (text.StartsWith("[service type=user") || text.StartsWith("[service type=chdelete") || text.StartsWith("[service type=grkick") || text.StartsWith("[service type=stream"))
                return false;

            return true;
        }

        public static ServiceType GetServiceMessageType(string text)
        {
            if (text.StartsWith("[service type=user")) return ServiceType.UpdateUserStatus;
            if (text.StartsWith("[service type=chdelete")) return ServiceType.DeleteChannel;
            if (text.StartsWith("[service type=grkick")) return ServiceType.Kicked;
            if (text.StartsWith("[service type=stream")) return ServiceType.Stream;
            return ServiceType.None;
        }
    }
}
