using Xunit.Sdk;

namespace TestCommandLineArgs
{
    public class TestCommandLineArgs
    {
        private void CreateNewInstanceCommandLineArgs(string[] args)
        {
            var creation = new CommandLineArgs(args);
        }

        [Fact]
        public void TestingFilePathTakingFirstElementOfArgumentArray()
        {
            //Arrange
            var arguments = new string[] { "umpa", "lumpa" };
            var expectedFilePath = arguments[0];

            //Act
            var commandLineArgs = new CommandLineArgs(arguments);

            //Assert
            Assert.Equal(expectedFilePath, commandLineArgs.FilePath);
        }
        [Fact]
        public void TestingOutputTakingSecondElementOfArgumentArray()
        {
            //Arrange
            var arguments = new string[] { "farmlingi", "trimbolo" };
            var expectedOutputMode = arguments[1];

            //Act
            var commandLineArgs = new CommandLineArgs(arguments);

            //Assert
            Assert.Equal(expectedOutputMode, commandLineArgs.OutputMode);
        }
       
        [Theory]
        [InlineData(0, "")]
        [InlineData(1, " ")]
        [InlineData(3, " ")]
        public void TestingIncorrectAmountOfArguments(int numberOfArguments, string argumentValue)
        {
            //Arrange
            var array = new string[numberOfArguments];
            for (int i = 0; i < numberOfArguments; i++)
            {
                array[i] = argumentValue;
            }
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => CreateNewInstanceCommandLineArgs(array));
        }
    }

}