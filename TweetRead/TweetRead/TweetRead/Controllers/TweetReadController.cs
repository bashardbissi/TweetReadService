using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataContracts;

namespace TweetRead.Controllers
{

  public class TweetController : ApiController
  {
      
    private TweetRead _TweetRead = new TweetRead();

    public IEnumerable<Tweet> GET()
    {
        return _TweetRead.GetAllTweets();
    }

    public string GET(string ScreenName) 
    
    {  
        return _TweetRead.GetAndSaveUserTweets(ScreenName);
    }

    public List<Tweet> GET(string KeyName, string KeyValue)
    {
        return _TweetRead.GetTweetsByKey(KeyName, KeyValue);
    }


    public void PUT(List<Tweet> Tweets)
    
    {
        _TweetRead.AddTweets(Tweets);
    }

    void DELETE(List<Tweet> Tweets)
    {
        _TweetRead.DeleteTweets(Tweets);
    
    }

  }
}
