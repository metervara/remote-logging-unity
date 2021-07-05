# remote-logging-unity

Basic starting point for remote logging in Unity. Abstract logger can be extended to support new services. Supports external config files for logger settings.

**Current implementations:**
- FileLogger.cs (logs to local files)
- LogglyLogger.cs (logs to https://www.loggly.com)

## Overview
To implement a new logger inherit from LogInterceptionBase (See FileLogger/LogglyLogger for examples) and override ```protected abstract void Log(LogItem msg);```. The base class handles configuration loading, ready state and log queue. You might also want to create a new Settings format for your new logger.

## Improvements
- Add support for JSON formatted log messages, so we can send more detailed information to log services.
- Generic settings format so we don't need a new one for each logger

## Usage
To include in a Unity project, in the package manager select 'Add package from git URL' and enter 
```https://github.com/metervara/remote-logging-unity.git#release```

Or manually add the following to your manifest.json file, under dependencies:

```json
"metervara.logging": "https://github.com/metervara/remote-logging-unity.git#release"
```


## Development

To deploy the package subtree as a package run the following command:

```bash
make deploy
```
