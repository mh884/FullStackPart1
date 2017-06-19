using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.Http.Metadata.Providers;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Presistence;
using GigHub.Presistence.Repository;
using GigHub.Tests.Extinctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace GigHub.Tests.Persistence.Repository
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRespository _gigRespository;
        private Mock<DbSet<Gigs>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gigs>>();

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);


            _gigRespository = new GigRespository(mockContext.Object);


        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsInThePast_ShouldNotBeReturn()
        {
            var gig = new Gigs { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _gigRespository.GetUpComingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsCanceled_ShoudNotBeReturned()
        {
            var gig = new Gigs { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRespository.GetUpComingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomoingByArtist_GigISforDiffrentArtist_ShouldNotBeReturn()
        {
            var gig = new Gigs { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };



            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRespository.GetUpComingGigsByArtist(gig.ArtistId + "_");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsforTheGivenArtistAndIsInTheFuture_ShouldReturnOk()
        {
            var gig = new Gigs { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };



            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigRespository.GetUpComingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
    }
}
