﻿using CalendarWebAPI.Countries;
using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using CalendarWebAPI.Repositories;
using System.Text.Json;

namespace CalendarWebAPI.Data
{
    public class AppDbInitializer
    {
        // we check every table. If some table is empty we fill it with data. Order is important as some tables are connected to each other.
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CalendarContext>();
                var dbEvents = context.Events;
                context.Database.EnsureCreated();
                /* Uncomment lines below to remove all data from database, start program, stop program, comment them back to put in default data*/
                //context.Confessions.RemoveRange(context.Confessions);
                //context.Shifts.RemoveRange(context.Shifts);
                //context.SchedulerItems.RemoveRange(context.SchedulerItems);
                //context.Schedulers.RemoveRange(context.Schedulers);
                //context.ApplicationEvents.RemoveRange(context.ApplicationEvents);
                //context.Recurrings.RemoveRange(context.Recurrings);
                //context.Events.RemoveRange(context.Events);
                //context.Applications.RemoveRange(context.Applications);
                //context.UserRoles.RemoveRange(context.UserRoles);
                //context.Users.RemoveRange(context.Users);
                //context.People.RemoveRange(context.People);
                //context.Holidays.RemoveRange(context.Holidays);
                //context.WorkingDays.RemoveRange(context.WorkingDays);
                //context.CalendarItems.RemoveRange(context.CalendarItems);
                //context.Calendar.RemoveRange(context.Calendar);
                //context.Creators.RemoveRange(context.Creators);
                context.SaveChanges();

                //Creators - data seed - we only add 1 creator (Application) for now
                if (!context.Creators.Any())
                {
                    Creator c = new Creator
                    {
                        Id = new Guid(),
                        Name = "Application"

                    };
                    context.Creators.Add(c);
                    context.SaveChanges();
                }
                //Recurrings - data seed
                if (!context.Recurrings.Any())
                {
                    context.Recurrings.AddRange(new List<Recurring>()
                    {
                        new Recurring
                        {
                            Id = Guid.NewGuid(),
                            RecurringType="None",
                            Separation=0,
                            Gap = 0
                        },
                        new Recurring
                        {
                            Id = Guid.NewGuid(),
                            RecurringType="Daily",
                            Separation=0,
                            Gap = 1
                        },
                        new Recurring
                        {
                            Id = Guid.NewGuid(),
                            RecurringType="Weekly",
                            Separation=0,
                            Gap = 7
                        },
                    });
                    context.SaveChanges(); // so events can be added
                }

                //Events - data seed
                var allRecurrings = context.Recurrings.Select(x => x.Id);
                if (!dbEvents.Any())
                {
                    foreach (var recurringId in allRecurrings) //for every event we have recurring = none, daily, weekly...
                    {

                        context.Events.AddRange(new List<Event>()
                        {
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Regular",
                                    Type = 1,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Regular work",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Overtime",
                                    Type = 2,
                                    Coefficient= Decimal.Parse("1,5"),
                                    Description= "Overtime work",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Work at night",
                                    Type = 3,
                                    Coefficient= Decimal.Parse("1,5"),
                                    Description= "Work at night",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Work on Sundays",
                                    Type = 4,
                                    Coefficient= Decimal.Parse("1,5"),
                                    Description= "Work on Sundays",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Work on holidays",
                                    Type = 5,
                                    Coefficient= Decimal.Parse("1,5"),
                                    Description= "Work on holidays",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Business trip",
                                    Type = 6,
                                    Coefficient= Decimal.Parse("1,5"),
                                    Description= "Business trip hours",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Field work",
                                    Type = 7,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Field work hours",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Standby",
                                    Type = 8,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Standby hours",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "On call",
                                    Type = 9,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Working hours on call",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Vacation",
                                    Type = 10,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Hours of vacation time",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "SickDay (company)",
                                    Type = 11,
                                    Coefficient= Decimal.Parse("0,7"),
                                    Description= "Hours of sick leave at the expense of the company",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "SickDay (Fund)",
                                    Type = 12,
                                    Coefficient= Decimal.Parse("0,7"),
                                    Description= "Hours of sick leave at the expense of the Fund",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Maternity (<6 month)",
                                    Type = 13,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Maternity leave for the first 6 months",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Maternity (>6 month)",
                                    Type = 14,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Maternity leave after 6 months",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Paid vacation",
                                    Type = 15,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Hours of paid vacation",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Unpaid leave",
                                    Type = 16,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Hours of unpaid leave",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Absence",
                                    Type = 17,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Absence during the daily working time schedule, approved or not approved by the employer",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Strike",
                                    Type = 18,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Hours spent on strike",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Exclusion",
                                    Type = 19,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Exclusion from work",
                                    RecurringId= recurringId
                            },
                            new Event()
                            {
                                    Id = Guid.NewGuid(),
                                    Name = "Downtime, interruption of work, etc.",
                                    Type = 20,
                                    Coefficient= Decimal.Parse("1,0"),
                                    Description= "Hours of downtime, work stoppages, etc. That occurred through the fault of the employer or due to circumstances for which the employee is not responsible",
                                    RecurringId= recurringId
                            }
                        });
                    }
                    context.SaveChanges();
                }

