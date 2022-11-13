using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    /* Observer pattern - Behavioral pattern
     * - defines a subscription mechanism  to notify objects about any events that happen to the object they're observing
     * - allows you to change or take action on a set of objects when and if the state of another object changes
     * - used when some objects must observe others, but only for a limited time or in specific cases
     */ 


    // **** Subscriber Interface ****
    //Declares the actual notification interface
    interface EventListener
    {
        void update(Event eventType);
    }

    // **** Concrete Subscribers ****
    //They perform some actions in response to notifications issued by the publisher

    //class for customers who chose to be notified via email
    class EmailMsgListener : EventListener
    {
        string email;

        public EmailMsgListener(string email)
        {
            this.email = email;
        }


        public void update(Event eventType)
        {
            //actually send the email
            Console.WriteLine($"{email}, you've got a new Email Message for {eventType}");
        }
    }

    //class for customers that chose to be notified via push notifications
    class MobileAppListener:EventListener
    {
        string username;

        public MobileAppListener(string username)
        {
            this.username = username;
        }

        public void update(Event eventType)
        {
            //actually send the push notification
            Console.WriteLine($"{username}, you've got a new Notification for {eventType}");
        }
    }

    public enum Event { NEW_ITEM, SALE}

    // **** Subject/Publisher ****
    //When an event happens, the publisher goes over the subscribers and calls the method declared in their interface
    //Should contain a list of customers and subscribe, unsubscribe and notify methods
    class NotificationService
    {
        //List<EventListener> customers; //- used when there is only one event to subscribe to
        Dictionary<Event, List<EventListener>> customers;

        public NotificationService()
        {
            //customers = new List<EventListener>(); //for only one event
            customers = new Dictionary<Event, List<EventListener>>();

            foreach(string ev in Enum.GetNames(typeof(Event)))
            {
                customers.Add((Event)Enum.Parse(typeof(Event),ev), new List<EventListener>()); //add a new list of EventListener to every item in enum Event
            }
        }

        public void subscribe(Event eventType, EventListener listener)
        {
            //customers.Add(listener);
            customers[eventType].Add(listener);
        }

        public void unsubscribe(Event eventType, EventListener listener)
        {
            //customers.Remove(listener);
            customers[eventType].Remove(listener);
        }

        public void notify(Event eventType)
        {
            //foreach(var listener in customers)
            //{
            //    listener.update();
            //}

            foreach(var listener in customers[eventType])
            {
                listener.update(eventType);
            }
        }


    }

    // **** Client ****
    //Creates the publisher and the subscriber objects separately and registers the subscribers for publisher updates

    class Store
    {
        NotificationService notifyService;

        public Store()
        {
            notifyService = new NotificationService();
        }

        public void newItemPromotion()
        {
            notifyService.notify(Event.NEW_ITEM);
        }

        public void salePromotion()
        {
            notifyService.notify(Event.SALE);
        }

        public NotificationService getNotificationService()
        {
            return notifyService;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            var sub = new EmailMsgListener("email1.com");
            store.getNotificationService().subscribe(Event.NEW_ITEM,sub);
            store.getNotificationService().subscribe(Event.NEW_ITEM, new MobileAppListener("ana21"));
            store.getNotificationService().subscribe(Event.SALE, new EmailMsgListener("email2.com"));
            store.newItemPromotion();
            store.salePromotion();

            store.getNotificationService().unsubscribe(Event.NEW_ITEM, sub);
            store.newItemPromotion();

            Console.ReadKey();
        }
    }
}
