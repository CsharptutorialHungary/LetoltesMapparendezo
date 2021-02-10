using DloadOrganizer.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DloadOrganizer.Tests
{
    [TestFixture]
    public class OrganizerTests
    {
        private Organizer _sut;
        private Mock<IConsole> _consoleMock;
        private Mock<IConfigurationManager> _configManagerMock;
        private Mock<IConfigurationValidator> _configValidatorMock;
        private Mock<IFileSystem> _filesystemMock;

        private Configuration.Config _config;
        private const string _sourceDir = "test:\\dir";
        private const string _targetDir = "test:\\dir2";
        private readonly string[] _files = new[] { "test:\\dir\\file.tst" }; 

        [SetUp]
        public void Setup()
        {
            _consoleMock = new Mock<IConsole>(MockBehavior.Loose);
            _configManagerMock = new Mock<IConfigurationManager>(MockBehavior.Strict);
            _configValidatorMock = new Mock<IConfigurationValidator>(MockBehavior.Strict);
            _filesystemMock = new Mock<IFileSystem>(MockBehavior.Strict);

            _config = new Configuration.Config
            {
                SourceDirectory = _sourceDir,
                Rules = new Configuration.Rule[]
                {
                    new Configuration.Rule
                    {
                        Patterns = new string[] { ".tst" },
                        TargetDirectory = _targetDir,
                    }
                }
            };

            _configManagerMock.Setup(x => x.WriteExampleConfig());
            _configManagerMock.SetupGet(x => x.IsConfigExisting).Returns(true);
            _configManagerMock.Setup(x => x.ReadConfigurationFile()).Returns(_config);

            _configValidatorMock.Setup(x => x.IsValid(_config)).Returns(true);

            _filesystemMock.Setup(x => x.GetFiles(_sourceDir)).Returns(_files);
            _filesystemMock.Setup(x => x.Move(It.IsAny<string>(), It.IsAny<string>()));


            _sut = new Organizer(_consoleMock.Object,
                                 _configManagerMock.Object,
                                 _configValidatorMock.Object,
                                 _filesystemMock.Object);
        }

        [Test]
        public void LoadConfigExistsAppIfConfigNotValid()
        {
            //arrange
            _configValidatorMock.Setup(x => x.IsValid(_config)).Returns(false);
            _configValidatorMock.SetupGet(x => x.Errors).Returns(Enumerable.Empty<string>);

            //act
            _sut.LoadConfig();

            //assert
            _consoleMock.Verify(x => x.PressKeyAndExit(), Times.Once);
        }

        [Test]
        public void LoadConfigWritesConfigAndExistsIfNotExisting()
        {
            //arrange
            _configManagerMock.SetupGet(x => x.IsConfigExisting).Returns(false);

            //act
            _sut.LoadConfig();

            //assert
            _configManagerMock.Verify(x => x.WriteExampleConfig(), Times.Once);
            _consoleMock.Verify(x => x.PressKeyAndExit(), Times.Once);
        }

        [Test]
        public void RunWorks()
        {
            //arrange
            _sut.LoadConfig();
            
            //act
            _sut.Run();

            //assert
            _filesystemMock.Verify(x => x.Move("test:\\dir\\file.tst", "test:\\dir2\\file.tst"), Times.Once);
        }
    }
}