                //WorkingDays - data seed
                if (!context.WorkingDays.Any())
                {
                    context.WorkingDays.AddRange(new List<WorkingDay>()
                    {
                        new WorkingDay()
                        {
                            Name="Monday",
                            IsWorkingDay = true
                        },
                        new WorkingDay()
                        {
                            Name="Tuesday",
                            IsWorkingDay = true
                        },
                        new WorkingDay()
                        {
                            Name="Wednesday",
                            IsWorkingDay = true
                        },
                        new WorkingDay()
                        {
                            Name="Thursday",
                            IsWorkingDay = true
                        },
                        new WorkingDay()
                        {
                            Name="Friday",
                            IsWorkingDay = true
                        },
                        new WorkingDay()
                        {
                            Name="Saturday",
                            IsWorkingDay = false
                        },
                        new WorkingDay()
                        {
                            Name="Sunday",
                            IsWorkingDay = false
                        }
                    });
                    context.SaveChanges();
                }
                //People - data seed
                if (!context.People.Any())
                {
                    List<Person> listPeople = new List<Person>();
                    string? path = AppDomain.CurrentDomain.BaseDirectory;
                    using (StreamReader r = new StreamReader(path + "/JsonFiles/Person.json"))
                    {
                        var peopleJson = r.ReadToEnd();
                        listPeople = JsonSerializer.Deserialize<List<Person>>(peopleJson);
                    }
                    var events = dbEvents.ToList();
                    List<Scheduler> schedulers = new List<Scheduler>();
                    foreach (var e in events)
                    {
                        foreach (var p in listPeople)
                        {
                            Scheduler sched = new Scheduler()
                            {
                                Id = new Guid(),
                                EventId = e.Id,
                                PersonId = p.Id
                            };
                            schedulers.Add(sched);
                        }
                    }
                    context.People.AddRange(listPeople);
                    context.Schedulers.AddRange(schedulers);
                    context.SaveChanges();
                }


                // Confessions - data seed
                if (!context.Confessions.Any())
                {
                    context.Confessions.AddRange(new List<Confession>()
                    {
                        new Confession
                        {
                            Id = Guid.Parse("B828F696-64BD-4034-83DD-8589638EFA36"),
                            Name="Kršćannska",
                            Description="Prema Julijanskom kalendaru",
                            IsDefault=true,
                            IsActive=true,
                        },
                        new Confession
                        {
                            Id = Guid.NewGuid(),
                            Name="Muslimanska",
                            Description=null,
                            IsDefault=false,
                            IsActive=true,
                        },
                        new Confession
                        {
                            Id = Guid.NewGuid(),
                            Name="Židovska",
                            Description=null,
                            IsDefault=false,
                            IsActive=true,
                        },
                        new Confession
                        {
                            Id = Guid.NewGuid(),
                            Name="Kršćannska",
                            Description="Prema Gregorijanskom kalendaru",
                            IsDefault=false,
                            IsActive=true,
                        },
                    });
                    context.SaveChanges();
                }
                //We add croatian holidays to table:
                if (!context.Holidays.Any())
                {
                    CroatianPublicHoliday croatianPublicHolidays = new();
                    //IList<BaseClass.Holiday> holidays = croatianPublicHolidays.PublicHolidaysInformation(2022);
                    IDictionary<DateTime, string> _croHolidays = croatianPublicHolidays.PublicHolidayNames(2022);
                    List<Holiday> holidays = new List<Holiday>();
                    foreach (var holiday in _croHolidays)
                    {
                        DateTime holidayDate = holiday.Key;
                        string holidayName = holiday.Value;
                        string strippedName = holiday.Value;
                        if (strippedName.Length > 50)
                        {
                            strippedName = holidayName.Substring(0, 50);
                        }
                        Holiday h = new Holiday()
                        {
                            Id = Guid.NewGuid(),
                            Name = strippedName,
                            Description = holidayName,
                            DateDay = holidayDate.Day.ToString(),
                            DateMonth = holidayDate.Month.ToString(),
                            IsCommon = true,
                            IsPermanent = true,
                            ConfessionId = Guid.Parse("B828F696-64BD-4034-83DD-8589638EFA36")

                        };
                        holidays.Add(h);
                    }
                    context.Holidays.AddRange(holidays);
                    context.SaveChanges();
                }

