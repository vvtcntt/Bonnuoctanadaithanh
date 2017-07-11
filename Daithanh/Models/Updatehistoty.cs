using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Daithanh.Models;
namespace Daithanh.Models
{
    public class Updatehistoty
    {
        public static void UpdateHistory(string task,string FullName,string UserID)
        {
            DaithanhContext db = new DaithanhContext();
             tblHistoryLogin tblhistorylogin = new tblHistoryLogin();
            tblhistorylogin.FullName = FullName;
            tblhistorylogin.Task = task;
            tblhistorylogin.idUser = int.Parse(UserID);
            tblhistorylogin.DateCreate = DateTime.Now;
            tblhistorylogin.Active = true;
            
            db.tblHistoryLogins.Add(tblhistorylogin);
            db.SaveChanges();
           
        }
    }
}