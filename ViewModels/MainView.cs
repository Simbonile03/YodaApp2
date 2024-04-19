using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using YodaApp2.Services;

namespace YodaApp2.ViewModels
{
    public class MainView : INotifyPropertyChanged
    {
        private string _yodaResponse;
        private readonly OpenAIService _openAIService;
        private readonly HttpClient _httpClient;

        public MainView()
        {

            _httpClient = new HttpClient();
        }

        public string YodaResponse
        {
            get => _yodaResponse;
            set
            {
                _yodaResponse = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GenerateYodaResponse(string inputText)
        {
            var yodaResponse = await GenerateYodaLikeResponse(inputText);
            _yodaResponse = yodaResponse;
        }

        public async Task<string> GenerateYodaLikeResponse(string prompt)
        {
            var requestUri = "https://loadsheddingaism.openai.azure.com/"; // Replace with the actual request URI
            var requestBody = new
            {
                prompt = $"Yoda says: {prompt}",
                temperature = 0.7,
                max_tokens = 50
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);

            var response = await _httpClient.PostAsync(requestUri, new StringContent(jsonRequest));

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to generate Yoda-like response.");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            return responseObject.choices[0].text;
        }

        public async Task<string> GetFunFact()
        {
            return await _openAIService.GetFunFact();
        }
    }

}
