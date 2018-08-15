using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using System.Reflection;

namespace AmazonNLP
{
    public class AmazonNLP
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public AmazonNLP()
        {
            AccessKeyId = "";
            SecretAccessKey = "";
        }

        private  DataTable FromEntity<T>( IList<T> LS) where T : class, new()
        {
            if (LS == null)
                return null;
            T entityType = new T();
            PropertyInfo[] entityProperties = entityType.GetType().GetProperties();
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
                dt.Columns.Add(entityProperties[i].Name);
            foreach (object entity in LS)
            {
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                dt.Rows.Add(entityValues);
            }
            return dt;
        }


        public DataTable Entities(string Text,string Encoding= "en")
        {
            DataTable DT = null;
            DetectEntitiesRequest request = new DetectEntitiesRequest();
            request.LanguageCode = Encoding;
            request.Text = Text;
            AmazonComprehendClient client = new AmazonComprehendClient(AccessKeyId, SecretAccessKey,Amazon.RegionEndpoint.USWest2);
            var RES = client.DetectEntities(request);
            DT = FromEntity(RES.Entities);
            DT.TableName = "Entities";
            return DT;
        }

        public DataTable KeyPhrases(string Text, string Encoding = "en")
        {
            DataTable DT = null;
            DetectKeyPhrasesRequest request = new DetectKeyPhrasesRequest();
            request.LanguageCode = Encoding;
            request.Text = Text;
            AmazonComprehendClient client = new AmazonComprehendClient(AccessKeyId, SecretAccessKey, Amazon.RegionEndpoint.USWest2);
            var RES = client.DetectKeyPhrases(request);
            DT = FromEntity(RES.KeyPhrases);
            DT.TableName = "KeyPhrases";
            return DT;
        }

        public DataTable Sentiment(string Text, string Encoding = "en")
        {
            DataTable DT = new DataTable();
            DT.TableName = "Sentiment";
            DT.Columns.Add("Sentiment", typeof(string));
            DT.Columns.Add("Mixed", typeof(float));
            DT.Columns.Add("Positive", typeof(float));
            DT.Columns.Add("Neutral", typeof(float));
            DT.Columns.Add("Negative", typeof(float));
            DetectSentimentRequest request = new DetectSentimentRequest();
            request.LanguageCode = Encoding;
            request.Text = Text;
            AmazonComprehendClient client = new AmazonComprehendClient(AccessKeyId, SecretAccessKey, Amazon.RegionEndpoint.USWest2);
            var RES = client.DetectSentiment(request);
            var dr = DT.NewRow();
            dr["Sentiment"] = RES.Sentiment.Value;
            dr["Mixed"] = RES.SentimentScore.Mixed;
            dr["Positive"] = RES.SentimentScore.Positive;
            dr["Neutral"] = RES.SentimentScore.Neutral;
            dr["Negative"] = RES.SentimentScore.Negative;
            DT.Rows.Add(dr);
            return DT;
        }

        

    }
}
