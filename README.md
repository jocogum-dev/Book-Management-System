# Book Management System

A self-hosted digital library platform for managing, browsing, and reading PDF ebooks.

---

## Vision

* Centralized library of PDF ebooks
* User accounts with personal book collections
* Fully self-hosted and extensible

## Architecture Overview

```
[ File System Scanner ] → [ SQLite Database ] → [ Web App UI ]
                                             ↓
                                        [ User Data ]
```

### 1. Scanner

A console application that:

* Recursively scans directories for `.pdf` files
* Collects file metadata (name, path, size, dates)
* Inserts, updates, and removes book records
* Acts as the **source of truth** for the library shelf

### 2. Data Store

* **SQLite** database
* Stores both library and user data

### 3. Web App

* ASP.NET Core (Razor Pages)
* Public library-style browsing experience
* Displays ebooks in a searchable, filterable table
* Uses file paths to open ebooks locally
* Authenticated user features (favorite books)

---

## Tech Stack

* **Scanner:** .NET Console Application
* **Backend:** ASP.NET Core
* **Database:** SQLite
* **Frontend:** Razor + Bootstrap
* **Auth:** ASP.NET Core Identity

---
## Core Concepts

### Library Shelf

The BookFile table represents the global library:

* All scanned books live here
* Read-only for users
* Managed by the scanner

Users browse this shelf and add books to their personal collection.

### User Collection

Each user has a personal collection:

* References books from the library shelf
* Supports favorite books

---

## Data Model (Initial)

### BookFile

* Id (GUID)
* FileName
* FullPath
* Directory
* SizeBytes
* LastModified
* AddedAt

### User

* Id
* Username / Email
* PasswordHash
* CreatedAt

### UserCollection (Planned)

* Id
* UserId
* BookFileId
* IsFavorite
* AddedAt
* LastOpenedAt

---

## Roadmap

### Phase 1 - Core Library
- [x] Scan local directories for PDF files (Console App)
- [x] Store results in SQLite (`books.db`)
- [x] Show all books
- [x] Admin dashboard (library summary)
- [x] Open ebook from the web UI

### Phase 2 - Library UX
- [x] Filter and search books

### Phase 3 - Users & Auth
- [ ] Add user authentication
- [ ] Add admin role
- [ ] Restrict dashboard to admin users only
- [ ] Hide / unhide books (admin)

### Phase 4 - Scanner Control
- [ ] Extend scanner from console app to background service
- [ ] Scan folders, sync records, remove orphaned records
- [ ] Allow admin to trigger scanner service manually

### Phase 5 - User Features
- [ ] User collections
- [ ] Favorites

### Phase 6 - Automation & Polish
- [ ] Detect new, updated, and deleted files automatically
- [ ] Handle duplicates efficiently
- [ ] Improve logging and performance

---

### Roles

### User

* Browse books
* Add/remove from collection
* Favorite books
* Bookmark pages (future)

### Admin

* Everything user can do
* Curate library
* Resolve scanner issues
* Control scan execution

---

## Status

🚧 **Early development / personal project**

This project is a learning-focused, self-hosted system that will evolve into a full personal digital library.
