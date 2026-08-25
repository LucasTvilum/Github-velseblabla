# UsersWebApi

Simpel Web API til at øve GitFlow + GitHub Projects. Matcher casen fra undervisningen:
`POST /users/register`, `POST /users/login`, `GET /users/{id}`, `GET /users`.

## Struktur (Controller + Repository pattern)

```
UsersWebApi/
├── Models/
│   └── User.cs              Datamodel
├── Repositories/
│   ├── IUserRepository.cs   Interface (kontrakt)
│   └── UserRepository.cs    In-memory implementation (ingen DB nødvendig)
├── Controllers/
│   └── UsersController.cs   Endpoints (HTTP-lag)
└── Program.cs                Opstart + dependency injection
```

Controlleren kender kun interfacet `IUserRepository`, ikke den konkrete klasse —
det gør det let at skifte til en rigtig database senere uden at ændre controlleren.

## Kør projektet

```bash
cd UsersWebApi
dotnet restore
dotnet run
```

Åbn derefter Swagger UI i browseren (URL vises i terminalen, typisk
`https://localhost:xxxx/swagger`) for at teste endpoints uden Postman.

## Sådan kan I øve GitFlow på det

1. `git init` og `git add . && git commit -m "initial commit"`
2. `git flow init` (brug default navne)
3. Par 1: `git flow feature start register-user` → ret evt. i `UsersController.Register`
4. Par 2: `git flow feature start login-auth` → ret evt. i `UsersController.Login`
5. `git flow feature finish <navn>` når I er færdige, eller `git flow feature publish <navn>`
   hvis I vil lave en Pull Request på GitHub i stedet
6. Opret et issue pr. endpoint på GitHub, læg dem på et Project board, og brug
   `Closes #<nummer>` i jeres PR-beskrivelse

## Idéer til at bygge videre (Sprint 2 øvelser)

- Tilføj en `UpdateUser` og `DeleteUser` metode
- Tilføj rigtig password-hashing (fx `BCrypt.Net`)
- Skriv en unit test for `UserRepository` (øv TDD: Red → Green → Refactor)
- Skift `UserRepository` til at bruge Entity Framework Core + en rigtig database
