using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using GroceryManSystem.Models;
using Twilio.Types;

namespace GroceryManagement1.Services
{
    public class NotificationService
    {
        //private readonly List<IObserver> observers = new List<IObserver>();


        public static void SendNotification(User user, string message)
        {
            const string accountSid = "AC465962ff48f5cb5836314f4091aca9de";
            const string authToken = "cb81f5f8bd5a86dc8b321990a005d491";
            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber("+19782957121"),
                to: new Twilio.Types.PhoneNumber(user.PhoneNumber)
            );
        }
    }
}


