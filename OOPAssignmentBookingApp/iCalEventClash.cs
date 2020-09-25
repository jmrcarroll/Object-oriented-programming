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
    interface ICalEventClash
    {
        bool EventClash(DateTime start, DateTime end, string loc, string room);
    }
}
