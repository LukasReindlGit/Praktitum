# Praktitum game

Praktitumspiel 2017

## Allgemeine Git bash befehle

`git fetch` lädt den aktuellen Stand vom Server.

`git status`
zeigt an in welchem Branch man sich befindet und wie der Zustand im vergleich zu origin ist

`git add <filename>` fügt eine bestimmte Datei zum nächsten Commit hinzu. Mit `git add .` werden alle Änderungen zum Commit hinzugefügt.

`git commit -m "<commit message>" ` erstellt einen lokalen Wiederherstellungspunkt.

`git push` lädt alle lokale Commits auf den aktuellen Branch auf den Server.

`git pull` lädt alle Commits vom Server herunter. (Aktualisiert den lokalen Branch)

`git checkout <branch name>` wechsel zu anderem Branch.

`git checkout -b <feature/branch-name>` erstellt einen neuen Branch, basierend auf den aktuellen.

`git merge feature/blabla` zieht sich alle commits von blabla und fügt sie dem eigenen Branch hinzu.

`git pull feature/blabla` zieht sich alle dateien von blabla und fügt sie dem eigenen Branch hinzu.