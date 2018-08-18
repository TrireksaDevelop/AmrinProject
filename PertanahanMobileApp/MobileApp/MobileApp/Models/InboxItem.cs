using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
   public class InboxItem:PropertyChange
    {
        private string sender;

        public string Sender
        {
            get { return sender; }
            set { SetProperty(ref sender ,value); }
        }


        public DateTime RecieveDate
        {
            get { return recieve; }
            set { SetProperty(ref recieve, value); }
        }

        public DateTime SenderDate
        {
            get { return senderDate; }
            set { SetProperty(ref senderDate, value); }
        }


        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public bool Readed
        {
            get { return readed; }
            set { SetProperty(ref readed, value); }
        }
        public string SenderInfo {

            get { return _senderInfo; }
            set
            {
                SetProperty(ref _senderInfo, value);
            }
        }


        private DateTime recieve;
        private DateTime senderDate;
        private bool readed;
        private string _senderInfo;
        private string message;
    }
}
