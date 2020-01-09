using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace TestRestClient
{
    public class MainWindowViewModel : BindableBase
    {
        private HttpClient client;
        private string resultText;

        public string Url { get; set; }

        public string ResultText
        {
            get { return resultText; }
            set
            {
                resultText = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SendCommand { get; set; }

        public MainWindowViewModel()
        {
            InitClient();

            SendCommand = new DelegateCommand(async () => await SendAsync());
        }

        private void InitClient()
        {
            client = new HttpClient();
        }

        private async Task SendAsync()
        {
            ResultText = await GetResult(Url);
        }

        private async Task<string> GetResult(string url)
        {
            string result = null;

            try
            {
                var stringTask = client.GetStringAsync(url);
                result = await stringTask;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return result;
        }
    }
}
