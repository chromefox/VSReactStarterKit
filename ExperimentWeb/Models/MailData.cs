using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Threading;
using Newtonsoft.Json;

namespace ExperimentWeb.Models
{
    public class MailData
    {
        public static int IdCounter = 1;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

        public MailData(string folderName)
        {
            Folder = folderName;
            Id = Interlocked.Increment(ref IdCounter).ToString();
            From = $"From {Id}";
            To = $"To {Id}";
            Date = $"Jan - {Id}";
            Subject = $"Subject {Id}";
        }

        /// <summary>
        /// Divisible by 4.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<MailData> GenerateMails(int count)
        {
            var list = new List<MailData>();
            for (var i = 0; i < count/4; i++)
            {
                list.Add(new MailData("Inbox"));
                list.Add(new MailData("Sent"));
                list.Add(new MailData("Archive"));
                list.Add(new MailData("Spam"));
            }
            return list;
        }
    }

    public class MailResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("mails")]
        public List<MailData> Mails { get; set; }

        public MailResponse(string id, List<MailData> mails)
        {
            Id = id;
            Mails = mails;
        }
    }
}