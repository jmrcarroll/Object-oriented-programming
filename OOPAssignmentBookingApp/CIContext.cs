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
using System.Data.Entity;

namespace OOPAssignmentBookingApp
{
    class CIContext : DbContext
    {
        public DbSet<CalEvent> Events { get; set; }
    }
}
