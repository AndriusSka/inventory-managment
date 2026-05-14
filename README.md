# Inventory managment

## Projekto inicializacija

1. Paleisti backend:

```bash
   cd backend/InventoryManagement.Api
   dotnet run
```

```
Backend veiks ant http://localhost:5000
```

2. Paleisti frontend:

```bash
   cd frontend
   npm install
   npm run dev
```

```
Frontend veiks ant http://localhost:5173
```

3. Atidaryti naršyklėje frontend'o adresą

## Testų paleidimas

```bash
cd backend
dotnet test
```

## Naudotos technologijos

Backend:

- EF Core (In-Memory) - duomenų bazė
- QuestPDF - PDF generavimas
- xUnit ir FluentAssertions - unit testai
- Swagger - API dokumentacija, be frontend pasitestuoti backend
- ASP.NET Core

Frontend:

- React
- TypeScript
- Vite
- Tailwind CSS - paprastesnis stilizavimas
- Axios - HTTP uzklausu klaidos metamos automatiskai

## Funkcionalumas

- Vartotojų sąrašas
- Inventoriaus įrašų sąrašas
- Filtravimas pagal tipą, komentarą ir vartotoją
- Soft delete
- PDF eksportas su dviem skirtingais šablonais:
  - Lentelė - visi įrašai suvestinėje
  - Sugrupuota pagal vartotoją - įrašai grupuojami į vartotojų blokus
- Eksportas atspindi aktyvius filtrus ir neįtraukia ištrintų įrašų

## Projekto struktūra

```
backend/
InventoryManagement.Api | HTTP sluoksnis (controller'iai, Program.cs)
InventoryManagement.Core | Modeliai, DTO, enums, interface'ai, mapper'iai
InventoryManagement.Infrastructure | DbContext'as, repositories, services, PDF
InventoryManagement.Tests | xUnit testai

frontend/src/
api | HTTP klientai
components | UI komponentai
pages | Puslapiai
types | TypeScript tipai
utils | Pagalbines funkcijos
```
