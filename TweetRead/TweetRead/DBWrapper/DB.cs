using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using DataContracts;
using DBWrapper.DataModels;

namespace DBWrapper
{
    public class DB
    {

        private MongoClient client;
        private MongoServer server;

        public DB(string ConnectionString)
        {
            client = new MongoClient(ConnectionString);
            server = client.GetServer();
        }

         public void AddTweets(List<Tweet> Tweets)
            {
                MongoDatabase database = server.GetDatabase("Tweets");
          
                foreach (Tweet _tweet in Tweets)

                {
                   
                    MongoCollection<Mongo_Tweet> _lstMongoTweets = database.GetCollection<Mongo_Tweet>("Tweets");

                    Mongo_Tweet _mongotweet = new Mongo_Tweet();
                    _mongotweet.TweetFrom = _tweet.TweetFrom;
                    _mongotweet.TweetText = _tweet.TweetText;
                    _mongotweet.TweetTime = _tweet.TweetTime;
                    _mongotweet.TweetSource = _tweet.TweetSource;
                    _mongotweet.TweetTimezone = _tweet.TweetTimezone;

                    _lstMongoTweets.Insert(_mongotweet);

 
                }

            }


         public void DeleteTweets(List<Tweet> Tweets)
             {

                foreach (Tweet _tweet in Tweets)

                    {

                     MongoDatabase database = server.GetDatabase("Tweets");
                     MongoCollection<Mongo_Tweet> _lstMongoTweets = database.GetCollection<Mongo_Tweet>("Tweets");

                     IMongoQuery query = Query.EQ("_id", _tweet.Id);
                     Mongo_Tweet _mongotweet = _lstMongoTweets.FindOne(query);

                     _lstMongoTweets.Remove(query);

                    }

             }


         public List<Tweet> GetAllTweets()
         {

             MongoDatabase database = server.GetDatabase("Tweets");

             MongoCollection<Mongo_Tweet> _lstMongoTweets = database.GetCollection<Mongo_Tweet>("Tweets");

             List<Tweet> _lstTweets = new List<Tweet> { };

             foreach (Mongo_Tweet _mongotweet in _lstMongoTweets.FindAll())

             {
                
                 Tweet _Tweet = new Tweet();

                 _Tweet.Id= _mongotweet.Id.ToString();
                 _Tweet.TweetFrom = _mongotweet.TweetFrom;
                 _Tweet.TweetText = _mongotweet.TweetText;
                 _Tweet.TweetSource = _mongotweet.TweetSource;
                 _Tweet.TweetSource = _mongotweet.TweetTimezone;

                 _lstTweets.Add(_Tweet);
             
             }

             return _lstTweets;
         
         
         }

         public List<Tweet> GetTweetsByKey(string KeyName, string KeyValue)

         {
             MongoDatabase database = server.GetDatabase("Tweets");

             MongoCollection<Mongo_Tweet> _MongoTweets = database.GetCollection<Mongo_Tweet>("Tweets");

             IMongoQuery query = Query.EQ( KeyName, KeyValue);
             MongoCursor<Mongo_Tweet> _lstMongoTweets = _MongoTweets.Find(query);

             List<Tweet> _lstTweets = new List<Tweet> { };

             foreach (Mongo_Tweet _mongotweet in _lstMongoTweets)
             {

                 Tweet _Tweet = new Tweet();

                 _Tweet.Id = _mongotweet.Id.ToString();
                 _Tweet.TweetFrom = _mongotweet.TweetFrom;
                 _Tweet.TweetText = _mongotweet.TweetText;
                 _Tweet.TweetSource = _mongotweet.TweetSource;
                 _Tweet.TweetTimezone = _mongotweet.TweetTimezone;

                 _lstTweets.Add(_Tweet);

             }

             return _lstTweets;
         
         }



        


      
    }

    }
