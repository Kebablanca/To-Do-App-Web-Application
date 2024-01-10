using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace halit
{
    public partial class RegisterPage : System.Web.UI.Page
    {


        protected async void RegisterButton_Click(object sender, EventArgs e)
        {

            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                bool isSuccess = await CallRegisterApi(email, password, name, surname);

                if (isSuccess)
                {

                    Response.Redirect("LoginPage.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showErrorPopup", "showErrorPopup();", true);
                }
            }


        }

        private async Task<bool> CallRegisterApi(string email, string password, string name, string surname)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiEndpoint = "https://localhost:7163/api/User";

                    var requestData = new { name = name, lastName = surname, email = email, password = password };

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Register successfully.");

                        return true;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Register failed. Error: " + errorResponse);

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during Register: " + ex.Message);
                return false;
            }
        }

    }
}
