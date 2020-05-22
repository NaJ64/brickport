# brickport
 Catan tracking companion using .NET Core

### Development Prerequisites:

[Visual Studio Code](https://code.visualstudio.com/)

[.NET Core 3.0 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0)

[Node.js](https://nodejs.org/en/)

[Quasar](https://www.npmjs.com/package/quasar)

---

### Building Node.js / NPM projects:
Node.js package `@quasar/cli` CLI is required for managing the front-end project(s).

1. Run the following (one time) to install the package globally:

        npm install -g @quasar/cli

2. Run the following to install all dependencies within the `brickport-ui` directory

        npm install

3. Run the following to have quasar build the front-end project(s) within the `brickport-ui` directory:

        quasar build

### Building .NET Core projects / solution:
Run the following (in root project folder) to restore/build the solution using the .NET CLI:

    dotnet build

---

### Debugging / running the web app in *Visual Studio Code*

- Launch the following configuration to start the .NET application using Visual Studio Code's built-in debugger:

        .NET Core Launch (web)

- Alternatively, launch `brickport-ui` in "dev" mode using the command line within the `brickport-ui` directory:

        quasar dev
