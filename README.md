![GitHub All Releases](https://img.shields.io/github/downloads/refactorsaurusrex/nodoz/total?style=for-the-badge)

# NoDoz
Ever need to temporarily prevent your Windows computer from going to sleep? 

## Background

You might have heard of [insomnia](https://dlaa.me/Insomnia/). It's super basic Windows desktop application that prevents Windows from going to sleep while it's running. It looks like it was originally written in 2009 and last updated in 2011. (Really, there's not much to maintain.) I've probably used insomnia that entire 10 year period as a way to temporarily prevent Windows from going to sleep. It's very handy, but I've always wished I could set it to run for a limited amount of time, rather than just indefinitely. 

Recently, I used the Windows Task Scheduler to launch insomnia every weekday morning at 9:30am so that I could remote desktop into my home computer as needed. When I got home at night, I closed insomnia so my computer would sleep as normal. Except, I often forgot, and my computer would run all night long. Sufficiently motivated, I created NoDoz as a 'new & improved' version of insomnia.

![NoDoz](https://raw.githubusercontent.com/refactorsaurusrex/NoDoz/master/images/screenshot.png)

For anyone wondering, [NoDoz](https://www.nodoz.com/) is a caffeine pill sold in the United States.

## Usage

> NOTE: NoDoz is intended to be run from the command line. However, you can also run the `exe` directly. If you installed using scoop, It can be found in [user]\scoop\apps\NoDoz\[version]\NoDoz.exe. If you downloaded the zip, it's wherever you put it.

- `NoDoz`: Starts NoDoz and runs indefinitely.
- `NoDoz -m`: Starts NoDoz minimized to the system tray and runs indefinitely. 
- `NoDoz -m -t 6h45m`: Starts NoDoz minimized to the system tray and runs for 6 hours and 45 minutes.

Refer to the [wiki](https://github.com/refactorsaurusrex/NoDoz/wiki) for additional documentation. 

## System Requirements

None! As of version 2.0.0, NoDoz is a self-contained .NET 5 application that consists of a single executable file. 

## Installation & Updates

### Option 1: Scoop (Recommended)

> If you've never used [scoop](https://scoop.sh/), I highly recommend checking it out. [It's like Chocolatey](https://github.com/lukesampson/scoop/wiki/Chocolatey-Comparison), but so much simpler!

1. [Install Scoop](https://github.com/lukesampson/scoop#installation).
2. Run `scoop checkup` and install any dependencies it tells you about. (I found the documentation on this point less than clear.)
3. Run `scoop install https://raw.githubusercontent.com/refactorsaurusrex/NoDoz/master/NoDoz.json`
4. Run `NoDoz` with desired parameters.
5. To update, run `scoop update NoDoz`. If a new version is available, it will be automatically downloaded and installed.

### Option 2: Download The Zip

> If you choose the zip method, keep in mind that NoDoz.exe will not be automatically put on your path.

1. Head over to the [releases page](https://github.com/refactorsaurusrex/NoDoz/releases). 
2. Download the zip for the latest version.
3. Extract to a location of your choosing.
4. Run `NoDoz.exe` directly. 
5. To update, repeat starting at step 1. :p
