namespace Assignment_1.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
   

    public class EventModel : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Assignment_1.Models.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public EventModel()
            : base("name=Model11")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Event> EventList { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string OwnerId { get; set; }
        public int Date { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public int SeatCount { get; set; }        
        public int MaxSeatCount { get; set; }

        public Double Price { get; set; }


    }
}