                // CALENDAR + CALENDARITEMS - data seed
                if (!context.Calendars.Any())
                {
                    List<Calendar> calendars = new List<Calendar>();
                    List<CalendarItem> dbCalendarItems = new List<CalendarItem>();
                    Creator creator = context.Creators.Where(x => x.Name == "Application").FirstOrDefault();
                    Models.Creator modelCreator = CreatorMapper.FromDatabase(creator);
                    CalendarItemsRepository calendarItemsRepository = new CalendarItemsRepository(context);

                    //Parent calendar (for example country - Croatia)
                    Models.Calendar calMaster = new Models.Calendar(Guid.NewGuid(), null, modelCreator, 2022, "Master", new DateTime(2022, 1, 1), new DateTime(2022, 12, 31), DateTime.Now, true);
                    Calendar dbCalendar = CalendarMapper.ToDatabase(calMaster);
                    calendars.Add(dbCalendar);
                    // We fill calendarItems for parent calendar
                    List<CalendarItem> calItems = calendarItemsRepository.FillCalendarItems(calMaster);
                    var datesExisting = context.CalendarItems.Where(x => x.CalendarId == calMaster.Id).Select(x => x.Date).ToList();
                    var filtered = calItems.Where(x => !datesExisting.Contains(x.Date));
                    dbCalendarItems.AddRange(filtered);



                    //His child (for example State - Splitsko dalmatinska)
                    Models.Calendar calMasterChild = new Models.Calendar(Guid.NewGuid(), calMaster, modelCreator, 2022, "2. razina", new DateTime(2022, 1, 1), new DateTime(2022, 1, 1), DateTime.Now, true);
                    Calendar dbChild = CalendarMapper.ToDatabase(calMasterChild);
                    calendars.Add(dbChild);
                    // We fill items for state:
                    List<CalendarItem> calItems2 = calendarItemsRepository.FillCalendarItems(calMasterChild);
                    var datesExisting2 = context.CalendarItems.Where(x => x.CalendarId == calMasterChild.Id).Select(x => x.Date).ToList();
                    var filtered2 = calItems2.Where(x => !datesExisting2.Contains(x.Date));
                    dbCalendarItems.AddRange(filtered2);


                    // State child (for Exaample City - Split)
                    Models.Calendar calChild = new Models.Calendar(Guid.NewGuid(), calMasterChild, modelCreator, 2022, "3. razina", new DateTime(2022, 1, 6), new DateTime(2022, 1, 6), DateTime.Now, true);
                    Calendar dbCalChild = CalendarMapper.ToDatabase(calChild);
                    calendars.Add(dbCalChild);
                    // We fill items for city:
                    List<CalendarItem> calItems3 = calendarItemsRepository.FillCalendarItems(calChild);
                    var datesExisting3 = context.CalendarItems.Where(x => x.CalendarId == calChild.Id).Select(x => x.Date).ToList();
                    var filtered3 = calItems3.Where(x => !datesExisting3.Contains(x.Date));
                    dbCalendarItems.AddRange(filtered3);

                    context.Calendars.AddRange(calendars);
                    context.CalendarItems.AddRange(dbCalendarItems);
                    context.SaveChanges(); // so we can read Id of calendarItems

                }

