using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatGPTIntegration.Services
{
    public class GenAiServices
    {
        public async Task<string> GenerateOptimizedQueryAsync(string prompt)
        {
            string apiKey = "api key";
            //string apiUrl = "https://api.openai.com/v1/completions";
            string apiUrl = "https://openrouter.ai/api/v1/";
            prompt = $"You are a highly professional and expert SQL assistant Optimize the following SQL query: {prompt}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                //client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.BaseAddress = new Uri(apiUrl);

                var requestBody = new
                {
                    model = "deepseek/deepseek-r1:free", // Specify the GPT model
                    messages = new List<ChatMessage>
                    {
                        //new ChatMessage
                        //{
                        //    role = "system",
                        //    content = "You are a highly professional and expert SQL assistant"
                        //},
                        new ChatMessage
                        {
                            role = "user",
                            content = prompt
                        }
                    },
                    //Stream = false, // Set to true if you want streaming responses
                    //max_tokens = 150, // Adjust token limit as needed
                    //temperature = 0.7 // Adjust creativity level
                };

                string jsonBody = JsonSerializer.Serialize(requestBody);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("chat/completions", content);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync(); 

                    // With the following corrected line:  
                    var result = JsonSerializer.Deserialize<JsonElement>(responseBody);

                    return result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
