# Book Management System

My personal **Book Management System** for tracking and organizing PDF ebooks stored on my computer.

This project scans the local file system for `.pdf` files, stores their metadata and file locations, and provides a web interface to browse, search, and manage the collection.

---

## Architecture Overview

```
[ File System Scanner ] → [ SQLite Database ] → [ Web App UI ]
```

### 1. Scanner

A console application that:

* Recursively scans directories for `.pdf` files
* Collects file metadata (name, path, size, dates)
* Stores or updates records in the database

### 2. Data Store

* **SQLite** database
* Single file, no server required
* Designed for easy querying and future expansion

### 3. Web App

* ASP.NET Core (Razor Pages or MVC)
* Displays ebooks in a searchable, filterable table
* Uses file paths to open ebooks locally

---

## Planned Tech Stack

* **Scanner:** .NET Console Application
* **Backend:** ASP.NET Core
* **Database:** SQLite
* **Frontend:** Razor + Bootstrap

---

## Data Model (Initial)

Each book record includes:

* Id (GUID)
* FileName
* FullPath
* Directory
* SizeBytes
* LastModified
* AddedAt

---

## Project Structure

```
BookManagementSystem/
│
├── BMS.Scanner/
│   ├── BMS.Scanner.csproj
│   └── Program.cs
│
├── BMS.Web/
│   ├── BMS.Web.csproj
│   └── Pages/
│
├── BMS.Data/
│   ├── BMS.Data.csproj
│   └── books.db
│
├── BookManagementSystem.sln
└── README.md

```

---

## Roadmap

- [x] Scan local directories for PDF files (Console App)  
- [x] Store results in SQLite (`books.db`)  
- [x] Manual execution of scanner  
- [x] Web interface for browsing and searching (Razor Pages)  
- [ ] Detect new, updated, and deleted files automatically  
- [ ] Handle duplicates efficiently  
- [ ] Improve logging and performance  
- [ ] Folder-based filtering in the web UI  
- [ ] Open ebook from the web UI  

---

## Future Ideas

* Extract PDF metadata (title, author)
* Tags and categories
* Reading status / progress tracking
* Notes per book
* Cross-device syncing

---

## Status

🚧 **Early development / personal project**

This project is primarily a learning and productivity tool and will evolve over time.
