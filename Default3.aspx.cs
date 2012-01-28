using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Net.NetworkInformation;

public partial class Default3 : System.Web.UI.Page
{
    private MistikaDBDataContext mainDb = new MistikaDBDataContext();

    /// <summary> /// Retrives
    //the position of the url from a search /// on
    public static int GetPosition(Uri url, string searchTerm)
    {
        string raw = "http://www.google.com/search?num=39&q={0}&btnG=Search";
        string search = string.Format(raw, HttpUtility.UrlEncode(searchTerm));
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(search);
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
            {
                string html = reader.ReadToEnd();
                return FindPosition(html, url);
            }
        }
    } /// <summary> /// Examins
    private static int FindPosition(string html, Uri url)
    {
        string lookup = "(href=\")(\\w+[a-zA-Z0-9.-?=/]*)";
        MatchCollection matches = Regex.Matches(html, lookup);
        for (int i = 0; i < matches.Count; i++)
        {
            string match = matches[i].Groups[2].Value;
            if (match.Contains(url.Host))
                return i + 1;
        } return 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string[] lista = new string[] 
        { 
                        "פופולריות עולה – פיננסים","הבנק הבינלאומי הראשון","בנק מזרחי טפחות","הראל חברה לביטוח","פועלים באינטרנט","בנק לאומי סניפים",
            "אמריקן אקספרס","שער הדולר","לאומי קארד","ביזפורטל","בנק אגוד","פופולריות עולה – קמעונאות","סופר פארם","שופרסל",
            "צומת ספרים","דיוטיפרי","הום סנטר","טויז אר אס","מחסני חשמל","סטימצקי","מקדונלדס","למטייל",
            "פופולריות עולה","מיקמק","עספור","עספור צפייה ישירה","פייסבוק בעברית","ארץ נהדרת","בועות בצרורות 2","h&m ישראל","בהצדעה",
            "החיפושים הפופולאריים","פייסבוק","וואלה","יוטיוב","יד 2","ynet","אגד","מקושרים","מורפיקס","רכבת ישראל","פופולריות עולה – אנשים",
            "מיכל אמדורסקי","גיא פינס","דוד אהרון","אייל גולן","קרין גורן","משה פרץ","קובי פרץ","פופולריות עולה – תוכניות טלוויזיה","מחוברים",
            "סופר נני","סרוגים עונה 2","הבורר עונה 3","היפה והחנון","ארץ נהדרת","רוקדים עם כוכבים","מאסטר שף","כוכב נולד","האח הגדול 2",
            "מיסטיקה רוחנית", "מחפשת קוראת בקלפים", "מחפש נומרולוגית", "מחפשת נומרולוגית", "מחפשת מסיבת רווקות",
            "מחפשת קוראת בקפה", "מחפשת אסטרולוגית", "מיסטיקנית למסיבת רווקות", "אסטרולוגית למסיבת רווקות",
            "מחפשת קוראת בקלפים", "קריאה בכף יד", "דרושה קוראת בקפה", "מחפשת מכשפה",
            "מחפשת קוראת בקפה למסיבת רווקות", "מחפש קוראת בקפה", "מחפשת אסטרולוגית",
            "צור קשר במייל", "דבר איתי במייל", "הנה הכתובת מייל שלי", "ניתן ליצור איתי קשר בדואל",
            "צור קשר בכתובת דואל", "יצירת קשר במייל", "יצירת קשר בדואר אלקטרוני",
            "כתובת הדואר האלקטרוני שלי היא", "המייל שלי הוא", "כתובת המייל שלי היא", "כתובת המייל שלי הוא",
            "המלצה על מיסטיקנית", "המלצה על קוראת בקלפים", "המלצה על קוראת בקפה",
            "אייל גולן","סלינה גומז","דודו אהרון","קרין גורן","יובל המבולבל","משה פרץ",
            "אי מייל", "סקס", "מכוניות", "מכבי חיפה", "גוגל", "יונית לוי", "אמא", "אבא", "עיריית תל אביב",
            "המייל שלי הוא", "תרשום את המייל שלי", "תרשמי את המייל שלי",
            "מה המייל שלך?", "כתובת הדואל שלי הוא", "כתובת הדואל שלי:", "המייל שלי:",
            "האח הגדול", "גלעד שליט", "האח הגדול","סוריה","איראן","לוב","אייפון 5","שביתה",
            "הפועל תל אביב","רכבת","בנק הפועלים","בר רפאלי","פרחים","ג'סטין ביבר",
            "פורים","פסח","לבבות","דגל ישראל","דרדסים","רמזור עונה 3","מיקמק","קיקה"
            ,"כוכב נולד 9","עם סגולה","החברים של נאור","עומר אדם","כוכב נולד",
            "מחפשת קוראת בכף יד", "ערבי רווקות", "מיסטיקה", "דרושה נומרולוגית", "מחפשת מיסטיקנית",
        };

        foreach (string m in lista)
        {
            GetPosition(m);
        }
    }

    public void Search(string[] lista)
    {
        var url_template = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&rsz=large&safe=active&q={0}&start={1}";
        Uri search_url;
        var results_list = new List<SearchResult>();


        for (int i = 0; i < lista.Count(); i++)
        {

            search_url = new Uri(string.Format(url_template, lista[i], 0));
            var page = new WebClient().DownloadString(search_url);
            JObject o = (JObject)JsonConvert.DeserializeObject(page);
            var t = o["responseData"]["results"].Children();

            search_url = new Uri(string.Format(url_template, lista[i], 8));
            var page1 = new WebClient().DownloadString(search_url);
            JObject o2 = (JObject)JsonConvert.DeserializeObject(page);
            var t2 = o2["responseData"]["results"].Children();

            search_url = new Uri(string.Format(url_template, lista[i], 16));
            var page2 = new WebClient().DownloadString(search_url);
            JObject o3 = (JObject)JsonConvert.DeserializeObject(page);
            var t3 = o3["responseData"]["results"].Children();

            foreach (JObject g in t)
            {
                string html = string.Empty;
                try
                {
                    html = getPageSource(g["url"].ToString());
                }
                catch (Exception gfd)
                {
                    string fdged = gfd.Message;
                }
                if (!string.IsNullOrEmpty(html))
                {
                    FindPositionUrl(html);
                }
            }
            Thread.Sleep(25000);
        }
    }

    public class SearchResult
    {
        public string url;
        public string title;
        public string content;
        public FindingEngine engine;
        public enum FindingEngine { google, bing, google_and_bing };

        public SearchResult(string url, string title, string content, FindingEngine engine)
        {
            this.url = url;
            this.title = title;
            this.content = content;
            this.engine = engine;
        }
    }

    public string getPageSource(string URL)
    {
        WebClient webClient = new WebClient();
        string strSource = webClient.DownloadString(URL);
        webClient.Dispose();
        return strSource;
    }

    public void GetPosition(string searchTerm)
    {
        for (int i = 10; i < 100; i += 10)
        {
            try
            {
                lock (this)
                {
                    string raw = "https://www.google.co.il/search?q={0}&hl=iw&rls=com.microsoft:en-US:%7Breferrer:source%7D&prmd=imvns&ei=mW6FT96_EoHN0QXn_6XKB&start={1}&sa=N&biw=1440&bih=775";
                    string search = string.Format(raw, searchTerm, i);
                    string m = getPageSource(search);
                    FindPosition(m);
                    Thread.Sleep(25000);
                }

            }
            catch (Exception b)
            {
                string m = b.Message;
                if (m.Contains("503"))
                {
                    break;
                }
                continue;
            }
        }
    }

    public void FindPosition(string html)
    {
        //string lookup = "(href=\")(\\w+[a-zA-Z0-9.-?=/]*)";
        string lookup = @"((https?|http|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)";
        MatchCollection matches = Regex.Matches(html, lookup);
        for (int i = 0; i < matches.Count; i++)
        {
            string match = matches[i].Value;

            if (match.IndexOf("google") == -1)
            {
                if (match.IndexOf('+') == -1)
                {
                    continue;
                }
                match = match.Substring(0, match.IndexOf('+'));

                try
                {
                    string g = getPageSource(match);
                    Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    foreach (Match m in t.Matches(g))
                    {
                        string val = m.Value;
                        if (val.Contains("ic=/upload/infocenter/info_images/") ||
                            val.Contains(".gif") || val.Contains(".jpg"))
                        {
                            continue;
                        }

                        if (val.Contains("wanted_result.aspx?"))
                        {
                            val = val.Replace("wanted_result.aspx?", "");
                        }

                        if (val.Contains("email="))
                        {
                            val = val.Replace("email=", "");
                        }

                        if (val.Contains("//"))
                        {
                            val = val.Replace("//", "");
                        }

                        if (val.Contains("%20"))
                        {
                            val = val.Replace("%20", "");
                        }

                        CVMail f = mainDb.CVMails.SingleOrDefault(b => b.MailValue == val);
                        if (f == null)
                        {
                            if (mainDb.CVMails.Count() > 0)
                            {
                                long x = mainDb.CVMails.OrderByDescending(y => y.MailID).First().MailID;
                                mainDb.CVMails.InsertOnSubmit(new CVMail
                                {
                                    MailID = x + 1,
                                    MailValue = val,
                                    MailSent = false,
                                    MailDate = DateTime.Now
                                });
                            }
                            else
                            {
                                mainDb.CVMails.InsertOnSubmit(new CVMail
                                {
                                    MailID = 1,
                                    MailValue = val,
                                    MailSent = false,
                                    MailDate = DateTime.Now
                                });
                            }
                            mainDb.SubmitChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }

    public void FindPositionUrl(string g)
    {
        //    for (int i = 0; i < 64; i += 8)
        //    {
        //        //var request = (HttpWebRequest)WebRequest.Create("https://ajax.googleapis.com/ajax/services/search/web?v=1.0&key=b41c19d0015b4418f64c40c97a1f86bae921ee2a&userip=87.68.25.208&q=hello");
        //        ////http://ajax.googleapis.com/ajax/services/search/web?v=1.0&key=831823424278.apps.googleusercontent.com&q=hello"
        //        ////var request = "https://accounts.google.com/o/oauth2/auth?scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.email+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.profile&state=%2Fprofile&redirect_uri=https%3A%2F%2Foauth2-login-demo.appspot.com%2Foauthcallback&response_type=code&client_id=831823424278.apps.googleusercontent.com";
        //        var request = (HttpWebRequest)WebRequest.Create("http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=" + m + "&start=" + i);
        //        var response = (HttpWebResponse)request.GetResponse();
        //        var responseText = (new StreamReader(response.GetResponseStream())).ReadToEnd();
        //        if (responseText.Contains("Suspected Terms of Service Abuse"))
        //        {
        //        }
        //        else
        //        {
        //            Regex r = new Regex("unescapedUrl");
        //            MatchCollection col = r.Matches(m);
        //            foreach (Match dfd in col)
        //            {
        //                string d = dfd.Groups["UNESCAPEDURL"].Value;
        //                FindPositionUrl(d);
        //            }
        //            Thread.Sleep(25000);
        //        }
        //    }

        Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        foreach (Match m in t.Matches(g))
        {
            string val = m.Value;
            if (val.Contains("ic=/upload/infocenter/info_images/") ||
                val.Contains(".gif") || val.Contains(".jpg"))
            {
                continue;
            }

            if (val.Contains("wanted_result.aspx?"))
            {
                val = val.Replace("wanted_result.aspx?", "");
            }

            if (val.Contains("email="))
            {
                val = val.Replace("email=", "");
            }

            if (val.Contains("//"))
            {
                val = val.Replace("//", "");
            }

            if (val.Contains("%20"))
            {
                val = val.Replace("%20", "");
            }

            CVMail f = mainDb.CVMails.SingleOrDefault(b => b.MailValue == val);
            if (f == null)
            {
                if (mainDb.CVMails.Count() > 0)
                {
                    long x = mainDb.CVMails.OrderByDescending(y => y.MailID).First().MailID;
                    mainDb.CVMails.InsertOnSubmit(new CVMail
                    {
                        MailID = x + 1,
                        MailValue = val,
                        MailSent = false,
                        MailDate = DateTime.Now
                    });
                }
                else
                {
                    mainDb.CVMails.InsertOnSubmit(new CVMail
                    {
                        MailID = 1,
                        MailValue = val,
                        MailSent = false,
                        MailDate = DateTime.Now
                    });
                }
                mainDb.SubmitChanges();
            }
        }
    }
}