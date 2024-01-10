using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace halit
{
    public partial class Default : System.Web.UI.Page
    {
        private const string EventsApiUrl = "https://localhost:7163/api/Event/"; // Replace with your actual API URL
        [WebMethod]
        public static string SaveEventApi(string title, string selectedDate)
        {
            try
            {
                return "Event saved successfully!";
            }
            catch (Exception ex)
            {
                return "Error saving event: " + ex.Message;
            }
        }
        public List<Event> events
        {
            get { return ViewState["EventsData"] as List<Event>; }
            set { ViewState["EventsData"] = value; }
        }
        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("AddEvent.aspx");
        }
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = GetUserIdFromCookies();
                events = await GetEventsData(userId);


                    HighlightEventDates(events);

                    DisplayEventDates();
                
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
        private async Task<List<Event>> GetEventsData(int userId)
        {
            List<Event> allEvents = new List<Event>();

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{EventsApiUrl}{userId}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    allEvents = JsonConvert.DeserializeObject<List<Event>>(data);
                }
            }

            return allEvents;
        }

        private void HighlightEventDates(List<Event> events)
        {

            foreach (Event eventData in events)
            {
                DateTime eventDate = eventData.Date;


                TableCell cell = FindCellByDate(calDisplay, eventDate);
                if (cell != null)
                {
                    cell.CssClass += " events-day";
                }
            }
        }

        private TableCell FindCellByDate(Calendar calendar, DateTime targetDate)
        {

            foreach (TableRow row in calendar.Controls.OfType<TableRow>())
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Controls.Count > 0 && cell.Controls[0] is LiteralControl)
                    {
                        DateTime cellDate;
                        if (DateTime.TryParse(((LiteralControl)cell.Controls[0]).Text, out cellDate))
                        {
                            if (cellDate.Date == targetDate.Date)
                            {
                                return cell;
                            }
                        }
                    }
                }
            }
            return null;
        }

        protected void calDisplay_SelectionChanged(object sender, EventArgs e)
            
        {

            DisplayEventDates();
            DisplaySelectedDate();
            HighlightEventDates(events);
        }



        private void DisplaySelectedDate()
        {

            lblSelectedDate.Text = "Selected Date: ";
            litSelectedDate.Text = calDisplay.SelectedDate.ToShortDateString();


            calDisplay.SelectedDayStyle.CssClass = "";


            calDisplay.SelectedDayStyle.CssClass = "selected-day";
        }

        protected void calDisplay_DayRender(object sender, DayRenderEventArgs e)
        {
            if (events != null)
            {
                DateTime currentDay = e.Day.Date;


                if (events.Any(eventData => eventData.Date.Date == currentDay.Date))
                {
                    e.Cell.CssClass += " events-day";
                }
            }
        }

        private void DisplayEventDates()
        {

            litEventDates.Text = "";


            DateTime selectedDate = calDisplay.SelectedDate.Date;


            foreach (Event eventData in events)
            {
                System.Diagnostics.Debug.WriteLine(eventData.Date.ToString());
                System.Diagnostics.Debug.WriteLine(selectedDate.ToString());
                if (eventData.Date == selectedDate)
                {
                    litEventDates.Text += $"{eventData.Title} - {eventData.Date}<br/>";
                }
            }
        }

    }
    [Serializable]
    public class Event
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
