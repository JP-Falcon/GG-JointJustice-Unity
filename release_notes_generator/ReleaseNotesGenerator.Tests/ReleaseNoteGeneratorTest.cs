using System;
using NUnit.Framework;

namespace ReleaseNotesGenerator.Tests;

[TestFixture]
[TestOf(typeof(ReleaseNoteGenerator))]
public class ReleaseNoteGeneratorTest
{
    [TestCase(
"""
chore(docs): Update auto-generated docs
feat!(Investigation): Split `&UNLOCK_CHOICE` into `&UNLOCK_MOVE_CHOICE` and `&UNLOCK_TALK_CHOICE`
feat(Investigation): Track already examined choice options
feat: Ability to pick up Details
fix(Investigation): Only show Details as already examined, if they were found in the same scene
fix!: Properly link mouse cursors
""",

"""
# New Features

## Investigation

- **BREAKING CHANGE**: Split `&UNLOCK_CHOICE` into `&UNLOCK_MOVE_CHOICE` and `&UNLOCK_TALK_CHOICE`
- Track already examined choice options

## Other

- Ability to pick up Details

# Bug Fixes

## Investigation

- Only show Details as already examined, if they were found in the same scene

## Other

- **BREAKING CHANGE**: Properly link mouse cursors
""")]
    [TestCase(
        "chore(docs): Update auto-generated docs",
        "No player-facing changes since last prerelease"
    )]
    [TestCase(
        "",
        "No player-facing changes since last prerelease"
    )]
    public void ConventionalCommitsGenerateMarkdown(string commits, string markdown)
    {
        Assert.That(ReleaseNoteGenerator.ParseLines(commits.Split(Environment.NewLine)), Is.EqualTo(markdown));
    }
    
}