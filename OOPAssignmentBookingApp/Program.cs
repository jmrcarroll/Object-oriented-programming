/*
 * John Carroll 1484479
 * 02/7/2020
 * 
 * 
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace OOPAssignmentBookingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new CIContext();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
                string menu = "#########################################\n\nCOLCHESTER INSTITUTE ROOM BOOKING SERVICE\n\n#########################################\n\nENTER THE NUMBERICAL VALUE OF YOUR OPTION.\n";
                menu += "1. List events\n";
                menu += "2. Add Event\n";
                menu += "3. Update Event\n";
                menu += "4. Delete Event\n";
                menu += "0. Exit\n\n";

                int inp;
                bool CorInp = false;
            using (db)
            {
                do
                {
                    Console.Clear();
                    Console.Title = "CI Room Booking";
                    Console.WriteLine(menu);

                    do
                    {
                        try
                        {
                            inp = Convert.ToInt32(Console.ReadLine());
                            CorInp = true;
                        }
                        catch (Exception)
                        {
                            CorInp = true;
                            inp = 1000;
                        }
                    } while (!CorInp);
                    CorInp = false;
                    switch (inp)
                    {
                        case 0: Console.WriteLine("Goodbye"); break;
                        case 1: SeeEvents(db); break;
                        case 2: CreateEvent(db); break;
                        case 3: UpdateEvent(db); break;
                        case 4: DelEvent(db); break;
                        default: Console.WriteLine("Invalid Input.\n\nPress any key to continue."); Console.ReadLine(); break;
                    }

                } while (inp != 0);
                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
            }
            
        }

        private static void SeeEvents(CIContext db)
        {
            int inp;
            bool colInp = false;
            Console.WriteLine("Wht events do you want to see?\n(1) All\t(2) Internal\t(3) External");
            do
            {
                try
                {
                    inp = Convert.ToInt32(Console.ReadLine());
                    colInp = true;
                    switch (inp)
                    {
                        case 1: ListEvent('A', db); break;
                        case 2: ListEvent('I', db); break;
                        case 3: ListEvent('E', db); break;
                        default: Console.WriteLine("Invalid value"); colInp = false; break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input.");
                }
            } while (!colInp);
            
            
        }
        private static void ListEvent(char type, CIContext db)
        {
            Console.Clear();
            Console.Title = "Event List";
            var Query = from e in db.Events orderby e.StartTime select e;
            if (type == 'A')    
            {
                Console.WriteLine("Printing all Events");
                foreach (var item in Query)
                {
                    item.EventPrint();
                }
            } else if (type == 'E'){
                Console.WriteLine("Print All External Events");
                foreach (var item in Query)
                {
                    Type EventType = item.GetType();
                    if (EventType.Equals(typeof(CalExtEvent)))
                    {
                        item.EventPrint();
                    }
                    
                }
            } else if (type == 'I'){
                Console.WriteLine("Print All Internal Events");
                foreach (var item in Query)
                {
                    Type EventType = item.GetType();
                    if (EventType.Equals(typeof(CalIntEvent)))
                    {
                        item.EventPrint();
                    }

                }
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }
        private static void CreateEvent(CIContext db)
        {
            Console.Title = "Create New Event";
            Console.Clear();
            Console.WriteLine("Please input the type of Event:\n1. Internal\n2. External");
            bool colInp = false;
            int inp = 0;
            do
            {
                do
                {
                    try
                    {
                        inp = Convert.ToInt32(Console.ReadLine());
                        colInp = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Input.");
                    }
                } while (!colInp);
                if (inp == 1)
                {
                    //Console.WriteLine("Create Internal Event");
                    Console.WriteLine("Room Number: ");
                    string RoomNum = Console.ReadLine();
                    Console.WriteLine("Event Description: ");
                    string Desc = Console.ReadLine();
                    Console.WriteLine("Location: ");
                    string Location = Console.ReadLine();
                    string Format = "dd MM yyyy HH:mm";
                    DateTime start;
                    DateTime end;
                    bool CorInp = false;
                    Console.WriteLine("Start time and date (in format dd/MM/yyyy HH:mm): ");
                    do
                    {
                        if (DateTime.TryParse(Console.ReadLine(), out start))
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
                    do
                    {
                        if (DateTime.TryParse(Console.ReadLine(), out end))
                        {
                            CorInp = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date.");
                        }
                    } while (!CorInp);
                    TimeSpan duration = end - start;
                    string DurString = duration.ToString();
                    if (!DurString.Contains("-"))
                    {
                        CalEvent NewEv = new CalIntEvent { EvDesc = Desc, RoomID = RoomNum, EvLocation = Location, EndTime = end, StartTime = start };
                        if (!Clashed(db, NewEv, RoomNum))
                        {
                            db.Events.Add(NewEv);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateException)
                            {
                                Console.WriteLine("Unable to update database.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Event clashes with an already existing event, quitting to main menu");
                        }

                    }
                    else
                    {
                        Console.WriteLine("End time before start time, Quitting to main menu");
                    }



                }
                else if (inp == 2)
                {
                    //Console.WriteLine("Create External Event");
                    Console.WriteLine("Customer ID: ");
                    string customer = Console.ReadLine();
                    Console.WriteLine("Event Description: ");
                    string Desc = Console.ReadLine();
                    Console.WriteLine("Location: ");
                    string Location = Console.ReadLine();
                    string Format = "dd MM yyyy HH:mm";
                    DateTime start;
                    DateTime end;
                    bool CorInp = false;
                    Console.WriteLine("Start time and date (dd/MM/yyyy HH:mm): ");
                    do
                    {
                        if (DateTime.TryParse(Console.ReadLine(), out start))
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
                    do
                    {
                        if (DateTime.TryParse(Console.ReadLine(), out end))
                        {
                            CorInp = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date.");
                        }
                    } while (!CorInp);
                    TimeSpan duration = end-start;
                    string DurString = duration.ToString();
                    if (!DurString.Contains("-"))
                    {
                        CalEvent NewEv = new CalExtEvent { EvDesc = Desc, CustID = customer, EvLocation = Location, EndTime = end, StartTime = start };
                        //if (eventClash(db, start, end, NewEv)) { }

                        if (!Clashed(db, NewEv, "No Room needed"))
                        {
                            db.Events.Add(NewEv);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateException)
                            {
                                Console.WriteLine("Unable to update database.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Event clashes with an already existing event, quitting to main menu");
                        }
                    }
                    else
                    {
                        Console.WriteLine("End time before start time, quitting to main menu.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            } while (!colInp);
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }
        private static void UpdateEvent(CIContext db)
        {
            Console.Clear();
            Console.Title = "Update Event";
            Console.WriteLine("Enter EventID to update:");

            bool corInp = false;
            int ID = 0;
            do
            {
                try
                {
                    ID = Convert.ToInt32(Console.ReadLine());
                    corInp = true;
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid input. please input an interger value");
                }
            } while (!corInp);
            var record = db.Events.Find(ID);
            if (record != null)
            {
                record.EventUpdate();
                db.Events.AddOrUpdate(record);
                db.SaveChanges();

            }
            else
            {
                Console.WriteLine("No event found");
            }
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        static bool Clashed(CIContext db,CalEvent newEvent, string room)
        {
            int counter = 0;
            var query = from e in db.Events orderby e.ID select e;
            
            foreach(var row in query)
            {
                if(row.EventClash(newEvent.StartTime,newEvent.EndTime, newEvent.EvLocation, room))
                {
                    counter++;
                }               
            }
            if (counter > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void DelEvent(CIContext db)
        {
            Console.Clear();
            Console.Title = "Delete Event";
            Console.WriteLine("Enter EventID of event to delete:");
            bool corInp = false;
            int inp = 0 ;
            do
            {
                try
                {
                    inp = Convert.ToInt32(Console.ReadLine());
                    corInp = true;
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid input. please input an interger value");
                }
            } while (!corInp);

            var row = db.Events.Find(inp);
            if(row != null)
            {
                db.Events.Remove(row);
                db.SaveChanges();
                Console.WriteLine("Event with ID of {0} deleted", inp);
            }
            else
            {
                Console.WriteLine("Event does not exist");
            }
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

    }
}
