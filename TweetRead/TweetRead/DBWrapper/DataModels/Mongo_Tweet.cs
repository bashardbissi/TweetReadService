using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DBWrapper.DataModels
{
    class Mongo_Tweet
    {
        public ObjectId Id { get; set; }
        public string TweetFrom { get; set; }
        public string TweetText { get; set; }
        public string TweetTimezone { get; set; }
        public string TweetTime { get; set; }
        public string TweetSource { get; set; }
    }
}