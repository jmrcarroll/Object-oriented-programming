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
    class CalExtEvent:CalEvent
    {
        public string CustID { get; set; }
        public override void EventPrint()
        {
            Console.WriteLine("Customer ID: {0}", CustID.ToString());
            base.EventPrint();
            Console.WriteLine("__________________________");
        }
        public override void EventSave()
        {
            base.EventSave();
            Console.WriteLine("External Event saved.");
        }
        public override bool EventClash(DateTime start, DateTime end, string loc, string room)
        {
            if (((this.StartTime <= start && this.EndTime> start) || (this.StartTime <= end && this.EndTime > end) || (start <= this.StartTime && end > this.StartTime) || (start<= this.EndTime && end > this.EndTime)) && this.EvLocation == loc && room == "No Room needed")
            {
                return true;
            }
            return false;
        }
        public override void EventUpdate()
        {
            string newInp;
            base.EventUpdate();
            Console.WriteLine("Enter New Customer ID (to keep the field the same, just hit enter):");
            newInp = Console.ReadLine();
            if (newInp != "")
            {
                this.CustID = newInp;
            }
        }
    }
}
