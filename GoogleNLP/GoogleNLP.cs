using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Language.V1;
using Grpc.Auth;
using Grpc.Core;
using Google.Api.Gax.Grpc;

namespace GoogleNLP
{
    public class Google
    {
        int TimeOut = 0;
        public Dictionary<string, object> Config { set; private get; }
        public void test()
        {
            LanguageServiceSettings settings = new LanguageServiceSettings();
            if (TimeOut > 0)
            {
                int hour = TimeOut / 3600;
                int min = TimeOut / 60;
                if (TimeOut >= 60)
                    TimeOut = TimeOut % 60;
                TimeSpan ts0 = new TimeSpan(hour, min, TimeOut);
                settings.AnalyzeSentimentSettings = CallSettings.FromCallTiming(CallTiming.FromTimeout(ts0));
            }
            string json = "{\"type\": \"service_account\",\"project_id\": \"" + Config["project_id"] + "\",\"private_key_id\": \"" + Config["private_key_id"] + "\",\"private_key\": \"" + Config["private_key"] + "\",\"client_email\": \"" + Config["client_email"] + "\",\"client_id\": \"" + Config["client_id"] + "\",\"auth_uri\": \"" + Config["auth_uri"] + "\",\"token_uri\": \"" + Config["token_uri"] + "\",\"auth_provider_x509_cert_url\": \"" + Config["auth_provider_x509_cert_url"] + "\",\"client_x509_cert_url\": \"" + Config["client_x509_cert_url"] + "\"}";
            var credential = GoogleCredential.FromJson(json).CreateScoped(LanguageServiceClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(
                LanguageServiceClient.DefaultEndpoint.ToString(),
                credential.ToChannelCredentials());
            LanguageServiceClient test = LanguageServiceClient.Create(channel, settings);
            var Sentimentresponse = test.AnalyzeSentiment(new Document()
            {
                Content = "hello",
                Type = Document.Types.Type.PlainText
            });
        }
    }
}
