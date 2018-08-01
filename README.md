# 7 days to Die Console GUI

Mod adding a GUI for 7 Days to Die Dedicated Server.

### The project suspended for some time due to lack of time for its support. Sorry gyus :(

**Currently only Windows**

## Summary

### Current features
* Log output
* Command line
* Command suggestions
* Server shutdown by button or window close

### Upcoming features
* Turning on/off message scopes (INFO, WARN, ERROR)
* Command history
* Announcements GUI (colored messages in chat by 2 clicks)
* Game text chat

### Future plans
* Application settings (changing colors or something)
* Players list, where you can:
  * Kick
  * Ban
  * Send private message
  * View players inventory
  * Change a health, stamina, gain XP and more
  
## Installation

1. Download latest [release](https://github.com/Zaklinatel/7dtd-ConsoleGUI/releases/) and copy its contents at ```<your dedicated server directory>/Mods/```. You must to get next structure:
```
<your_server_directory>/
  Mods/
    ConsoleGUI/
      ConsoleGUI.dll
      ModInfo.xml
      Resources/
        ...  
```

2. Run the server! After a couple seconds after start will be opened a GUI window.

## Build

If you are C# developer or just want to try yourself in C# coding, you can fork this repo and make some changes.

1. Clone repository to your computer
2. Go to `<project_dir>/7dtd-binaries` and check `README.txt`. There you can find list of required assemblies (```.dll```) you need to copy from ```<your_dedicated_server_dir>/7DaysToDieServer_Data/Managed/```.
3. Open solution (`.sln`) file in Visual Studio or JetBrains Rider.
4. Make changes
5. Build solution with any configuration
6. If build will success, you will find `ConsoleGUI.dll`, `ModInfo.xml` and `Resources/` dir in `<project_dir>/bin/Debug` if you build with Debug configuration, or in `<project_dir>/bin/Release` if you build with Release.

You can add more references from Managed dir and extend you possibilities to modify game. Maybe you will need to disassemble some of the .dll's and look for methods, classes or events. I usual doing it with JetBrains dotPeek.
 
## License
 
### MIT License

Copyright (c) 2017 Ivan L. Sennov @Zaklinatel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the folloswing conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
