using Azure;
using Azure.AI.OpenAI;
using YodaApp2.Configuration;
using YodaApp2.Models;



namespace YodaApp2.Services
{
    public class OpenAIService : AIAssistant
    {
        private AiSettings _settings;
        private const string AssistantMessage = "I am an AI assistant that can help you with your loadshedding questions.";

        public OpenAIService(AiSettings settings)
        {
            _settings = settings;
        }

        public ChatResponseMessage GetCompletion(IList<ChatMessages> chatInboundHistory, ChatMessages userMessage)
        {
            var client = new OpenAIClient(new Uri(_settings.AzureOpenAiEndPoint), new AzureKeyCredential(_settings.AzureOpenAiKey));
            string deploymentName = "gbt35turbo16";
            string searchIndex = "index";

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                        {
                            new AzureSearchChatExtensionConfiguration()
                            {
                                SearchEndpoint = new Uri(_settings.AzureSearchEndPoint),
                                Authentication = new OnYourDataApiKeyAuthenticationOptions(_settings.AzureSearchKey),
                                IndexName = searchIndex,
                            }
                        }
                },
                DeploymentName = deploymentName
            };

            foreach (var ChatResponse in chatInboundHistory)
            {

            }

            Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);

            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;

            return responseMessage;
        }
        public async Task<string> GetFunFact()
        {
            // Call the AIAssistant to retrieve a fun fact
            GetFunFact();
            return "Fun fact: The first computer virus was created in 1983.";
        }
    }
}
