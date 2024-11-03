# Contributing

Most people volunteer to work on this project, so the team wants to ensure everyone can contribute autonomously and efficiently. Please read and follow the guidelines below.  
If you still have questions, contact any user under the `Studio Lovelies` role on [Discord](https://discord.gg/cv78xMTHVu).

## Initial setup

[Set up the project on your machine](https://github.com/Studio-Lovelies/GG-JointJustice-Unity/blob/develop/README.md) and pick any of the [open issues not assigned to a person](https://github.com/Studio-Lovelies/GG-JointJustice-Unity/issues?q=is%3Aopen+is%3Aissue+no%3Aassignee).  

> [!TIP]
[GitHub](https://skills.github.com/) and [Unity](https://learn.unity.com/) offer free courses to get you started with no prior knowledge required.
To get experience on this project use the list of [**good first issues**](https://github.com/Studio-Lovelies/GG-JointJustice-Unity/contribute).

Before you start working on the issue, [assign the issue to yourself for the next few days](https://docs.github.com/en/issues/tracking-your-work-with-issues/using-issues/assigning-issues-and-pull-requests-to-other-github-users). This prevents others from working on the same issue accidentally.

> [!IMPORTANT]  
> If you can't finish the issue after around 3 days, remove your assignment again or contact someone for support. That way others can pick up the issue and work on it.

Create a branch and commit your changes. If you can't push your branch to this repository, [create a fork](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo) and push your changes there.

While not strictly required, follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) standard. Each commit of type `feat` or `fix` generates release notes from your commit messages which GitHub automatically sends to all developers after integrating your changes:

```commit
feat(GitHub)!: Automatically creates new build, if Demo script on Google Drive changes
fix(Case Select): Adds button to start the SR Demo from the main menu
```

![GitHub Release notes generated from the preceding commits](assets/2024-11-03-15-29-27.png)

If you don't want to follow this standard for each commit while developing, set a final conventional commit message to group your changes [after Pull Request creation](#after-pull-request-creation).

## Integrating your changes

To integrate your changes, you will create a Pull Request. Strive to [Make Your Code Reviewer Fall in Love with You](https://mtlynch.io/code-review-love/).

Changes require a few checks to pass:

- at least one other person has manually reviewed your changes
- all automated tests pass
- documentation for writers stays up-to-date
- changes required only for local development don't remain in the code

GitHub checks this automatically, but the process differs depending on your role:

### External contributor

If you don't have write access to this repository, [create a pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request) from your branch to `develop`, fill out the template and contact any user under the `Studio Lovelies` role on [Discord](https://discord.gg/cv78xMTHVu). After someone responds, you can reopen your Pull Request in the browser and continue with this page.

### Internal contributor

[Create a **draft** pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request) from your branch to `develop` and fill out the template.

### After Pull Request creation

1. GitHub automatically starts all checks. Scroll down to watch the progress:

    ![a list of pending checks on a GitHub Pull Request](assets/2024-11-03-15-05-08.png)

2. Scroll through the list of checks:

   - Yellow icons indicates running checks. Wait around 30 minutes for them to complete.
   - Red icons show errors. Click `Details` on the right, read the error message, fix the underlying issue locally and push the changes to your branch. The push restarts the checks automatically. Repeat this process until all checks pass.
   - Instead of pushing code to re-run tests, you can [run relevant tests locally](https://docs.unity3d.com/Packages/com.unity.test-framework@1.4/manual/workflow-run-test.html) instead, to verify potential fixes more quickly.
   - Green icons show successful checks. Once all checks pass, a reviewer manually reviews your changes.

3. Set your Pull Request to auto-complete:

   - Select `Rebase`, if you want all your commits to remain and become part of the release notes
   - Select `Squash`, if you want to discard all your previous commit messages and instead set a final conventional commit message for your changes instead

    ![GitHub's dropdown menu for auto-completing a Pull Request](assets/2024-11-03-15-17-58.png)

4. After setting the auto-complete method and all checks pass, GitHub automatically integrates your change and informs the team
