using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultWriter : Page
{
    MistikaDBDataContext db = new MistikaDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateText();
        //SendMailsMails("","");
    }

    public void SendMails(string mailaddress, string password, List<string> listmails, bool clearAll)
    {
        try
        {
            MailMessage mail = new MailMessage();
            NetworkCredential cred = new NetworkCredential(mailaddress, password);
            mail.Subject = "";
            for (int i = 1; i < listmails.Count(); i++)
            {
                mail.To.Add(listmails[i]);
            }
            mail.From = new MailAddress(mailaddress);
            mail.IsBodyHtml = true;
            mail.Body = File.ReadAllText(HttpContext.Current.Server.MapPath("MiniMail.html"), Encoding.UTF8);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(mail);
            if (clearAll)
            {
                StreamWriter writer = new StreamWriter(@"C:\Users\Or\Desktop\Spider\TextFile.txt");
                writer.WriteLine("");
                writer.Close();
            }
            //Thread.Sleep(25000);
        }
        catch (Exception gh)
        {
            string cv = gh.Message;
        }
    }

    public void CreateText()
    {
        StreamWriter writer = new StreamWriter(@"C:\Users\Or\Desktop\Spider\TextFile.txt");
        var mails = from m in db.CVMails
                    where m.MailSent == null || m.MailSent == false
                    select m;

        StringBuilder builder = new StringBuilder();
        foreach (var s in mails)
        {
            builder.Append(s.MailValue + ",");
            s.MailSent = true;
            db.SubmitChanges();
        }
        writer.Write(builder.ToString());
        writer.Close();
    }

    public void SendMailsMails(string mail, string password)
    {
        StreamReader reader = new StreamReader(@"C:\Users\Or\Desktop\Spider\TextFile.txt");
        string ssdf = reader.ReadToEnd();
        reader.Close();
        string[] listmails = ssdf.Split(',');
        List<string> lista = new List<string>();
        string last = string.Empty;
        if (listmails.Count() > 500)
        {
            last = listmails[489];
            for (int i = 0; i < 490; i++)
            {
                lista.Add(listmails[i]);
            }
        }
        else
        {
            for (int i = 0; i < listmails.Count(); i++)
            {
                lista.Add(listmails[i]);
            }

        }
        if (!string.IsNullOrEmpty(last))
        {
            StreamWriter r = new StreamWriter(@"C:\Users\Or\Desktop\Spider\LastMail.txt");
            r.WriteLine(last);
            r.Close();
        }
        else
        {
            StreamWriter r = new StreamWriter(@"C:\Users\Or\Desktop\Spider\LastMail.txt");
            r.WriteLine("");
            r.Close();
        }

        SendMails(mail, password, lista, false);
    }
}