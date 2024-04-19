using Azure.AI.OpenAI;
using YodaApp2.Models;

namespace YodaApp2.Services
{
    public interface AIAssistant
    {
        ChatResponseMessage GetCompletion(IList<ChatMessages> chatInboundHistory, ChatMessages userMessage);
    }
}
