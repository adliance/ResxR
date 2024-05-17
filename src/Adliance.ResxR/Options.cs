using CommandLine;
using CommandLine.Text;

namespace Adliance.ResxR;

sealed class Options
{
    [Value(2, MetaName = "files", Required = false)]
    public required IEnumerable<string> Files { get; set; }

    // Two files

    [Option('m', "missing-keys", HelpText = "Finds keys present in the first file which are missing in the second")]
    public bool MissingKeys { get; set; }

    [Option('p', "present-keys", HelpText = "Finds keys that are present in both the first and the second file")]
    public bool PresentKeys { get; set; }

    [Option('d', "different-values", HelpText = "Finds keys present in both files whos values differ")]
    public bool DifferentValues { get; set; }

    [Option('i', "identical-values", HelpText = "Finds keys present in both files with identical values")]
    public bool IdenticalValues { get; set; }

    [Option('s', "mismatched-metadata", HelpText = "Finds keys present in both files which have differing metadata (type, mimetype, space or comment)")]
    public bool MismatchedMetadata { get; set; }

    // One file

    [Option('u', "duplicate-keys", HelpText = "Finds keys that appear more than once")]
    public bool DuplicateKeys { get; set; }

    [Option('e', "missing-spacepreserve", HelpText = "Finds keys that are missing the xml:space=\"preserve\" attribute")]
    public bool MissingSpacePreserve { get; set; }

    // Operations on two files

    [Option('c', "copy-missing-keys", HelpText = "Copies missing keys from the first file to the second")]
    public bool CopyMissingKeys { get; set; }

    [Option('v', "copy-different-values", HelpText = "Copies differing values from the first file to the second")]
    public bool CopyDifferentValues { get; set; }

    // Operations on any number of files

    [Option('a', "alphabetise", HelpText = "Sorts keys into alphabetical order")]
    public bool Alphabetise { get; set; }

    [Option('r', "add-missing-spacepreserve", HelpText = "Adds xml:space=\"preserve\" attributes to keys that don't have it")]
    public bool AddMissingSpacePreserve { get; set; }

    // Formating

    [Option('f', "full-data", HelpText = "Shows all fields from the data elements")]
    public bool FullData { get; set; }

    // Usage

    //[Usage] Can't use it, because it does not generate the tool name to the examples.
    public static IEnumerable<Example> Usage
    {
        get
        {
            yield return new Example("ResxDiff [OPTION]... [FILE]", new Options { DuplicateKeys = true, Files = [@"C:\resx\duplicate-keys.resx"] });
            yield return new Example("ResxDiff [OPTION]... [FILE1] [FILE2]", new Options { MissingKeys = true, Files = [@"C:\resx\Translations.resx", @"C:\resx\Translations.de.resx"] });
            yield return new Example("ResxDiff [OPTION]... [FILE]...", new Options { Alphabetise = true, Files = [@"C:\resx\Test1.resx", @"C:\resx\Translations.resx", @"C:\resx\Translations.de.resx"] });
        }
    }

    public static HelpText GetUsageHelpText()
    {
        var help = new HelpText
        {
            Heading = HeadingInfo.Default,
            Copyright = CopyrightInfo.Default,
            AdditionalNewLineAfterOption = true,
            AddDashesToOption = true
        };
        help.AddPreOptionsLine("Displays information about .resx files, shows differences between .resx files and performs operations on .resx files");
        //Use the example usages from Usage property
        foreach (var example in Usage)
        {
            help.AddPreOptionsLine($"Usage: {example.HelpText}");

        }
        return help;
    }
}
