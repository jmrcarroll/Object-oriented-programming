/*
 * John Carroll 1484479
 * 02/7/2020
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOPAssignmentBookingApp
{
    abstract class CalEvent: ICalEventPrint, ICalEventSave, ICalEventClash, ICalEventUpdate
    {
        public int ID { get; set; }
        public string EvDesc { get; set; }
        public string EvLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime{ get; set; }
        public virtual void EventPrint()
        {
            TimeSpan duration = EndTime - StartTime;
            Console.WriteLine("Event ID: {0}\nDescription: {1}\nEvent Location: {5}\nStart Time: {2}\nEnd Time: {3}\nDuration: {4}",ID, EvDesc, StartTime, EndTime,duration,EvLocation);
        }
        public virtual void EventSave()
        {
            Console.WriteLine("Saving...");
        }
        public abstract bool EventClash(DateTime start, DateTime end, string loc, string room);

        public virtual void EventUpdate()
        {
            Console.WriteLine("Current Details: ");
            this.EventPrint();

            Console.WriteLine("===================================================================");
            Console.WriteLine("Enter New Description (to keep the field the same, just hit enter):");
            string newInp = Console.ReadLine();
            if (newInp != "")
            {
                this.EvDesc = newInp;
            }
            Console.WriteLine("Enter New Location (to keep the field the same, just hit enter):");
            newInp = Console.ReadLine();
            if (newInp != "")
            {
                this.EvLocation = newInp;
            }
            DateTime newdate;
            bool CorInp = false;
            Console.WriteLine("Start time and date (in format dd/MM/yyyy HH:mm): ");
            Console.WriteLine("(to keep the field the same, just hit enter)");

            do
            {
                newInp = Console.ReadLine();
                if (DateTime.TryParse(newInp, out newdate))
                {
                    this.StartTime = newdate;
                    CorInp = true;
                }
                else if (newInp == "")
                {
                    CorInp = true;
                }
                else
                {
                    Console.WriteLine("Invalid date.");
                }
            } while (!CorInp);
            
            CorInp = false;
            Console.WriteLine("End time and date (dd/MM/yyyy HH:mm): ");
            Console.WriteLine("(to keep the field the same, just hit enter)");
            do
            {
                newInp = Console.ReadLine();
                if (DateTime.TryParse(newInp, out newdate))
                {
                    this.EndTime = newdate;
                    CorInp = true;
                }
                else if (newInp == "")
                {
                    CorInp = true;
                }
                else
                {
                    Console.WriteLine("Invalid date.");
                }
            } while (!CorInp);



        }
    }
}
