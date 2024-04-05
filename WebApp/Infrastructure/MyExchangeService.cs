using Microsoft.Exchange.WebServices.Data;
using System.Net;
using System.Security;

namespace WebApp.Infrastructure
{
    public static class MyExchangeService
    {

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        public static ExchangeService UseExchangeService(string userEmailAddress, SecureString userPassword)
        {
            ExchangeService service = new ExchangeService();

            service.Url = new Uri("https://mail.example.com/EWS/Exchange.asmx"); // Replace with your Exchange server URL

            #region Authentication

            // Set specific credentials.
            service.Credentials = new NetworkCredential(userEmailAddress, userPassword);
            #endregion

            #region Endpoint management

            try
            {
                //// Look up the user's EWS endpoint by using Autodiscover.
                //service.AutodiscoverUrl(userEmailAddress, RedirectionUrlValidationCallback);

                service.Url = new Uri("https://webmail.pbcgov.org/EWS/Exchange.asmx"); // Replace with your Exchange server URL

            }
            catch (Exception)
            {

                throw;
            }

            Console.WriteLine("EWS Endpoint: {0}", service.Url);
            #endregion
            return service;
        }
    }
}
