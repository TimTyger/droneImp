## Drones

[[_TOC_]]

---

:scroll: **START**


### Introduction

There is a major new technology that is destined to be a disruptive force in the field of transportation: **the drone**. Just as the mobile phone allowed developing countries to leapfrog older technologies for personal communication, the drone has the potential to leapfrog traditional transportation infrastructure.

Useful drone functions include delivery of small items that are (urgently) needed in locations with difficult access.

---

### Task description

We have a fleet of **10 drones**. A drone is capable of carrying devices, other than cameras, and capable of delivering small loads. For our use case **the load is medications**.

A **Drone** has:
- serial number (100 characters max);
- model (Lightweight, Middleweight, Cruiserweight, Heavyweight);
- weight limit (500gr max);
- battery capacity (percentage);
- state (IDLE, LOADING, LOADED, DELIVERING, DELIVERED, RETURNING).

Each **Medication** has: 
- name (allowed only letters, numbers, ‘-‘, ‘_’);
- weight;
- code (allowed only upper case letters, underscore and numbers);
- image (picture of the medication case).

Develop a service via REST API that allows clients to communicate with the drones (i.e. **dispatch controller**). The specific communicaiton with the drone is outside the scope of this task. 

The service should allow:
- registering a drone;
- loading a drone with medication items;
- checking loaded medication items for a given drone; 
- checking available drones for loading;
- check drone battery level for a given drone;

> Feel free to make assumptions for the design approach. 

---

### Requirements

While implementing your solution **please take care of the following requirements**: 

#### Functional requirements

- There is no need for UI;
- Prevent the drone from being loaded with more weight that it can carry;
- Prevent the drone from being in LOADING state if the battery level is **below 25%**;
- Introduce a periodic task to check drones battery levels and create history/audit event log for this.

---

#### Non-functional requirements

- Input/output data must be in JSON format;
- Your project must be buildable and runnable;
- Your project must have a README file with build/run/test instructions (use DB that can be run locally, e.g. in-memory, via container);
- Required data must be preloaded in the database.
- JUnit tests are optional but advisable (if you have time);
- Advice: Show us how you work through your commit history.

---

:scroll: **END**


##### Implementation
-A base/generic controller controller, service and repository was created which contains basic functions/commands that can be inherited by other services
-A time interval was cnfigured in the appsettings (to give user ability to be able to modify intervals at which reports are generated)
-also added to the appsettings.json is a maximum weight field (for control purpose). Makes it easy to reconfigure the maximum weight any drone may be permitted to carry. This can be different for different regions based on policies, hence it can easily be reconfigured in the appsettings.

-The system consists of 5 tables 
 drone- which houses the registerd drones
 droneItems - where the items attached to a drone are located
 droneModel - where the different drone models are stored
 medication - a table where the available medications are stored
 state - the table housing the different states a drone can be in
The drone, droneModel,medication and state table have ben preloaded with data to aid the loading of drones.

-There are six(6) available endpoints
 -FetchAllDrones - to get all the drones in the system
 -FetchAvailableDrones - to get all drones that are Idle/available
 -RegisterDrone - to register new drones
 -LoadDrone - to load medications on a drone (during loading, drone state is updated to loading. If loading was successful, state is updated to loaded else it is returned to an idle state for failed loading process)
 -FetchDroneItems - to help retrieve items attached to a drone using the drone S/N
 -GetDroneBatteryLevel - to help retrieve battery level for a drone using its serial number.

To build and run solution
-pull code from repository
-restore nugget packages for solution to restore project dependencies (
    alternatively, you need to install the following nuGet packages
    -Microsoft.AspNetCore.Mvc 2.2.0
    -Microsoft.EntityFrameworkCore 6.0.11
    -Microsoft.EntityFrameworkCore.InMemory 6.0.11
    -Newtonsoft.Json 13.0.2
    -Nlog 5.1.0
    -Nlog.Web.AspNetCore 5.2.0
    -Nlog.Extensions.Logging 5.2.0
)
-build solution (swagger UI loads up to test solution endpoints).