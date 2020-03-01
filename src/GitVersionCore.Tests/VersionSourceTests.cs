using GitTools.Testing;
using GitVersion;
using GitVersion.Configuration;
using GitVersion.Logging;
using GitVersion.Models.LibGitSharpWrappers;
using GitVersion.VersionCalculation;
using GitVersionCore.Tests.Helpers;
using LibGit2Sharp;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace GitVersionCore.Tests
{
    [TestFixture]
    public class VersionSourceTests : TestBase
    {
        private ILog log;
        private INextVersionCalculator nextVersionCalculator;

        [SetUp]
        public void SetUp()
        {
            var sp = ConfigureServices();

            log = sp.GetService<ILog>();
            nextVersionCalculator = sp.GetService<INextVersionCalculator>();
        }

        [Test]
        public void VersionSourceSha()
        {
            var config = new Config().ApplyDefaults();

            using var fixture = new EmptyRepositoryFixture();
            var initialCommit = fixture.Repository.MakeACommit();
            Commands.Checkout(fixture.Repository, fixture.Repository.CreateBranch("develop"));
            _ = fixture.Repository.MakeACommit();
            var featureBranch = fixture.Repository.CreateBranch("feature/foo");
            Commands.Checkout(fixture.Repository, featureBranch);
            _ = fixture.Repository.MakeACommit();

            var context = new GitVersionContext(fixture.Repository, log, fixture.Repository.Head, config);
            var version = nextVersionCalculator.FindVersion(context);

            version.BuildMetaData.VersionSourceSha.ShouldBe(initialCommit.Sha);
            version.BuildMetaData.CommitsSinceVersionSource.ShouldBe(2);
        }

        [Test]
        public void VersionSourceShaOneCommit()
        {
            var config = new Config().ApplyDefaults();

            using var fixture = new EmptyRepositoryFixture();
            var initialCommit = fixture.Repository.MakeACommit();

            var context = new GitVersionContext(new LibGitRepository(fixture.Repository), log, new LibGitBranch(fixture.Repository.Head), config);
            var version = nextVersionCalculator.FindVersion(context);

            version.BuildMetaData.VersionSourceSha.ShouldBe(initialCommit.Sha);
            version.BuildMetaData.CommitsSinceVersionSource.ShouldBe(0);
        }

        [Test]
        public void VersionSourceShaUsingTag()
        {
            var config = new Config().ApplyDefaults();

            using var fixture = new EmptyRepositoryFixture();
            _ = fixture.Repository.MakeACommit();
            Commands.Checkout(fixture.Repository, fixture.Repository.CreateBranch("develop"));
            var secondCommit = fixture.Repository.MakeACommit();
            _ = fixture.Repository.Tags.Add("1.0", secondCommit);
            var featureBranch = fixture.Repository.CreateBranch("feature/foo");
            Commands.Checkout(fixture.Repository, featureBranch);
            _ = fixture.Repository.MakeACommit();

            var context = new GitVersionContext(new LibGitRepository(fixture.Repository), log, new LibGitBranch(fixture.Repository.Head), config);
            var version = nextVersionCalculator.FindVersion(context);

            version.BuildMetaData.VersionSourceSha.ShouldBe(secondCommit.Sha);
            version.BuildMetaData.CommitsSinceVersionSource.ShouldBe(1);
        }
    }
}
