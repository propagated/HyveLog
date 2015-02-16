# HyveLog  [![Build Status](https://travis-ci.org/propagated/HyveLog.svg?branch=master)](https://travis-ci.org/propagated/HyveLog)

A lightweight logging library designed for .net applications that run in both Console and Windows Service modes.

## Features
* Togglable logging mode-- to console, text file or both, all with one method.
* Automatically determines if calling executable is running as a Console Application or Windows Serivce and logs appropriately.

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


### More to Come 
See issues for additional todos and planned future enhancements. The project uses NUnit for unit test coverage, and is currently being tested on travis-ci.org in a mono environment.



License: MIT License
