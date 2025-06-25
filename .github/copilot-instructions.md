# Instructions and Guidance for Copilot

## Building

- When building this repository, use `dotnet build`
- If you have performed a full build once and need to build again, append `--no-restore` to prevent restore from being called if you have not modified the package dependencies.
- If you add a new dependency like a new package, the next build you will have to `restore` first.
