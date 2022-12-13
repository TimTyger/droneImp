using ddrone_DataAccess;
using drone_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_DataAccess
{
    public class AppSeedClass{
            public static void SeedData(ApplicationContext context)
    {
        if (!context.Models.Any())
        {
            var models = new List<DroneModel>
                {
                    new DroneModel
                    {
                        Id = 1,Model="LightWeight",MaxWeight=100,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new DroneModel
                    {
                        Id = 2,Model="MiddleWeight",MaxWeight=250,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new DroneModel
                    {
                        Id = 3,Model="CruiserWeight",MaxWeight=350,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new DroneModel
                    {
                        Id = 4,Model="HeavyWeight",MaxWeight=500,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    }

                };
            context.Models.AddRange(models);
            context.SaveChanges();

        };
        if (!context.States.Any())
        {
            var states = new List<State>
                {
                    new State
                    {
                        Id = 1,Value="Idle"
                    },
                    new State
                    {
                        Id = 2,Value="Loading"
                    },
                    new State
                    {
                        Id = 3,Value="Loaded"
                    },
                    new State
                    {
                        Id = 4,Value="Delivering"
                    },
                    new State
                    {
                        Id = 5,Value="Delivered"
                    },
                    new State
                    {
                        Id = 6,Value="Returning"
                    }

                };
            context.States.AddRange(states);
            context.SaveChanges();
        };
            if (!context.Drones.Any())
            {
                var drones = new List<Drone>
                {
                    new Drone
                    {
                        Id = 1,SerialNumber="A1234567890",ModelId=1,BatteryLevel=100,StateId=1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new Drone
                    {
                        Id = 2,SerialNumber="A1234567891",ModelId=2,BatteryLevel=100,StateId=1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new Drone
                    {
                        Id = 3,SerialNumber="A1234567892",ModelId=3,BatteryLevel=100,StateId=1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new Drone
                    {
                        Id = 4,SerialNumber="A1234567893",ModelId=4,BatteryLevel=100,StateId=1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },
                    new Drone
                    {
                        Id = 5,SerialNumber="A123456789B",ModelId=1,BatteryLevel=100,StateId=1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now
                    },

                };
                context.Drones.AddRange(drones);
                context.SaveChanges();
            };
            if (!context.Medications.Any())
            {
                var medications = new List<Medication>
                {
                    new Medication
                    {
                        Id = 1,CreatedBy="SYSTEM",CreatedAt=DateTime.Now,Name="HeavyItem",Code="HV_999",Weight=400,Image="iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
                    },new Medication
                    {
                        Id = 2,Name="Paracetamol",CreatedBy="SYSTEM",CreatedAt=DateTime.Now,Code="50PCM",Weight=50,Image="iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
                    },
                    new Medication
                    {
                        Id = 3,Name="TestItem",CreatedBy="SYSTEM",CreatedAt=DateTime.Now,Code="ITEM2",Weight=1700,Image="iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
                    },
                    new Medication
                    {
                        Id = 4,Name="Test-Item2",CreatedBy="SYSTEM",CreatedAt=DateTime.Now,Code="TEST250",Weight=250,Image="iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
                    },
                    new Medication
                    {
                        Id = 5,Name="Test_Item3",CreatedBy="SYSTEM",CreatedAt=DateTime.Now,Code="TST_3",Weight=40,Image="iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
                    },

                };
                context.Medications.AddRange(medications);
                context.SaveChanges();
            }
        }

}

}
