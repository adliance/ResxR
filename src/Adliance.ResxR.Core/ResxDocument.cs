using System.Xml.Linq;

namespace Adliance.ResxR.Core;
public class ResxDocument
{
    public IList<ResxData> Data { get; set; }

    private readonly XDocument _xml;

    public ResxDocument(XDocument xml)
    {
        if (xml.Root == null || xml.Root.Name != "root")
        {
            throw new ArgumentException("No <root> element found");
        }

        Data = xml.Root.Elements("data").Select(elem => new ResxData(elem)).ToList();

        _xml = new XDocument(xml);
    }

    public XDocument? ToXml()
    {
        var xml = new XDocument(_xml);
        var xmlRoot = xml.Root;
        if (xmlRoot == null)
        {
            return null;
        }

        xmlRoot.Elements("data").Remove();
        xmlRoot.Add(Data.Select(data => data.ToXml()).ToArray());

        return xml;
    }
}
