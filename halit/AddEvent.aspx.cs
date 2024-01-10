using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace halit
{
    public partial class AddEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                
            }
        }
        private int GetUserIdFromCookies()
        {

            if (Request.Cookies["UserId"] != null)
            {
                int userId;
                if (int.TryParse(Request.Cookies["UserId"].Value, out userId))
                {
                    return userId;
                }
            }

            return -1; 
        }
        protected async void btnSave_Click(object sender, EventArgs e)
        {


            string title = txtTitle.Text;
            int userId = GetUserIdFromCookies();



            Console.WriteLine("Date: " + txtDate.Text);
            Console.WriteLine("Title: " + title);


            bool isSuccess = await SaveEventApi(txtDate.Text, title,userId);
            if (isSuccess)
            {
                
            }
            else
            {

                string script = "alert('Failed to save data. Please try again.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", script, true);
            }
        }

        private async Task<bool> SaveEventApi(string date, string title, int userId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiEndpoint = "https://localhost:7163/api/Event";

                    var requestData = new {userId =userId, date = date, title = title};

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);
                    System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        Response.Redirect("Default.aspx");
                        return true;
                    }
                    else
                    {
                        string script = "alert('Failed to save data. Please try again.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", script, true);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        
    }
}
