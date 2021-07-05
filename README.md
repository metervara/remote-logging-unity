# remote-logging-unity

Basic starting point for remote logging in Unity. Abstract logger can be extended to support new services. Supports external config files for logger settings.

**Current implementations:**
- FileLogger.cs (logs to local files)
- LogglyLogger.cs (logs to https://www.loggly.com)


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
