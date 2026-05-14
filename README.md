# Inventory managment

## Projekto paleidimas

1. Paleisti backend'a:

```bash
   cd backend/InventoryManagement.Api
   dotnet run
```

```
    Backend'as veiks ant http://localhost:5000
```

2. Paleisti frontend'a:

```bash
   cd frontend
   npm install
   npm run dev
```

```
    Frontend'as veikia ant http://localhost:5173
```

3. Atidarykite narsykleje frontendo ip

## Testu paleidimas

```bash
cd backend
dotnet test
```

## Naudotos technologijos

Backend:

- Entity Framework Core (In-Memory) - duomenu baze
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

- Vartotoju sarasas
- Inventoriaus irasu sarasas
- Filtravimas pagal tipa, komentara ir vartotoja
- Soft delete
- PDF eksportas su dviem skirtingais sablonais:
  - Lentele - visi irasai suvestineje
  - Sugrupuota pagal vartotoja - irasai grupuojami i vartotoju blokus
- Eksportas atspindi aktyvius filtrus ir neitraukia istrintu irasu

## Projekto struktura

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
