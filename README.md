# Board‑Gamer‑App

*Organisations‑App für private Brettspielrunden*

---

## 1 · Projektüberblick

Die **Board‑Gamer‑App** reduziert den Abstimmungs‑ und Kommunikations­aufwand rund um regelmäßige Brettspielabende. Gastgeber\:innen können Termine festlegen, Teilnehmende schlagen Spiele vor, stimmen ab und bewerten den Abend nachträglich.
**Hauptfunktionen**
Add commentMore actions
| Kategorie        | Features                                                                       |
| ---------------- | ------------------------------------------------------------------------------ |
| Terminverwaltung | Nächsten Spielabend sehen, Gastgeber\*in & Ort rotieren lassen, RSVP‑Übersicht |
| Spieleauswahl    | Spiele vorschlagen, Up‑/Down‑Votes, Gewinner automatisch ermitteln             |
| On‑Site Chat     | Sitzungsspezifischer Chat, Push‑Benachrichtigungen                             |
| Review           | Sterne‑Ratings für Gastgeber\*in, Verpflegung & Gesamtabend                    |

---

## 2 · Technologiestack

| Schicht                | Technologie                   | Motivation                                                       |
| ---------------------- | ----------------------------- | ---------------------------------------------------------------- |
| **Programmiersprache** | **C#**                        | Team‑Know‑how, einheitlich für Front‑ & Backend                  |
| **Frontend**           | **.NET MAUI**                 | Eine Code‑Basis ➜ Android & iOS, XAML + MVVM‑Pattern             |
| **Backend API**        | **ASP.NET Core (Web API)**    | Plattformunabhängig, große Community, einfache Cloud‑Integration |
| **Datenbank**          | **Firebase Realtime DB**      | Skalierbare NoSQL‑Datenbank, großzügiges Free‑Tier               |
| **App‑Distribution**   | **Firebase App Distribution** | Schnelles Roll‑Out an Testnutzer\:innen                          |

> **Hinweis:** Zum Ausführen der App musst du deine *eigenen* Firebase‑Projekt‑Secrets (z. B. `FIREBASE_API_KEY`, `PROJECT_ID`) als **User‑Secrets** oder **Umgebungsvariablen** hinterlegen. Diese werden *nicht* im Repository versioniert.

---

## 3 · Architektur & Datenmodell

Die Anwendung nutzt eine **Layered Architecture (Schichtenarchitektur)**:

* **Presentation Layer** – `BoardGamerApp` (Views, ViewModels)
* **Business Layer** – `BoardGamerApp.Business` (Services, Models, Interfaces)
* **Data Access Layer** – `BoardGamerApp.Database` (Repositories, Datenzugriff)

```
Presentation (.NET MAUI)  ⇆  Business (Services)  ⇆  Data Access (Firebase)
```

### Entity‑Relationship‑Diagramm

![ERD\_Board-Gamer-App](https://github.com/user-attachments/assets/2e221f71-8388-4881-b58c-8d4baa03d590)


*Wesentliche Entitäten*

Client (.NET MAUI)  ⇆  ASP.NET Core API  ⇆  Firebase (NoSQL)

---

## 4 · Workflow & Deployment

| Phase               | Tools                                                         |
| ------------------- | ------------------------------------------------------------- |
| **Planung**         | Jira, Figma Wireframes                                        |
| **Implementierung** | Rider / Visual Studio + Hot Reload                            |
| **Funktionstests**  | Manuelle Tests in Android Studio‑Emulator & realen Endgeräten |
| **Deployment**      | Firebase App Distribution                                     |

---

## 5 · Roadmap

1. Veröffentlichung im Google Play Store
2. iOS‑Version erstellen und im Apple App Store veröffentlichen
3. Erinnerung an Spieler\:innen: Lieblingsessensrichtung angeben (italienisch, griechisch, türkisch …)
4. Gastgeber\:innen rechtzeitig über mehrheitlich gewünschte Essensrichtung informieren, um passenden Lieferdienst auszuwählen
5. Spieler\:innen rechtzeitig das Menü des ausgewählten Lieferdienstes anzeigen, um Bestellung an Gastgeber\:in übermitteln zu können

---

## 6 · Lizenz

Dieses Projekt steht unter der **MIT‑License** – siehe [`LICENSE`](LICENSE). Du darfst den Code kostenlos verwenden, verändern und verbreiten, solange der ursprüngliche Copyright‑Hinweis erhalten bleibt.

---
