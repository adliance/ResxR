using System.Xml.Linq;
using ResxDiffLib;

namespace ResxDiffConsole;

sealed class ResxDocumentManager
{
    private readonly Options _options;
    private readonly IDictionary<string, SavableResxDocument> _loadedDocuments = new Dictionary<string, SavableResxDocument>();
    private readonly ISet<string> _documentsToSave = new HashSet<string>();

    public ResxDocumentManager(Options options)
    {
        _options = options;
    }

    public SavableResxDocument RequireOneFile()
    {
        if (_options.Files.Count() != 1)
        {
            Console.Error.Write("An option you have specified requires exactly one file");
            Environment.Exit(1);
        }
        return GetDocument(_options.Files.First());
    }

    public Tuple<SavableResxDocument, SavableResxDocument> RequireTwoFiles()
    {
        var files = _options.Files.ToList();
        if (files.Count != 2)
        {
            Console.Error.Write("An option you have specified requires exactly two files");
            Environment.Exit(1);
        }
        return new Tuple<SavableResxDocument, SavableResxDocument>(
            GetDocument(files[0]),
            GetDocument(files[1]));
    }

    public IEnumerable<SavableResxDocument> RequireFiles()
    {
        if (!_options.Files.Any())
        {
            Console.Error.Write(Options.GetUsage());
            Environment.Exit(1);
        }
        return _options.Files.Select(GetDocument).ToList();
    }

    public void MarkForSaving(SavableResxDocument document)
    {
        _documentsToSave.Add(document.Path);
    }

    public void MarkForSaving(IEnumerable<SavableResxDocument> documents)
    {
        foreach (var d in documents)
        {
            MarkForSaving(d);
        }
    }

    public void SaveMarkedDocuments()
    {
        _documentsToSave.Select(path => _loadedDocuments[path]).ToList().ForEach(d => d.Save());
    }

    private SavableResxDocument GetDocument(string path)
    {
        if (!_loadedDocuments.TryGetValue(path, out var value))
        {
            value = new SavableResxDocument(path);
            _loadedDocuments[path] = value;
        }
        return value;
    }

    public sealed class SavableResxDocument : ResxDocument
    {
        public string Path { get; set; }

        public SavableResxDocument(string path)
            : base(XDocument.Load(path))
        {
            Path = path;
        }

        public void Save()
        {
            var xml = ToXml();
            if (xml == null)
            {
                return;
            }
            xml.Save(Path);
        }
    }
}
