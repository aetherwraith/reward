#Reward Insight QA Challenge

##Assumptions

Due to the limited amount of information given the following assumptions have been made:

+ Frontend has consistent and sensible IDs for elements.
+ Frontend is in plain HTML.
+ Reviews are presented in a simple table.
+ Simple username/password authentication.
+ Server side has a RESTful interface which the frontend interfaces with.
+ Unable or unnecessary to directly access data store (this can be achieved with Entity Framework if necessary).

## Technology and Design

### Restful interface

RestSharp has been chosen for this as it provides a robust and well-tested method for communicating with RESTful
 interfaces.

RestSharp allows the JSON to be modelled and serialized/deserialized easily enabling the repid development of tests.

### Logging and reporting

A common library has been provided that provides basic logging and reporting facilities.

Common.Logging is being used as it allows a choice of the actual logging implementation (here log4net).

Extent reports has been chosen as a "logging reporter" meaning it is easy to use alongside the logger and is familiar 
for anyone used to writing logging statements.

### Frontend

A basic framework around Selenium has been provided. It adds robusness by utilising the Selenium ExpectedConditions and 
WebDriverWait methods to ensure web elements are present and in the correct state before interacting with them.

This sits behind a DriverProvider which stores the configuration of the browser being used to test and provides
convenience methods for starting and stopping the browser.

### Configuration

Configuration is handled via the app.config file. Parameters include the desired browser (although this can be 
overridden by setting it directly on a new DriverProvider instance). Other parameters include proxy settings and whether
to use a remote browser instance (for instance in a Selenium Grid configuration).

