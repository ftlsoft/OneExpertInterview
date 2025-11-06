### Project overview
oneExpert Developer NET interview project


### How to run
Windows:

Debug:  

**& "OneExpert Interview\OneExpertInterview\bin\Debug\net8.0\OneExpertInterview.exe"**
  
Release:  

**& "OneExpert Interview\OneExpertInterview\bin\Release\net8.0\OneExpertInterview.exe"**
  
Dll directly:  

**dotnet "OneExpert Interview\OneExpertInterview\bin\Debug\net8.0\OneExpertInterview.dll"**

docker:
docker build -t oneexpert_orderapp -f OneExpertInterview/Dockerfile .
docker run --rm -it oneexpert_orderapp


### Architecture diagram (text or mermaid)
'''mermaid''' - I don't know, unfortunately, I won't pretend

Program.cs -> OrderService -> IOrderRepository -> OrderRepository (in-memory)

IOrderService / OrderService
- Orchestrates the main logic for processing orders.
- Validates input via IOrderValidator.
- Retrieves the order from IOrderRepository.
- Logs progress and errors using ILogger.
- On success, notifies users via INotificationService.

ILogger / ConsoleLogger
Responsible for timestamped console logging.
Reads log level (Info or Error) from appsettings.json via IConfiguration.
Skips info logs when level = Error

appsettings.json
Holds configuration data like LogLevel

 
### List of completed bonus tasks
All bonus tasks completed.