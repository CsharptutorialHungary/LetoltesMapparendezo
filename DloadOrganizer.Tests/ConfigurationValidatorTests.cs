using DloadOrganizer.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DloadOrganizer.Tests
{
    [TestFixture]
    internal class ConfigurationValidatorTests
    {
        public Config ValidConfig;

        private ConfigurationValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ConfigurationValidator();
            ValidConfig = new Config
            {
                SourceDirectory = "c:\\",
                Rules = new Rule[]
                {
                    new Rule
                    {
                        Patterns = new string[] { ".txt" },
                        TargetDirectory = "c:\\"
                    }
                }
            };
        }

        [Test]
        public void TestValidConfig()
        {
            //act
            bool result = _sut.IsValid(ValidConfig);

            //assert
            Assert.IsFalse(_sut.Errors.Any());
            Assert.IsTrue(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("z:\\asd\\foo")]
        public void TestSourceDir(string dir)
        {
            //arrange
            ValidConfig.SourceDirectory = dir;

            //act
            bool result = _sut.IsValid(ValidConfig);

            //assert
            Assert.IsFalse(result);
            Assert.IsTrue(_sut.Errors.Any());
        }


        internal static IEnumerable<Rule> InvalidRules
        {
            get
            {
                yield return new Rule
                {
                    TargetDirectory = null,
                    Patterns = new string[] { ".txt" }
                };
                yield return new Rule
                {
                    TargetDirectory = "",
                    Patterns = new string[] { ".txt" }
                };
                yield return new Rule
                {
                    TargetDirectory = "z:\\adgft\\wqg3g",
                    Patterns = new string[] { ".txt" }
                };
                yield return new Rule
                {
                    TargetDirectory = "c:\\",
                    Patterns = null,
                };
                yield return new Rule
                {
                    TargetDirectory = "c:\\",
                    Patterns = new string[0],
                };
            }
        }

        [TestCaseSource(nameof(InvalidRules))]
        public void TestRules(Rule rule)
        {
            ValidConfig.Rules[0] = rule;

            //act
            bool result = _sut.IsValid(ValidConfig);

            //assert
            Assert.IsFalse(result);
            Assert.IsTrue(_sut.Errors.Any());
        }

        [Test]
        public void TestNoRules()
        {
            ValidConfig.Rules = null;

            //act
            bool result = _sut.IsValid(ValidConfig);

            //assert
            Assert.IsFalse(result);
            Assert.IsTrue(_sut.Errors.Any());
        }
    }
}