                //Shifts - data seed (currently not used for aynthing, sample data)
                if (!context.Shifts.Any())
                {
                    List<Shift> shifts = new List<Shift>();
                    foreach (var item in context.CalendarItems)
                    {
                        Shift s = new Shift
                        {
                            Id = Guid.NewGuid(),
                            CalendarItemId = item.Id,
                            Description = "Shift 1",
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(16, 0, 0),
                            ShiftType = 1,
                            IsActive = true,
                        };
                        shifts.Add(s);
                    }
                    context.AddRange(shifts);
                    context.SaveChanges();

                }

                // Applications - data seed - since we only need "Scheduler" we only add this to it
                if (!context.Applications.Any())
                {
                    context.Applications.AddRange(new List<Application>()
                    {
                        new Application()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Scheduler",
                            ShortName="SCHED"
                        },
                        new Application()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PayRoll",
                            ShortName="PR"
                        }
                    });
                    context.SaveChanges();
                }

                // ApplicationEvents - data seed
                var dbEventsIds = dbEvents.Select(x => x.Id).ToList();

                if (!context.ApplicationEvents.Any())
                {
                    List<ApplicationEvent> appEvents = new List<ApplicationEvent>();
                    var apps = context.Applications;
                    foreach (var a in apps)
                    {
                        foreach (var e in dbEventsIds)
                        {
                            appEvents.Add(new ApplicationEvent() { Id = Guid.NewGuid(), ApplicationId = a.Id, EventId = e });
                        }
                    }
                    context.ApplicationEvents.AddRange(appEvents);
                    context.SaveChanges();
                }
                // Roles -data seed - Sample data (only Roles are Admin and User)
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>() {
                        new Role()
                        {
                            Id = Guid.NewGuid(),
                            Name="Superadmin"
                        },
                        new Role()
                        {
                            Id = Guid.NewGuid(),
                            Name="Admin"
                        },
                        new Role()
                        {
                            Id = Guid.NewGuid(),
                            Name="User"
                        }
                    });
                    context.SaveChanges();
                }
                //Users - data seed
                if (!context.Users.Any())
                {
                    var hashing = new Helper.HashingManager();
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Id = Guid.NewGuid(),
                            Username="Superadmin",
                            Password=hashing.HashToString("Superadmin"),
                            Email="superadmin@softly.hr",
                            PersonId=null,
                        },
                        new User()
                        {
                            Id = Guid.NewGuid(),
                            Username="Admin",
                            Password=hashing.HashToString("Admin"),
                            Email="admin@softly.hr",
                            PersonId=context.People.Where(x=>x.FirstName=="Ivica" && x.LastName=="Andrun").Select(x=>x.Id).FirstOrDefault(),
                        },
                        new User()
                        {
                            Id = Guid.NewGuid(),
                            Username="ajurisic",
                            Password=hashing.HashToString("ajurisic"),
                            Email="ajurisic@pmfst.hr",
                            PersonId=context.People.Where(x=>x.FirstName=="Andelo" && x.LastName=="Jurisic").Select(x=>x.Id).FirstOrDefault(),
                        }
                    });
                    context.SaveChanges();
                }

                // UserRoles - data seed
                if (!context.UserRoles.Any())
                {
                    // just for test purposes and easier recognition admin username is "Admin" and PersonId = (FirstName=Ivica, LastName = Andrun)
                    // later we check if User permission is "Admin" or "User"
                    context.UserRoles.AddRange(new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = Guid.NewGuid(),
                            UserId = context.Users.Where(x=>x.Username=="Superadmin").Select(x=>x.Id).FirstOrDefault(),
                            RoleId = context.Roles.Where(x=>x.Name=="Superadmin").Select(x=>x.Id).FirstOrDefault()
                        },
                        new UserRole()
                        {
                            Id = Guid.NewGuid(),
                            UserId = context.Users.Where(x=>x.Username=="Admin").Select(x=>x.Id).FirstOrDefault(),
                            RoleId = context.Roles.Where(x=>x.Name=="Admin").Select(x=>x.Id).FirstOrDefault()
                        },
                        new UserRole()
                        {
                            Id = Guid.NewGuid(),
                            UserId = context.Users.Where(x=>x.Username=="ajurisic").Select(x=>x.Id).FirstOrDefault(),
                            RoleId = context.Roles.Where(x=>x.Name=="User").Select(x=>x.Id).FirstOrDefault()
                        }
                    });
                    context.SaveChanges();
                }






            }
        }
    }
}