using System.Xml.Linq;

namespace Adliance.ResxR.Core;
public static class XmlUtils
{
    public static string? ValueOrNull(XAttribute? x)
    {
        return x?.Value;
    }

    public static string? ValueOrNull(XElement? x)
    {
        return x?.Value;
    }
}
