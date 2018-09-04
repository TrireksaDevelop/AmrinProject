using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
   public class inbox:PropertyChange
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }



        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private DateTime dateTime;
        public DateTime Tanggal
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }
        }

        private int permohonanId;
        public int PermohonanId
        {
            get { return permohonanId; }
            set { SetProperty(ref permohonanId ,value); }
        }



        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }



        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

    }
}
