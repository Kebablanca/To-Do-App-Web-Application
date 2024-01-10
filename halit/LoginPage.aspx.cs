using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace halit
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected async void LoginButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                bool isSuccess = await CallLoginApi(email, password);
                int userId = await GetUserIdApi(email);
                System.Diagnostics.Debug.WriteLine(isSuccess);
                System.Diagnostics.Debug.WriteLine(userId);
                if (isSuccess && userId != -1)
                {
                    SaveUserIdToCookies(userId);
                    Response.Redirect($"Default.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showErrorPopup", "showErrorPopup();", true);
                }
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("RegisterPage.aspx");


        }

        private async Task<bool> CallLoginApi(string email, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiEndpoint = "https://localhost:7163/api/Auth/Authenticate";

                    var requestData = new { email = email, password = password };

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {

                        return true;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Login failed. Error: " + errorResponse);

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during login: " + ex.Message);
                return false;
            }
        }

        private async Task<int> GetUserIdApi(string email)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiEndpoint = "https://localhost:7163/api/Auth/GetUserId";

                    var requestData = new { email = email,};

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);
                    System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if (int.TryParse(responseBody, out int intValue))
                        {
                            System.Diagnostics.Debug.WriteLine("Response value: " + intValue);
                            return intValue;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Failed to parse response body as integer.");
                            return 0; 
                        }
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error during geting user data. " + errorResponse);
                        System.Diagnostics.Debug.WriteLine("Error during geting user data. " + errorResponse);
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception during geting user data: " + ex.Message);
                Console.WriteLine("Exception during geting user data: " + ex.Message);
                return -1;
            }
        }
        private void SaveUserIdToCookies(int userId)
        {
            HttpCookie userIdCookie = new HttpCookie("UserId");
            userIdCookie.Value = userId.ToString();
            userIdCookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(userIdCookie);
        }

    }
}
