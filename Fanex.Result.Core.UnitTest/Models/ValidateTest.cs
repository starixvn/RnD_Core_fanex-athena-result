namespace Fanex.Result.Core.UnitTest.Models
{
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Models;
    using NUnit.Framework;

    /// <summary>
    /// Apply unit testing for Validate class.
    /// </summary>
    [TestFixture]
    public class ValidateTest
    {
        private IValidate _validate = Validate.Instance;

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Testing DTO name is empty.
        /// </summary>
        [Test]
        public void ValidateNormal_EmptyDTOName_ReturnFalse()
        {
            // Arrange
            var dtoName = string.Empty;
            var sportType = SportType.Soccer;

            // ACT
            var result = _validate.ValidateNormal(dtoName, sportType);

            // Assert
            Assert.IsFalse(result, "DTO name should empty.");
        }

        /// <summary>
        /// Testing sport type is not existed.
        /// </summary>
        [Test]
        public void ValidateNormal_NotExistSportType_ReturnFalse()
        {
            // Arrange
            var dtoName = DataDomain.SPORTBOOK;
            var sportType = (SportType)(-1);

            var result = Validate.Instance.ValidateNormal(dtoName, sportType);

            Assert.IsFalse(result, "Sport type is not existed.");
        }

        /// <summary>
        /// Testing sport type is live casino, DTO name is right or wrong.
        /// </summary>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="expected">Expected data.</param>
        [TestCase(DataDomain.LIVECASINO, true)]
        [TestCase("WrongDTO_LiveCasino", false)]
        public void ValidateNormal_SportTypeIsLiveCasino_CheckDTOName(string dtoName, bool expected)
        {
            var sportType = SportType.LiveCasino;

            var result = Validate.Instance.ValidateNormal(dtoName, sportType);

            Assert.AreEqual(expected, result, "Sporttype is live casino.");
        }

        /// <summary>
        /// Testing sport type is number game, DTO name is right or wrong.
        /// </summary>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="expected">Expected data.</param>
        [TestCase(DataDomain.NUMBERGAME, true)]
        [TestCase("WrongDTO_NumberGame", false)]
        public void ValidateNormal_SportTypeIsNumberGame_CheckDTOName(string dtoName, bool expected)
        {
            var sportType = SportType.NumberGame;

            var result = Validate.Instance.ValidateNormal(dtoName, sportType);

            Assert.AreEqual(expected, result, "Sporttype is number game.");
        }

        /// <summary>
        /// Testing sport type is Horse Racing, DTO name is right or wrong.
        /// </summary>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="expected">Expected data.</param>
        [TestCase(DataDomain.RACING, true)]
        [TestCase("WrongDTO_Racing", false)]
        public void ValidateNormal_SportTypeIsHorseRacing_CheckDTOName(string dtoName, bool expected)
        {
            var sportType = SportType.HorseRacing;

            var result = Validate.Instance.ValidateNormal(dtoName, sportType);

            Assert.AreEqual(expected, result, "Sporttype is HorseRacing.");
        }

        /// <summary>
        /// Testing sport type is sports book, DTO name is right or wrong.
        /// </summary>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="expected">Expected data.</param>
        [TestCase(DataDomain.SPORTBOOK, true)]
        [TestCase("WrongDTO_Sportsbook", false)]
        public void ValidateNormal_SportTypeIsSportsbook_CheckDTOName(string dtoName, bool expected)
        {
            var sportType = SportType.OtherSports;

            var result = Validate.Instance.ValidateNormal(dtoName, sportType);

            Assert.AreEqual(expected, result, "Sporttype is sportsbook.");
        }

        /// <summary>
        /// Testing Validate OutRight method.
        /// </summary>
        [Test]
        public void ValidateOutRight_ExistDTOName_ReturnTrue()
        {
            var dtoName = "OutRight";
            var result = Validate.Instance.ValidateOutRight(dtoName);
            Assert.IsTrue(result, "Validation outright when input DTO Name is outright.");
        }

        /// <summary>
        /// Testing Validate Parlay method.
        /// </summary>
        [Test]
        public void ValidateParlay_ExistDTOName_ReturnTrue()
        {
            var dtoName = "Parlay";
            var result = Validate.Instance.ValidateParlay(dtoName);
            Assert.IsTrue(result, "Validation Parlay when input DTO Name is Parlay.");
        }
    }
}