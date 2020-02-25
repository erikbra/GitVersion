using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitTools.Testing;
using GitVersion.Extensions;
using GitVersion.Models.Abstractions;
using GitVersion.Models.LibGitSharpWrappers;
using LibGit2Sharp;
using NUnit.Framework;
using Shouldly;

namespace GitVersionCore.Tests.Models
{
    public class LibGitRepository_
    {
        [Test]
        public void Can_List_All_Commits()
        {
            using var fixture = new EmptyRepositoryFixture();

            for (var i = 0; i < 10; i++)
            {
                _ = fixture.Repository.MakeACommit();
            }

            var coreRepo = fixture.Repository;
            var wrapperRepo = new LibGitRepository(coreRepo);

            Shas(wrapperRepo.Commits).ShouldBeEquivalentTo(Shas(coreRepo.Commits));
        }

        [Test]
        public void Can_List_All_Branches()
        {
            using var fixture = new EmptyRepositoryFixture();

            for (var i = 0; i < 10; i++)
            {
                _ = fixture.Repository.MakeACommit();
                _ = fixture.Repository.MakeACommit();
                Commands.Checkout(fixture.Repository, fixture.Repository.CreateBranch("branch" + i));
                _ = fixture.Repository.MakeACommit();
            }

            var coreRepo = fixture.Repository;
            var wrapperRepo = new LibGitRepository(coreRepo);

            var wrapperGraph = new StringBuilder();
            wrapperRepo.DumpGraph(s => wrapperGraph.Append(s));

            var coreGraph = new StringBuilder();
            coreRepo.DumpGraph(s => coreGraph.Append(s));

            Console.WriteLine(coreGraph);

            wrapperGraph.ToString().ShouldBe(coreGraph.ToString());
            Names(wrapperRepo.Branches).ShouldBeEquivalentTo(Names(coreRepo.Branches));
        }

        [Test]
        public void QueryBy_Works()
        {
            using var fixture = new EmptyRepositoryFixture();

            for (var i = 0; i < 10; i++)
            {
                _ = fixture.Repository.MakeACommit();
                _ = fixture.Repository.MakeACommit();
                Commands.Checkout(fixture.Repository, fixture.Repository.CreateBranch("branch" + i));
                _ = fixture.Repository.MakeACommit();
            }

            var tip = fixture.Repository.Head.Tip;
            var wrappedTip = new LibGitCommit(tip);

            var coreRepo = fixture.Repository;
            var wrapperRepo = new LibGitRepository(coreRepo);

            var coreCommitLog = wrapperRepo.Wrapped.Commits.QueryBy(
                new CommitFilter()
                {
                    IncludeReachableFrom = (tip)
                }
            );

            var wrappedCommitLog = wrapperRepo.Commits.QueryBy(new GitCommitFilter
            {
                IncludeReachableFrom = wrappedTip
            });

            Shas(wrappedCommitLog).ShouldBeEquivalentTo(Shas(coreCommitLog));
        }

        private static List<string> Shas(ICommitLog commits) => commits.Select(c => c.Sha).ToList();
        private static List<string> Shas(IGitCommitLog commits) => commits.Select(c => c.Sha).ToList();
        private static List<string> Names(IGitBranchCollection branches) => branches.Select(c => c.CanonicalName).ToList();
        private static List<string> Names(BranchCollection commits) => commits.Select(c => c.CanonicalName).ToList();
    }
}
