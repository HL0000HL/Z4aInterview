# Run tests

In Solotion folder

- Restore dependencies for the solution:

```bash
dotnet restore
```

- Build the solution:

```bash
dotnet build
```

- Run all tests in the solution:

```bash
dotnet test
```

- Run all tests and show testcases on console:
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Containerized tests (Docker)

Prerequisites: Docker installed.

- **Build test image:**

```bash
docker build -t interviewz4a-tests . --progress=plain
```
