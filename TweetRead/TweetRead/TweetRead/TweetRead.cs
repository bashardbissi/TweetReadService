using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContracts;
using DBWrapper.DataModels;
using System.Net;
using System.Xml.Linq;

namespace TweetRead
{
    public class TweetRead : ITweetRead

  {
        public string GetAndSaveUserTweets(string ScreenName)
        {
           
            WebClient _twitter = new WebClient();

            string _TweetsString = "";
            try
            {
                _TweetsString = _twitter.DownloadString(new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=" + ScreenName + ""));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Not Found")) { return "Screen name not found"; }
                else { return ex.Message; }
                
            }

            XElement _xmlTweets = XElement.Parse(_TweetsString);

            IEnumerable<XElement> _lstXMlTweets = _xmlTweets.Descendants("status");

            int _RetrievedTweetsCount= 0;

            string StatusString = "";

            if (_lstXMlTweets.Count<XElement>() != 0)
            {

                _RetrievedTweetsCount = _lstXMlTweets.Count<XElement>();

                List<Tweet> _lstTweets = new List<Tweet>();

                foreach (XElement _RetrievedTweet in _lstXMlTweets)
                {
                    Tweet _tweet = new Tweet();
                    _tweet.TweetFrom = _RetrievedTweet.Element("user").Element("name").Value;
                    _tweet.TweetText = _RetrievedTweet.Element("text").Value;
                    if (_RetrievedTweet.Element("user").Element("created_at").Value != "")
                    {
                        string[] _CreatedAtParts = _RetrievedTweet.Element("user").Element("created_at").Value.Split(' ');

                        string day = "";
                        string month = "";
                        string year = "";

                        day = _CreatedAtParts[2];
                        year = _CreatedAtParts[5];

                        switch (_CreatedAtParts[1])
                        {
                            case "Jan":
                                month = "1";
                                break;
                            case "Feb":
                                month = "2";
                                break;
                            case "Mar":
                                month = "3";
                                break;
                            case "Apr":
                                month = "4";
                                break;
                            case "May":
                                month = "5";
                                break;
                            case "Jun":
                                month = "6";
                                break;
                            case "Jul":
                                month = "7";
                                break;
                            case "Aug":
                                month = "8";
                                break;
                            case "Sep":
                                month = "9";
                                break;
                            case "Oct":
                                month = "10";
                                break;
                            case "Nov":
                                month = "11";
                                break;
                            case "Dec":
                                month = "12";
                                break;

                        }

                        _tweet.TweetTime = month + "/" + day + "/" + year;

                    }
                   
                    if (_RetrievedTweet.Element("source").Value.Contains("<"))
                    {
                        _tweet.TweetSource = _RetrievedTweet.Element("source").Value.Substring(_RetrievedTweet.Element("source").Value.IndexOf(">") + 1, _RetrievedTweet.Element("source").Value.LastIndexOf("<") - _RetrievedTweet.Element("source").Value.IndexOf(">") - 1);
                    }

                    else
                    {
                        _tweet.TweetSource = _RetrievedTweet.Element("source").Value;
                    }

                    _tweet.TweetTimezone = _RetrievedTweet.Element("user").Element("time_zone").Value;

                    _lstTweets.Add(_tweet);
                }

                if (_lstTweets.Count > 0)
                {

                    try
                    {
                        AddTweets(_lstTweets);
                        StatusString = "(" + _RetrievedTweetsCount + ") tweets saved successfully";

                    }

                    catch
                    {

                        //Error can be handled here using Log4Net, Email notifications ...etc
                        StatusString = "Faild to save tweets";

                    }


                }

                else
                {
                    StatusString = "No tweets saved";

                }

            }

            else { StatusString = "No tweets retrieved"; }


            return StatusString;
           
        }

        public void AddTweets(List<Tweet> Tweets)
          {

              try
                  {
                      DBWrapper.DB _DB = new DBWrapper.DB(System.Configuration.ConfigurationManager.AppSettings.Get("DBServerName"));
                      _DB.AddTweets(Tweets);
                  }
              catch
                  {
                      //Error can be handled here using Log4Net, Email notifications, returning error to user as a friendly string... etc.
                  }

          }

      public void DeleteTweets(List<Tweet> Tweets)
          {

              try
              {
                  DBWrapper.DB _DB = new DBWrapper.DB(System.Configuration.ConfigurationManager.AppSettings.Get("DBServerName"));
                  _DB.DeleteTweets(Tweets);
              }
              catch
              {
                  //Error can be handled here using Log4Net, Email notifications, returning error to user as a friendly string... etc.
              }
      
          }

      public List<Tweet> GetAllTweets()
      {

          try
          {
              DBWrapper.DB _DB = new DBWrapper.DB(System.Configuration.ConfigurationManager.AppSettings.Get("DBServerName"));
              return  _DB.GetAllTweets();
          }
          catch
          {
              //Error can be handled here using Log4Net, Email notifications, returning error to user as a friendly string... etc.

              return null;
          }
      
      
      }


     public List<Tweet> GetTweetsByKey(string KeyName, string KeyValue)
      {

          try
          {
              DBWrapper.DB _DB = new DBWrapper.DB(System.Configuration.ConfigurationManager.AppSettings.Get("DBServerName"));
              return _DB.GetTweetsByKey(KeyName, KeyValue);
          }
          catch
          {
              //Error can be handled here using Log4Net, Email notifications, returning error to user as a friendly string... etc.

              return null;
          }
      
      
      }


   
  }
}