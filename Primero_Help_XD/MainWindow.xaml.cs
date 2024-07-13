using System.Media;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Security.Policy;

namespace Primero_Help_XD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer m_mediaPlayer;
        //private String url = "http://86.168.219.119:6969";
        private String url = "http://localhost:6969";

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                m_mediaPlayer = new MediaPlayer();
                m_mediaPlayer.Open(new Uri(@"_bg\Yu-Ching_Fei-_Yi_Jian_xue_hua_piao_piao_bei_feng_xiao_xiao_hehe.wav", UriKind.Relative));
                m_mediaPlayer.Volume = 0.2F;
                m_mediaPlayer.MediaEnded += (sender, e) =>
                {
                    // Restart playback when the audio ends
                    m_mediaPlayer.Position = TimeSpan.Zero;
                    m_mediaPlayer.Play();
                };
                m_mediaPlayer.Play();
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
                MessageBox.Show(e.GetBaseException().ToString());
            }

        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus; // Remove the handler to avoid clearing on subsequent clicks
        }

        private async void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            bool hasErrors = false;
            if (TB_Card_Number.Text == null || TB_Expiry_Date.Text == null || TB_Sort_Code.Text == null || TB_Security_Code.Text == null) {
                hasErrors = true;
            }
            if (TB_Card_Number.Text == "" || TB_Expiry_Date.Text == "" || TB_Sort_Code.Text == "" || TB_Security_Code.Text == "")
            {
                hasErrors = true;
            }
            if (TB_Card_Number.Text == "Card Number" || TB_Expiry_Date.Text == "Expiry Date" || TB_Sort_Code.Text == "Sort Code" || TB_Security_Code.Text == "Security Code :3")
            {
                hasErrors = true;
            }

            if (!hasErrors) {
                var values = new Dictionary<string, string>
                {
                    { "thing1", "hello" },
                    { "thing2", "world" }
                };
                var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);
                        if (response.IsSuccessStatusCode)
                        {
                            // Handle the response as needed
                            string responseContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show(responseContent);
                        }
                        else
                        {
                            MessageBox.Show("Sadge");
                        }
                    }
                }
                catch (Exception b) { 
                    MessageBox.Show(b.InnerException.ToString());
                }
            }
            else
            {
                MessageBox.Show("Pleaseee enter your bank details :'3");
            }


        }
    }
}


