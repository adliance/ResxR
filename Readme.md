# ResxR

[![Build Status](https://dev.azure.com/adliance/Open%20Source%20Projects/_apis/build/status%2FResxR?branchName=main)](https://dev.azure.com/adliance/Open%20Source%20Projects/_build/latest?definitionId=195&branchName=main)
[![NuGet](https://img.shields.io/nuget/v/Adliance.ResxR.svg)](https://www.nuget.org/packages/Adliance.ResxR/)

Originally forked from https://github.com/tomwadley/resx-diff as of version published on 20 Jan 2013, then modified and upgraded tech-stack.

Helps you deal with your growing number of .NET Resource files by quickly showing you the differences and similarities between them as well as performing simple operations on them.

## Installing

```
dotnet tool install -g Adliance.ResxR --no-cache --ignore-failed-sources -v q
```

Or build it yourself (scroll down for build instructions) and install it via the `install.cmd` file on root level of this repository.
The install file packs the project as tool and installs ist locally as global tool.

## Usage

`$ ResxR --help`

```
ResxR 1.0.2
(c) Adliance GmbH 2024
Displays information about .resx files, shows differences between .resx files and performs operations on .resx files
Usage: ResxR [OPTION]... [FILE]
Usage: ResxR [OPTION]... [FILE1] [FILE2]
Usage: ResxR [OPTION]... [FILE]...

  -m, --missing-keys                 Finds keys present in the first file which are missing in the second

  -p, --present-keys                 Finds keys that are present in both the first and the second file

  -d, --different-values             Finds keys present in both files whos values differ

  -i, --identical-values             Finds keys present in both files with identical values

  -s, --mismatched-metadata          Finds keys present in both files which have differing metadata (type, mimetype, space or comment)

  -u, --duplicate-keys               Finds keys that appear more than once

  -e, --missing-spacepreserve        Finds keys that are missing the xml:space="preserve" attribute

  -c, --copy-missing-keys            Copies missing keys from the first file to the second

  -v, --copy-different-values        Copies differing values from the first file to the second

  -a, --alphabetise                  Sorts keys into alphabetical order

  -r, --add-missing-spacepreserve    Adds xml:space="preserve" attributes to keys that don't have it

  -f, --full-data                    Shows all fields from the data elements

  --help                             Display this help screen.

  --version                          Display version information.

  files (pos. 2)
```

## Build instructions

Dependencies are installed via NuGet. Package restore has been enabled for this solution. That means that in order to build, you have to be running a recent version of NuGet and have "Allow NuGet to download missing packages during build" enabled in the settings as shown [here](http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages).

Note: if you having trouble upgrading NuGet, see [this](http://docs.nuget.org/docs/reference/known-issues#Upgrading_to_latest_NuGet_from_an_older_version_causes_a_signature_verification_error.)

After the dependencies have been downloaded by NuGet, it should build. Build it using the [build.cmd](build.cmd) file.
Run the exe in place or install as dotnet tool for convenient access.

---

MIT licensed. Pull requests appreciated :)
