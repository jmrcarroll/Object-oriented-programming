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
    using System.Threading.Tasks;

    namespace OOPAssignmentBookingApp
    {
        class CalIntEvent : CalEvent
        {
            public string RoomID { get; set; }
            public override void EventPrint()
            {
                Console.WriteLine("Room ID: {0}", RoomID.ToString());
                base.EventPrint();
                Console.WriteLine("_______________");

            }
            public override void EventSave()
            {
                base.EventSave();
                Console.WriteLine("Interal Event Saved.");
            }
            public override bool EventClash(DateTime start, DateTime end, string loc, string room)
            {
                if (((this.StartTime <= start && this.EndTime > start) || (this.StartTime <= end && this.EndTime > end) || (start <= this.StartTime && end > this.StartTime) || (start <= this.EndTime && end > this.EndTime)) && this.RoomID == room) 
                {
                    return true;
                }
                return false;
            }

            public override void EventUpdate()
            {
                string newInp;
                base.EventUpdate();
                Console.WriteLine("Enter New Room ID (to keep the field the same, just hit enter):");
                newInp = Console.ReadLine();
                if (newInp != "")
                {
                    this.RoomID = newInp;
                }
            }
        }
    }

