using System.Xml.Linq;
using NUnit.Framework;

namespace Adliance.ResxR.Core.Tests;

[TestFixture]
public class LoadTest
{

    [Test]
    public void Load()
    {
        var test1 = new ResxDocument(TestUtils.LoadTest1());

        Assert.That(test1.Data.Count, Is.EqualTo(3));
        Assert.That(test1.Data[0].Name, Is.EqualTo("Test_key_1"));
        Assert.That(test1.Data[0].Type, Is.EqualTo(null));
        Assert.That(test1.Data[0].Mimetype, Is.EqualTo(null));
        Assert.That(test1.Data[0].Space, Is.EqualTo("preserve"));

        Assert.That(test1.Data[0].Value, Is.EqualTo("Test value 1"));
        Assert.That(test1.Data[0].Comment, Is.EqualTo("A comment"));
    }

    [Test]
    public void ResxDataToXml()
    {
        var data = new ResxData
        {
            Name = "A_key",
            Value = "A key",
            Space = "preserve"
        };
        var elem = data.ToXml();

        Assert.That(elem.Name.ToString(), Is.EqualTo("data"));
        Assert.That(elem.Attribute("name")?.Value, Is.EqualTo("A_key"));
        Assert.That(elem.Attribute(XNamespace.Xml + "space")?.Value, Is.EqualTo("preserve"));
        Assert.That(elem.Element("value")?.Value, Is.EqualTo("A key"));
    }

    [Test]
    public void ResxDocumentToXml()
    {
        var test1 = new ResxDocument(TestUtils.LoadTest1());
        Assert.That(test1.ToXml()?.ToString(), Is.EqualTo(TestUtils.LoadTest1().ToString()));
    }

    [Test]
    public void ResxDocumentToXmlWithChanges()
    {
        var test1 = new ResxDocument(TestUtils.LoadTest1());
        test1.Data.Add(new ResxData
        {
            Name = "Test_key_4",
            Value = "Test value 4",
            Space = "preserve"
        });
        Assert.That(test1.ToXml()?.ToString(), Is.EqualTo(TestUtils.LoadTest2().ToString()));
    }
}
