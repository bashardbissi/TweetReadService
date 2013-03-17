using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContracts
{
    public class Tweet
    {
        public string Id { get; set; }
        public string TweetFrom { get; set; }
        public string TweetText { get; set; }
        public string TweetTimezone { get; set; }
        public string TweetTime { get; set; }
        public string TweetSource { get; set; }

    }
}
