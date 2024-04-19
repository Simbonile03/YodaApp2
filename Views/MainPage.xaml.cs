using YodaApp2.ViewModels;

namespace YodaApp2.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainView _viewModel;
        private readonly HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = (MainView)BindingContext;
            _httpClient = new HttpClient();
        }

        private async void GenerateResponse_Clicked(object sender, EventArgs e)
        {
            await _viewModel.GenerateYodaResponse(InputEntry.Text);
        }

        private async void GetFunFact_Clicked(object sender, EventArgs e)
        {
            var funFact = await _viewModel.GetFunFact();
            await DisplayAlert("Fun Fact", funFact, "OK");
        }


    }
}
