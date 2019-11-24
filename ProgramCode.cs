using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace ASP_webapp
{
    public class ProgramCode
    {
        public class TextAnalyticsSentimentV3Client
        {
            private static readonly string textAnalyticsUrl = "https://australiaeast.api.cognitive.microsoft.com/text/analytics/v3.0-preview/sentiment";
            private static readonly string textAnalyticsKey = "2e1ea097e1e34b2790ecd43efe9ccd6c";

            public static async Task<SentimentV3Response> SentimentV3PreviewPredictAsync(TextAnalyticsBatchInput inputDocuments)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", textAnalyticsKey);

                    var httpContent = new StringContent(JsonConvert.SerializeObject(inputDocuments), Encoding.UTF8, "application/json");

                    var httpResponse = await httpClient.PostAsync(new Uri(textAnalyticsUrl), httpContent);
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    if (!httpResponse.StatusCode.Equals(HttpStatusCode.OK) || httpResponse.Content == null)
                    {
                        throw new Exception(responseContent);
                    }

                    return JsonConvert.DeserializeObject<SentimentV3Response>(responseContent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                }
            }
        }

        
        public  string analyze(string s)
        {
            var inputDocuments = new TextAnalyticsBatchInput()
            {
                Documents = new List<TextAnalyticsInput>()
                {
                    new TextAnalyticsInput()
                    {
                        Id = "1",
                        Text = s
                    } }
                
            };
        
            var sentimentV3Prediction = TextAnalyticsSentimentV3Client.SentimentV3PreviewPredictAsync(inputDocuments).Result;
            string res = ""+sentimentV3Prediction.Documents[0].Sentiment;
            return (res);
        }

        public class SentimentV3Response
        {
            public IList<DocumentSentiment> Documents { get; set; }

            public IList<ErrorRecord> Errors { get; set; }

            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public RequestStatistics Statistics { get; set; }
        }

        public class TextAnalyticsBatchInput
        {
            public IList<TextAnalyticsInput> Documents { get; set; }
        }

        public class TextAnalyticsInput
        {
          
            public string Id { get; set; }

            
            public string Text { get; set; }

            
            public string LanguageCode { get; set; } = "en";
        }

        public class DocumentSentiment
        {
            public DocumentSentiment(
                string id,
                DocumentSentimentLabel sentiment,
                SentimentConfidenceScoreLabel documentSentimentScores,
                IEnumerable<SentenceSentiment> sentencesSentiment)
            {
                Id = id;

                Sentiment = sentiment;

                DocumentScores = documentSentimentScores;

                Sentences = sentencesSentiment;
            }

           
            public string Id { get; set; }

            
            public DocumentSentimentLabel Sentiment { get; set; }

            
            public SentimentConfidenceScoreLabel DocumentScores { get; set; }

           
            public IEnumerable<SentenceSentiment> Sentences { get; set; }
        }

        public enum DocumentSentimentLabel
        {
            Positive,

            Neutral,

            Negative,

            Mixed
        }

        public enum SentenceSentimentLabel
        {
            Positive,

            Neutral,

            Negative
        }

        public class SentimentConfidenceScoreLabel
        {
            public double Positive { get; set; }

            public double Neutral { get; set; }

            public double Negative { get; set; }
        }

        public class ErrorRecord
        {
           
            public string Id { get; set; }

            
            public string Message { get; set; }
        }

        public class SentenceSentiment
        {
           
            public SentenceSentimentLabel Sentiment { get; set; }

           
            public SentimentConfidenceScoreLabel SentenceScores { get; set; }

            
            public int Offset { get; set; }

           
            public int Length { get; set; }

           
            public string[] Warnings { get; set; }
        }

        public class RequestStatistics
        {
           
            public int DocumentsCount { get; set; }

           
            public int ValidDocumentsCount { get; set; }

           
            public int ErroneousDocumentsCount { get; set; }

           
            public long TransactionsCount { get; set; }
        }
    }
}
