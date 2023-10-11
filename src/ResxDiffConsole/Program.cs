using CommandLine;
using CommandLine.Text;
using ResxDiffLib;

namespace ResxDiffConsole;

sealed class Program
{
    static void Main(string[] args)
    {
        var parserResult = Parser.Default.ParseArguments<Options>(args);
        parserResult.WithParsed(options =>
        {
            try
            {
                OnSuccessfulParse(options);
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        });
        parserResult.WithNotParsed(errs =>
        {
            var helpText = HelpText.AutoBuild(parserResult, h =>
            {
                return HelpText.DefaultParsingErrorsHandler(parserResult, h);
            }, e => e);
            Environment.Exit(1);
        });
    }

    private static void OnSuccessfulParse(Options options)
    {
        var manager = new ResxDocumentManager(options);
        var printer = new ResxDataPrinter(options.FullData);

        if (options.MissingKeys)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.MissingKeys(docs.Item1, docs.Item2);
            var data = keys.Select(k => docs.Item1.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, data.Select(printer.SingleDataString)));
        }

        if (options.PresentKeys)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.PresentKeys(docs.Item1, docs.Item2);
            var leftData = keys.Select(k => docs.Item1.Data.First(d => d.Name == k));
            var rightData = keys.Select(k => docs.Item2.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, leftData.Zip(rightData, printer.DoubleDataString)));
        }

        if (options.DifferentValues)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.DifferentValues(docs.Item1, docs.Item2);
            var leftData = keys.Select(k => docs.Item1.Data.First(d => d.Name == k));
            var rightData = keys.Select(k => docs.Item2.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, leftData.Zip(rightData, printer.DoubleDataString)));
        }

        if (options.IdenticalValues)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.IdenticalValues(docs.Item1, docs.Item2);
            var data = keys.Select(k => docs.Item1.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, data.Select(printer.SingleDataString)));
        }

        if (options.MismatchedMetadata)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.MismatchedMetadata(docs.Item1, docs.Item2);
            var leftData = keys.Select(k => docs.Item1.Data.First(d => d.Name == k));
            var rightData = keys.Select(k => docs.Item2.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, leftData.Zip(rightData, printer.DoubleDataString)));
        }

        if (options.DuplicateKeys)
        {
            var doc = manager.RequireOneFile();
            var keys = Helpers.DuplicateKeys(doc);
            var data = keys.Select(k => doc.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, data.Select(printer.SingleDataString)));
        }

        if (options.MissingSpacePreserve)
        {
            var doc = manager.RequireOneFile();
            var keys = Helpers.MissingSpacePreserve(doc);
            var data = keys.Select(k => doc.Data.First(d => d.Name == k));
            Console.Out.Write(string.Join(Environment.NewLine, data.Select(printer.SingleDataString)));
        }

        // Operations

        if (options.CopyMissingKeys)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.MissingKeys(docs.Item1, docs.Item2);
            Operations.CopyKeys(keys, docs.Item1, docs.Item2);
            manager.MarkForSaving(docs.Item2);
        }

        if (options.CopyDifferentValues)
        {
            var docs = manager.RequireTwoFiles();
            var keys = Helpers.DifferentValues(docs.Item1, docs.Item2);
            Operations.CopyValues(keys, docs.Item1, docs.Item2);
            manager.MarkForSaving(docs.Item2);
        }

        if (options.Alphabetise)
        {
            var docs = manager.RequireFiles();
            docs.ToList().ForEach(Operations.Alphabetise);
            manager.MarkForSaving(docs);
        }

        if (options.AddMissingSpacePreserve)
        {
            var docs = manager.RequireFiles();
            docs.ToList().ForEach(Operations.AddMissingSpacePreserve);
            manager.MarkForSaving(docs);
        }

        manager.SaveMarkedDocuments();
    }
}
