using NUnit.Framework;
using System.IO;

namespace DloadOrganizer.Tests
{
    [TestFixture]
    public class ConfigurationManagerTests
    {
        private ConfigurationManager _sut;

        [SetUp]
        public void Setup()
        {
            //arrange
            _sut = new ConfigurationManager();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(Path.Combine(_sut.AppDir, "DloadOrganizerConfig.json")))
            {
                File.Delete(Path.Combine(_sut.AppDir, "DloadOrganizerConfig.json"));
            }
        }

        [Test]
        public void WriteConfigWorks()
        {
            //act
            _sut.WriteExampleConfig();
            //assert
            Assert.IsTrue(_sut.IsConfigExisting);
        }

        [Test]
        public void ReadConfigWorks()
        {
            //act
            _sut.WriteExampleConfig();
            var config = _sut.ReadConfigurationFile();

            //assert
            Assert.IsNotNull(config.SourceDirectory);
            Assert.IsNotNull(config.Rules);
        }
    }
}