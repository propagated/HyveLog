# HyveLog #

A custom lightweight logging library designed for Hybrid Console/Service applications written in .net.

## Features
* Logging modes to console, text file or both with one method.
* Automatic determination if calling executable is running as a service or not, if logging location is not specified.

## Usage
```csharp
//automatically determine logging mode
var logger = new Logger();
//change to write to both the console and a file
logger.LoggingMode = LogTarget.Both;
//you can specify a filepath, default is to %userfolder%\appdata\Errors\errorlog.txt
logger.LogFileFullPath = @"c:\errorLog.txt";
//or a relative path
logger.LogFileFullPath = @"errors\errorLog.txt";
//finally, log your message!
logger.Log("Pod Seven Critical");
```
## License: MIT License

### More to Come
Issues are tracking todos and future enhancements! The project uses NUnit for unit test coverage.

