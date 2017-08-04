#Age Ranger
AgeRanger is a world leading application designed to identify person's age group!

##Features
###The following features have been implemented
 - Allows user to add a new person - every person has the first name, last name, and age;
 - Displays a list of people in the DB with their First and Last names, age and their age group. The age group should be determened based on the AgeGroup DB table - a person belongs to the age group where person's age >= 
 than group's MinAge and < than group's MaxAge. Please note that MinAge and MaxAge can be null;
 - Allows user to search for a person by his/her first or last name and displays all relevant information for the person - first and last names, age.
 - Allow user to edit existing person records and expose a WEB API. Use PUT action for API/Person controller
###Running the application
 - Just run the application using visual studio / IISExpress or deploy it to IIS Website.
##Third Party Tools
- Log4Net
- Autofac5
- Moq
- Sinonjs
- Newtonsoft.Json
- ASP.NET WebApi
- ASP.NET MVC
- MSTestExtension

##Unit Testing
###Front End Tesitng
- access the /Test controller to access the unit test page which is using QUnit

###Back End Testing using MSTest
- run MSTest using Visual Studio
