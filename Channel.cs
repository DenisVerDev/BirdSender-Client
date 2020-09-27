using MessangerClient.MSGService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MessangerClient
{
    public abstract class Channel
    {

        public string Address { get; set; }

        public List<string> Users { get; set; }

        public bool IsGroup { get; protected set; }

        public long Total { get; set; }

        public int UsersCount { get; set; }

        public List<Message> LastMessages { get; set; }

        public int NewMsgCount { get; set; }
        public int UnreadMessages { get; set; }

        public Channel()
        {
            this.LastMessages = new List<Message>();
            this.Users = new List<string>();
            this.UsersCount = 0;
            this.Total = 0;
        }

        public Channel(string[] users)
        {
            LastMessages = new List<Message>();
            this.Users = new List<string>();
            this.Users.AddRange(users);
            this.UsersCount = this.Users.Count;
            this.Total = 0;
        }

        public Channel(string address, bool IsGroup)
        {
            LastMessages = new List<Message>();
            this.Users = new List<string>();
            this.Address = address;
            this.IsGroup = IsGroup;
            ReadConfig();
        }

        public abstract void LoadMessages();

        public abstract void SaveMessagesInHistory();

        public abstract void SaveConfigurations();

        public abstract void ReadConfig();


        public async void SendMessage(Message msg)
        {
            ResultCodes result = ResultCodes.ServerError;
            await Task.Run(() => {
            try
            {
                msg.Id = Total;
                Total++;
                LastMessages.Add(msg);
                SaveMessagesInHistory();
                SaveConfigurations();
                FileWork(msg.Text);
                if (!App.IsConnecting) result = App.Client.SendMessage(msg);
            }
            catch(Exception ex)
            {
                    result = ResultCodes.ServerConnectionLost;
            }
            });
            if (result == ResultCodes.ServerConnectionLost) App.ConnectToServer();
        }

        public async void ReceiveMessage(Message msg)
        {
            await Task.Run(() =>
            {
                msg.Id = Total;
                Total++;
                LastMessages.Add(msg);
                SaveMessagesInHistory();
                SaveConfigurations();
                FileWork(msg.Text);
            });
        }


        protected abstract void FileWork(string text);

        public abstract void RemoveAllData();

    }
}
