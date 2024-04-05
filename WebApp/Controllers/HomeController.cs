using App.ApplicationLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using WebApp.Infrastructure;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        //private readonly IReadService _readService;


        private readonly ILogger<HomeController> _logger;

        private const string token = "eyJhbGciOiJIUzI1NiJ9.eyJ0aWQiOjI4MzcwMjk2OSwiYWFpIjoxMSwidWlkIjo0ODc0ODg0OCwiaWFkIjoiMjAyMy0wOS0yMlQxMzoxNTozMS4yOTZaIiwicGVyIjoibWU6d3JpdGUiLCJhY3RpZCI6Njc4MTg4NiwicmduIjoidXNlMSJ9.4gYxviwwhkrve9tUVdoFOoXNgZELqNku73lz_Sw3vCE";

        private const string version = "2023-7";

        private const string query1 = "{ me { name } boards (ids: [5195170630]) { id name } }";
        private const string mutationUpdate = @"mutation { create_update (item_id: 5195295018, body: ""Created by Ridel with GraphQL 10/12-6"") { id } }";



        public HomeController(
            ILogger<HomeController> logger
            //, IReadService readService
            )
        {
            //_readService = readService;
            _logger = logger;
        }


        #region monday
        public IActionResult Index()
        {

            //var names = _readService.GetDescs().ToList();

            return View();
        }


        private async Task<JsonResult> SaveV1(
                        ItemM model
            )
        {
            string? data = null;
            bool ok;
            string? errorMessage = null;
            try
            {
                if (ModelState.IsValid)
                    errorMessage = "Invalid model";

                var mutation = $@"mutation {{
                                     create_item (board_id: 5309837814, group_id: ""topics"", item_name: ""{model.ProjectName}"", 
                                       column_values: ""{{
                                           \""long_text93\"":\""{model.ExecutiveSummary}\"",
                                           \""project_type\"":\""{model.Priority}\"",
                                           \""status_19\"":\""{model.ProjectType}\""
                                           }}"") 
                                       {{ id }} }}";

                var query = mutationUpdate;

                using (var client = new HttpClient())
                {
                    string prefix = "{\"mutation\":\"";
                    string suffix = "\"}";
                    string content = prefix + query + suffix;

                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.monday.com/v2/"))
                    {
                        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                        request.Headers.Add("Authorization", token);
                        request.Headers.Add("API-Version", version);

                        using (HttpResponseMessage response = await client.SendAsync(request))
                        {
                            data = await response.Content.ReadAsStringAsync();
                            ok = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                ok = false;
            }

            return Json(new { success = ok, errorMessage, value = data });
        }

        private async Task<JsonResult> SaveV2(
                        ItemM model
            )
        {
            string? data = null;
            bool ok;
            string? errorMessage = null;
            try
            {
                if (ModelState.IsValid)
                    errorMessage = "Invalid model";

                var mutation = $@"mutation {{
                                     create_item (board_id: 5309837814, group_id: ""topics"", item_name: ""{model.ProjectName}"", 
                                       column_values: ""{{
                                           \""long_text93\"":\""{model.ExecutiveSummary}\"",
                                           \""project_type\"":\""{model.Priority}\"",
                                          \""status_19\"":\""{model.ProjectType}\"",
                                           \""numbers\"":\""{model.StrategicAlignment}\"",
                                           \""numbers_1\"":\""{model.CustomerBenefit}\"",
                                           \""risk_reduction\"":\""{model.CommercialBenefit}\"",
                                           \""commercial_benefit\"":\""{model.TechnicalBenefit}\""
                                           }}"") 
                                       {{ id }} }}";

                var query = mutation;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.monday.com");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("API-Version", version);
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    //HTTP POST
                    //var postTask = client.PostAsync("/v2/", new StringContent(jsonData, Encoding.UTF8, "application/json"));

                    //var postTask = client.PostAsJsonAsync("/v2/", new BodyQ() { Query = @"query { me { name } 
                    //                                                                        boards (ids: [5195170630]) { id name } 
                    //                                                                        }" });
                    //var postTask = client.PostAsJsonAsync("/v2/", new BodyQ() { Query = @"mutation { create_update (item_id: 5195295018, body: ""Created by Ridel with GraphQL 10/12-7"") { id } }" });

                    var postTask = client.PostAsJsonAsync("/v2/", new BodyQ() { Query = query });

                    postTask.Wait();

                    var result = postTask.Result;
                    ok = result.IsSuccessStatusCode;

                    if (ok)
                    {

                        data = await result.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        errorMessage = "Web api Error";
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                ok = false;
            }

            return Json(new { success = ok, errorMessage, value = data });
        }

        [HttpPost]
        public async Task<JsonResult> Save(
            ItemM model
            )
        {

            return await SaveV2(model);
        }


        [HttpPost]
        public JsonResult TestSave(
           [FromForm]ItemM model
           )
        {

            return Json(new { success = true, value = "OK" });
        }

        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region exchange

        public IActionResult Calendar()
        {
            return View();
        }

        private async System.Threading.Tasks.Task Example1()
        {
            var passSecureString = new SecureString();
            string pass = "1mayo1983-21";
            Array.ForEach(pass.ToArray(), passSecureString.AppendChar);

            var service = MyExchangeService.UseExchangeService("rdominguez", passSecureString);
            // Initialize values for the start and end times, and the number of appointments to retrieve.
            DateTime startDate = new DateTime(2024, 1, 8);
            DateTime endDate = startDate.AddDays(1);
            const int NUM_APPTS = 5;
            // Initialize the calendar folder object with only the folder ID. 
            CalendarFolder calendar = await CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());
            // Set the start and end time and number of appointments to retrieve.
            CalendarView cView = new CalendarView(startDate, endDate, NUM_APPTS);
            // Limit the properties returned to the appointment's subject, start time, and end time.
            cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);
            // Retrieve a collection of appointments by using the calendar view.
            FindItemsResults<Appointment> appointments = await calendar.FindAppointments(cView);


            foreach (var item in appointments)
            {
                PropertySet props = new PropertySet(BasePropertySet.IdOnly,
                                      ItemSchema.Body,
                                      AppointmentSchema.Organizer,
                                      AppointmentSchema.Location,
                                      AppointmentSchema.Resources
                                      );

                Appointment tmp = await Appointment.Bind(service, item.Id, props);

                string body = tmp.Body.Text;
                string organizer = tmp.Organizer.Name;
                string location = tmp.Location;



            }
        }

        private async System.Threading.Tasks.Task Example2()
        {
            var passSecureString = new SecureString();
            string pass = "1mayo1983-21";
            Array.ForEach(pass.ToArray(), passSecureString.AppendChar);

            ExchangeService service = MyExchangeService.UseExchangeService("rdominguez", passSecureString);

            string locationFilter = "ISS-Chip".ToLower();

            DateTime startDate = new DateTime(2024, 2, 19);
            DateTime endDate = startDate.AddDays(10);

            // Define a calendar view for a date range (you can adjust this based on your needs)
            CalendarView calendarView = new CalendarView(startDate, endDate);

            // Specify the property set for the appointment properties you want to retrieve
            //PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties);
            //propertySet.Add(AppointmentSchema.Subject);
            //propertySet.Add(AppointmentSchema.Start);
            //propertySet.Add(AppointmentSchema.End);
            //propertySet.Add(AppointmentSchema.Location);

            // binding to the calendar folder of that user
            FolderId targetUserCalendarId = new FolderId(WellKnownFolderName.Calendar, new Mailbox("ISS-ChipUrbachConRm@pbc.gov") );

            CalendarFolder calendarFolder = await CalendarFolder.Bind(service, targetUserCalendarId);


            PropertySet propertySet = new PropertySet(
                                      BasePropertySet.IdOnly,
                                      AppointmentSchema.Subject,
                                      //ItemSchema.Body,
                                      AppointmentSchema.Start,
                                      AppointmentSchema.End,
                                      AppointmentSchema.Organizer,
                                      AppointmentSchema.Location
                                      );


            // Retrieve appointments using FindAppointments method
            //FindItemsResults<Appointment> findResults = await service.FindAppointments(WellKnownFolderName.Calendar, calendarView);

            FindItemsResults<Appointment> findResults = await calendarFolder.FindAppointments(calendarView);

            var locationAppointments = findResults.Items
              .Where(appointment => (appointment.Location != null) && appointment.Location.ToLower().Contains(locationFilter)).ToList();

            List<dynamic> list = new List<dynamic>();

            // Display appointment information
            foreach (var appointment in locationAppointments)
            {
                //Appointment tmp = await Appointment.Bind(service, appointment.Id, propertySet);

                var tmp = appointment;
                if ((tmp.Location != null) && tmp.Location.ToLower().Contains(locationFilter))
                {
                    list.Add(new
                    {
                        tmp.Subject,
                        tmp.Organizer,
                        tmp.Start,
                        tmp.End,
                    });

                }
                //Console.WriteLine($"Subject: {appointment.Subject}");
                //Console.WriteLine($"Start: {appointment.Start}");
                //Console.WriteLine($"End: {appointment.End}");
                //Console.WriteLine($"Location: {appointment.Location}");
                //Console.WriteLine();
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetExchangeAppointments(

)
        {
            try
            {
                Example2();

                return Json(new { success = true, value = "OK" });
            }
            catch (Exception e)
            {
                var ex = e;
            }
            return Json(new { success = false, value = "Error" });
        }


        [HttpPost]
        public async Task<JsonResult> GetAppoiments(
          RequestCalendarInfoViewModel model
          )
        {

            string? errorMessage = null;
            bool isSuccess = true;
            List<dynamic> list = new List<dynamic>();


            try
            {
                var passSecureString = new SecureString();
                string pass = "1mayo1983-21";
                Array.ForEach(pass.ToArray(), passSecureString.AppendChar);

                ExchangeService service = MyExchangeService.UseExchangeService("rdominguez", passSecureString);

                string locationFilter = "ISS-Chip".ToLower();

                var month = int.Parse(model.Month.Substring(model.Month.Length - 2, 2));
                var year = int.Parse(model.Month.Substring(0, 4));

                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                // Define a calendar view for a date range (you can adjust this based on your needs)
                CalendarView calendarView = new CalendarView(startDate, endDate);


                // binding to the calendar folder of that user
                FolderId targetUserCalendarId = new FolderId(WellKnownFolderName.Calendar, new Mailbox(model.Location));

                CalendarFolder calendarFolder = await CalendarFolder.Bind(service, targetUserCalendarId);


                PropertySet propertySet = new PropertySet(
                                          BasePropertySet.IdOnly,
                                          AppointmentSchema.Subject,
                                          //ItemSchema.Body,
                                          AppointmentSchema.Start,
                                          AppointmentSchema.End,
                                          AppointmentSchema.Organizer,
                                          AppointmentSchema.Location
                                          );

                FindItemsResults<Appointment> findResults = await calendarFolder.FindAppointments(calendarView);

                var locationAppointments = findResults.Items
                  /*.Where(appointment => (appointment.Location != null) && appointment.Location.ToLower().Contains(locationFilter))*/.ToList();


                // Display appointment information
                foreach (var appointment in locationAppointments)
                {
                    var tmp = appointment;
                    if ((tmp.Location != null) && tmp.Location.ToLower().Contains(locationFilter))
                    {
                        list.Add(new
                        {
                            tmp.Subject,
                            tmp.Organizer,
                            tmp.Start,
                            tmp.End,
                        });

                    }
                }

            }
            catch (Exception ex)
            {

                isSuccess = false;
                errorMessage = ex.Message;
            }
            return Json(new { success = isSuccess, errorMessage, value = list });
        }

        #endregion


    }
}