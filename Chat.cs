using MessangerClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerClient
{
    public class Chat : Channel
    {

        public Chat(string[] users) : base(users)
        {
            Address = Users[0] + "_" + Users[1];
            this.IsGroup = false;
            Directory.CreateDirectory(String.Format("ClientData/Chats/{0}", Address));
            Directory.CreateDirectory(String.Format("ClientData/Chats/{0}/files", Address));
            SaveConfigurations();
        }

        public Chat(string address) : base(address, false)
        {

        }

        public override void SaveMessagesInHistory()
        {
            try
            {
                MessageSerializer ser = new MessageSerializer();
                FileStream fs = new FileStream(String.Format("ClientData/Chats/{0}/history.mxml", this.Address), FileMode.OpenOrCreate, FileAccess.Write);
                if (LastMessages.Count > 100) LastMessages.RemoveRange(0, LastMessages.Count - 50);
                foreach (Message msg in LastMessages)
                {
                    ser.Serialize(fs, msg);
                }
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public override void SaveConfigurations()
        {
            try
            {
                FileStream fs = new FileStream(String.Format("ClientData/Chats/{0}/info.txt", this.Address), FileMode.OpenOrCreate, FileAccess.Write);
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Total);
                    bw.Write(NewMsgCount);
                    bw.Write(UnreadMessages);
                    bw.Write(UsersCount);
                    foreach(string user in Users)
                    {
                        bw.Write(user);
                    }
                }
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public override void ReadConfig()
        {
            try
            {
                FileStream fs = new FileStream(String.Format("ClientData/Chats/{0}/info.txt", this.Address), FileMode.Open, FileAccess.Read);
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Total = br.ReadInt64();
                    NewMsgCount = br.ReadInt32();
                    UnreadMessages = br.ReadInt32();
                    UsersCount = br.ReadInt32();
                    for(int i = 0; i < UsersCount; i++)
                    {
                        Users.Add(br.ReadString());
                    }
                }

                NewMsgCount += UnreadMessages;
                LoadMessages();
            }
            catch (Exception ex)
            {

            }
        }


        protected override void FileWork(string text)
        {
            if (text.StartsWith("[file"))
            {
                string[] files = Directory.GetFiles(String.Format("ClientData/Chats/{0}/files/", Address));
                if (files.Length > 49)
                {
                    DateTime time = File.GetCreationTime(files[0]);
                    int index = 0;
                    for (int i = 0; i < files.Length - 1; i++)
                    {
                        DateTime tmp = File.GetCreationTime(files[i]);
                        if (time > tmp)
                        {
                            time = tmp;
                            index = i;
                        }
                    }
                    File.Delete(files[index]);
                }
            }
        }


        public override void LoadMessages()
        {
            try
            {
                MessageSerializer ser = new MessageSerializer();

                string path = String.Format("ClientData/Chats/{0}/history.mxml", this.Address);
                long pos = 0;
                if (Total > 50 && NewMsgCount == 0) pos = Total - 50;
                for (long i = pos; i < Total; i++)
                {
                    Message msg = ser.GetMessageById(path, i);
                    LastMessages.Add(msg);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public override void RemoveAllData()
        {
            //deleting files
            string[] files = Directory.GetFiles(String.Format("ClientData/Chats/{0}_{1}/files", Users[0], Users[1]));
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(String.Format("ClientData/Chats/{0}_{1}/files", Users[0], Users[1]));
            //deleting chat folder
            File.Delete(String.Format("ClientData/Chats/{0}_{1}/history.mxml", Users[0], Users[1]));
            File.Delete(String.Format("ClientData/Chats/{0}_{1}/info.txt", Users[0], Users[1]));
            Directory.Delete(String.Format("ClientData/Chats/{0}_{1}", Users[0], Users[1]));
        }
    }
}
