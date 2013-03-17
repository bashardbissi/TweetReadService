using System.Collections.Generic;
using DataContracts;

namespace TweetRead
{
    public interface ITweetRead
  {

       string GetAndSaveUserTweets(string ScreenName);

       void AddTweets(List<Tweet> Tweets);

       void DeleteTweets(List<Tweet> Tweets);

       List<Tweet> GetAllTweets();

       List<Tweet> GetTweetsByKey(string KeyName, string KeyValue);

  }
}
