# Lumberjack


 * Lumberjack
 * Create a web application/service that will display log information. Log information will be compiled from many sources: log entries in database(s), from email logs, apache logs, OS error logs, TSQL transaction logs, etc. The application will have filtering functionality depending on what sources it comes from. Possibly view Database logs could be filtered by value present in a certain column, email logs could be parsed and broken into ‘columns’.. It all depends on where they come from. Maybe define a base class with a few properties: LogSourceType, Body/Message, Date, 
 * Coworkers
 * Manage Log entry data from various sources with filtering capabilities  (stretch: per user account)
 * Tecnologies
  * mvc.net - framework for web interface
  * log4net - dotnet library for logging to databases
  * Kibana - Graphs and things?
   * https://github.com/elastic/kibana      
   * http://demo.elastic.co/packetbeat/#/dashboard/Packetbeat-Dashboard
  * logstash - Way to aggregate data?
   * https://www.elastic.co/products/logstash          
   * https://github.com/elastic/logstash
  * ServiceStack - More framework stuff? Not Free though..?
   * https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project
  * Etc.. (More as I come up with need..?)
 * Stretch: make each logging entitiy ( Database logs, Email logs, etc) generic; by that I mean a user could  possibly create an account and say “Add a Database logging data source”, “Add an Email Logging data source”  and be able to view all of them and continually add more. Documentation would be required for this part.. not all databases are the same, not all email logs would be configured correctly, authentication for each portal I would get information from.. could be very stretchy?

### (C) Will it be proprietary to your current employer? ###
	At work I’ve attempted to dive through their ServiceStack .net code for several tasks that I’ve been assigned.. 
	I’d love to get to know what it is/how to use it. I have constructed a very basic version of this log viewer that 
	was written completely with inline asp (Very similar to how the php work was done for 401) because that’s all I really 
	now so far. I’d love the opportunity to make the application much more generic (rather than pointing to a single 
	hard-coded table in a database) and really have this application usable to the company. I know that our Customer Service 
	reps would love to be able to parse email logs to answer customer questions as to why their alert didn’t go out. 
	Developers want to know specific questions that are being logged to a file and/or database but are tired of custom tailoring 
	SQL queries to answer their questions. Having a web application that they could log into and use UI to filter through would 
	“Be very useful”.
	
	Eventually I’ll be integrating what I come up with for this project into what we use at work, specifically for the databases/email 
	logs that AlertSense uses.. But until then I will be hosting all of my source code publicly with the intent that, hopefully, 
	it could be used by anyone that wants to aggregate log data for their applications/databases and filter through it. 
