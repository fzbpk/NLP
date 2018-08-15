using System.Net;
using System.Text;

namespace CoreNLP
{
    public class CoreNLP
    {
        private string PostJson(string url,string postString)
        {
            using (WebClient webClient = new WebClient())
            { 
                byte[] postData = Encoding.UTF8.GetBytes(postString); 
                byte[] responseData = webClient.UploadData(url, "POST", postData);   
                return Encoding.UTF8.GetString(responseData);
            }
        } 

        public void test()
        {
            string url = "http://localhost:9000/?properties%3d%7b%22annotators%22%3a+%22tokenize%2cssplit%2cpos%22%2c+%22outputFormat%22%3a+%22json%22%7d";
            string json = PostJson(url, "Kosgi Santosh sent an email to Stanford University.");
        }
    }
}
