using System;
using System.Text;
using GitTools.Testing;
using GitVersion;
using GitVersion.Configuration;
using GitVersion.Extensions;
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

            var context = new GitVersionContext(fixture.Repository.Wrap(), log, new LibGitBranch(fixture.Repository.Head), config);
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

            var context = new GitVersionContext(fixture.Repository.Wrap(), log, new LibGitBranch(fixture.Repository.Head), config);
            var version = nextVersionCalculator.FindVersion(context);

            version.BuildMetaData.VersionSourceSha.ShouldBe(initialCommit.Sha);
            version.BuildMetaData.CommitsSinceVersionSource.ShouldBe(0);
        }

        [Test]
        public void VersionSourceShaUsingTag()
        {
            // ReSharper disable UnusedVariable
            var config = new Config().ApplyDefaults();

            using var fixture = new EmptyRepositoryFixture();
            var commit1 = fixture.Repository.MakeACommit();
            Commands.Checkout(fixture.Repository, fixture.Repository.CreateBranch("develop"));
            var secondCommit = fixture.Repository.MakeACommit();
            var commit2 = fixture.Repository.Tags.Add("1.0", secondCommit);
            var featureBranch = fixture.Repository.CreateBranch("feature/foo");
            Commands.Checkout(fixture.Repository, featureBranch);
            var commit3 = fixture.Repository.MakeACommit();

            var wrappedRepository = fixture.Repository.Wrap();
            var context = new GitVersionContext(wrappedRepository, log, new LibGitBranch(fixture.Repository.Head), config);


            // DEBUGGING
            var coreGraph = new StringBuilder();
            fixture.Repository.DumpGraph(s => coreGraph.Append(s));

            var wrappedGraph = new StringBuilder();
            wrappedRepository.DumpGraph(s => wrappedGraph.Append(s));

            Console.WriteLine(coreGraph.ToString());
            Console.WriteLine(wrappedGraph.ToString());

            wrappedGraph.ToString().ShouldBe(coreGraph.ToString());
            // END DEBUGGING


            var version = nextVersionCalculator.FindVersion(context);

            version.BuildMetaData.VersionSourceSha.ShouldBe(secondCommit.Sha);
            version.BuildMetaData.CommitsSinceVersionSource.ShouldBe(1);
            // ReSharper restore UnusedVariable
        }
    }
}
