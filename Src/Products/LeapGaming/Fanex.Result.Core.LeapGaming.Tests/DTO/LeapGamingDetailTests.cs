namespace Fanex.Result.Core.LeapGaming.Tests.DTO
{
    using Fanex.Result.Core.LeapGaming.DTO;
    using NUnit.Framework;

    public class LeapGamingDetailTests
    {
        [Test]
        public void MatchDate_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.MatchDate;

            // Assert
            Assert.AreEqual("5", act);
        }

        [Test]
        public void MatchDate_HasNoData_RetureEmpty()
        {
            // Arrange
            var transDesc = "mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.MatchDate;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void MatchId_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.MatchId;

            // Assert
            Assert.AreEqual("710897", act);
        }

        [Test]
        public void MatchId_HasNoData_RetureEmpty()
        {
            // Arrange
            var transDesc = "se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.MatchId;

            // Assert
            Assert.IsEmpty(act);
        }
        [Test]
        public void Season_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.Season;

            // Assert
            Assert.AreEqual("2954", act);
        }

        [Test]
        public void Season_HasNoData_RetureEmpty()
        {
            // Arrange
            var transDesc = "mid=710897;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.Season;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void HomeTeam_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeTeam;

            // Assert
            Assert.AreEqual("London", act);
        }

        [TestCase("")]
        [TestCase("m=")]
        public void HomeTeam_HasNoData_RetureEmpty(string m)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;{m};s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeTeam;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void AwayTeam_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayTeam;

            // Assert
            Assert.AreEqual("Florence", act);
        }

        [TestCase("")]
        [TestCase("m=")]
        public void AwayTeam_HasNoData_RetureEmpty(string m)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;{m};s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayTeam;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void HomeHTScore_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeHTScore;

            // Assert
            Assert.AreEqual("1", act);
        }

        [TestCase("")]
        [TestCase("m=")]
        public void HomeHTScore_HasNoData_RetureEmpty(string htscore)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;{htscore};ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeHTScore;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void AwayHTScore_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayHTScore;

            // Assert
            Assert.AreEqual("2", act);
        }

        [TestCase("")]
        [TestCase("ht=")]
        public void AwayHTScore_HasNoData_RetureEmpty(string htscore)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;{htscore};ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayHTScore;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void HomeFinalScore_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeFinalScore;

            // Assert
            Assert.AreEqual("1", act);
        }

        [TestCase("")]
        [TestCase("ft=")]
        public void HomeFinalScore_HasNoData_RetureEmpty(string ftscore)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;{ftscore};mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.HomeFinalScore;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void AwayFinalScore_HasData_RetureCorrectData()
        {
            // Arrange
            var transDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;ft=1-1;mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayFinalScore;

            // Assert
            Assert.AreEqual("1", act);
        }

        [TestCase("")]
        [TestCase("ft=")]
        public void AwayFinalScore_HasNoData_RetureEmpty(string ftscore)
        {
            // Arrange
            var transDesc = $"mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;ht=1-2;{ftscore};mo=WIN";
            var detail = new LeapGamingDetail(transDesc);

            // Act
            var act = detail.AwayFinalScore;

            // Assert
            Assert.IsEmpty(act);
        }
    }
}
