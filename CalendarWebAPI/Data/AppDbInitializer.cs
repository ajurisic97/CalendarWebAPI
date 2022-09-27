﻿using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using CalendarWebAPI.Repositories;
using System.Text.Json;

namespace CalendarWebAPI.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CalendarContext>();
                context.Database.EnsureCreated();
                // Uncomment 5 Lines before to remove all data (5 tables), start program, stop program, comment 5 lines back to reset all data
                //context.SchedulerItems.RemoveRange(context.SchedulerItems);
                //context.Schedulers.RemoveRange(context.Schedulers);
                //context.Recurrings.RemoveRange(context.Recurrings);
                //context.Events.RemoveRange(context.Events);
                //context.People.RemoveRange(context.People);
                //Recurrings:
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
                }
                context.SaveChanges(); // so events can be added
                //Events:
                var allRecurrings = context.Recurrings.Select(x => x.Id);
                if (!context.Events.Any())
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
                }
                context.SaveChanges();
                //WorkingDays
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
                            IsWorkingDay = true
                        }
                    });
                }
                //People:
                if (!context.People.Any())
                {
                    List<Person> listPeople = new List<Person>();
                    string? path = AppDomain.CurrentDomain.BaseDirectory;
                    using (StreamReader r = new StreamReader(path + "/JsonFiles/Person.json"))
                    {
                        var peopleJson = r.ReadToEnd();
                        listPeople = JsonSerializer.Deserialize<List<Person>>(peopleJson);
                    }
                    var events = context.Events.ToList();
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
                }


                // Confessions - maybe added
                if (!context.Confessions.Any())
                {
                    //FILL CONFESSIONS
                }
                //Holidays to be added
                if (!context.Holidays.Any())
                {
                    // FILL holidays
                }

                // CALENDAR + CALENDARITEMS
                if (!context.Calendars.Any())
                {
                    List<Calendar> calendars = new List<Calendar>();
                    List<CalendarItem> dbCalendarItems = new List<CalendarItem>();
                    Guid creatorId = Guid.Parse("ba4875f8-181e-41fb-ab72-3bc67c251ac7"); //Creator: application
                    Creator creator = context.Creators.Where(x => x.Id == creatorId).FirstOrDefault();
                    Models.Creator modelCreator = CreatorMapper.FromDatabase(creator);
                    CalendarItemsRepository calendarItemsRepository = new CalendarItemsRepository(context);
                    
                    //Parent calendar (for example country - Croatia)
                    Models.Calendar calMaster = new Models.Calendar(Guid.NewGuid(), null, modelCreator, 2022, "Master", new DateTime(2022, 1, 1), new DateTime(2022, 12, 31), DateTime.Now, true);
                    Calendar dbCalendar = CalendarMapper.ToDatabase(calMaster);
                    calendars.Add(dbCalendar);
                    // We fill calendarItems for parent calendar
                    List<CalendarItem> calItems = calendarItemsRepository.FillCalendarItems(calMaster);
                    var datesExisting = context.CalendarItems.Where(x=>x.CalendarId == calMaster.Id).Select(x=>x.Date).ToList();
                    var filtered = calItems.Where(x => !datesExisting.Contains(x.Date));
                    dbCalendarItems.AddRange(filtered);



                    //His child (for example State - Splitsko dalmatinska)
                    Models.Calendar calMasterChild = new Models.Calendar(Guid.NewGuid(), calMaster, modelCreator, 2022, "2. razina", new DateTime(2022, 1, 1), new DateTime(2022, 1,1), DateTime.Now, true);
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

                }


                //Shifts for test purposes - currently not in use
                if (!context.Shifts.Any())
                {
                    //context.Shifts.AddRange(new List<Shift>()
                    //{
                    //    new DbModels.Shift()
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        CalendarItemId = Guid.Parse("8657DEE3-079A-4EA1-BEA4-01861CC2411F"),
                    //        Description = "Sample data Shift 1",
                    //        StartTime = DateTime.Today,
                    //        EndTime = DateTime.Today,
                    //        ShiftType = 1,
                    //        IsActive = true

                    //    },
                    //    new DbModels.Shift()
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        CalendarItemId = Guid.Parse("8657DEE3-079A-4EA1-BEA4-01861CC2411F"),
                    //        Description = "Sample data Shift 2",
                    //        StartTime = DateTime.Today,
                    //        EndTime = DateTime.Today,
                    //        ShiftType = 2,
                    //        IsActive = true

                    //    },
                    //});
                }
                context.SaveChanges();

            }
        }
    }
}
