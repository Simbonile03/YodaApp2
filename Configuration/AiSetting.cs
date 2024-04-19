namespace YodaApp2.Configuration
{
    public interface AiSettings
    {
        public string AzureSearchEndPoint { get; }
        public string AzureSearchKey { get; }
        public string AzureOpenAiEndPoint { get; }
        public string AzureOpenAiKey { get; }

    }
}
