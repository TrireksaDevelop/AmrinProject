using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.ModelDTO
{
    [TableName("Inbox")]
    public class inbox:BaseNotify
    {
        private int id;
        [DbColumn("Id")]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id ,value); }
        }

        private string userId;
        [DbColumn("UserId")]
        public string UserId
        {
            get { return userId; }
            set { SetProperty(ref userId ,value); }
        }



        private string message;
        [DbColumn("pesan")]
        public string Message
        {
            get { return message; }
            set {SetProperty(ref message ,value); }
        }

        private DateTime dateTime;
        [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get { return dateTime; }
            set {SetProperty(ref dateTime ,value); }
        }

        private int permohonanId;
        [DbColumn("PermohonanId")]
        public int PermohonanId
        {
            get { return permohonanId; }
            set { permohonanId = value; }
        }



        private bool isRead;
        [DbColumn("IsRead")]
        public bool IsRead
        {
            get { return isRead; }
            set { SetProperty(ref isRead ,value); }
        }



        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName , value); }
        }


    }
}
