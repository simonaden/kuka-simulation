# 🤖 KukaRobo – Virtuelle Roboterzelle

> Ein **KUKA KR 800 fortec** als digitaler Zwilling in Unity – steuern, Posen teachen, Programme abspielen. Das Ziel: eine komplette, simulierte Roboterzelle.

![Unity](https://img.shields.io/badge/Unity-6000.3.6f1-black?logo=unity)
![Robot](https://img.shields.io/badge/Robot-KUKA%20KR%20800%20fortec-orange)
![Status](https://img.shields.io/badge/Status-in%20Entwicklung-yellow)

---

## ✨ Vision

Aus der Einzelrobotersimulation soll Schritt für Schritt eine **vollständige Roboterzelle** werden: Roboter, Werkstücke, Sicherheitstechnik und Ablaufsteuerung – alles virtuell, ohne Hardware-Risiko. Ideal zum Lernen, Programmieren und Ausprobieren von Abläufen, bevor sie an einen echten Roboter gehen.

## ✅ Aktueller Stand

| Feature | Beschreibung |
|---|---|
| 🎮 **Achssteuerung** | Alle 6 Achsen (A1–A6) per Slider im Spiel steuerbar, begrenzt auf die echten Gelenkgrenzen aus der URDF |
| 💾 **Posen speichern** | Aktuelle Stellung als Pose ablegen („Teachen“) |
| ▶️ **Programm abspielen** | Gespeicherte Posen nacheinander anfahren, mit Pause pro Pose und optionalem Loop |
| ⏹ **Stopp / Löschen** | Ablauf jederzeit abbrechen, Posenliste leeren |
| 🏠 **Homing** | Roboter per Knopfdruck in die Startpose (`0, -90, 90, 0, 0, 0`) fahren |
| ⚙️ **Realistische Dynamik** | `ArticulationBody`-Antriebe mit Steifigkeit, Dämpfung und Kraftlimits je Achse sowie begrenzter Achsgeschwindigkeit (Werte aus der URDF) |

### Bedienung

Im Play-Modus erscheint oben links ein Panel:

- **A1–A6** – Slider für die Zielwinkel der Achsen
- **Start** – Homing, fährt die Startpose an
- **Speichern** – aktuelle Pose zum Programm hinzufügen
- **Play** – gespeicherte Posen abfahren (wiederholt, wenn `loop` aktiv ist)
- **Stopp** – laufenden Ablauf abbrechen
- **Löschen** – alle Posen entfernen

Im Inspector des `MonoBehaviourScript` lassen sich u. a. `loop`, `pauseSeconds`, `homeAngles`, `maxSpeed` sowie die Antriebsparameter anpassen.

## 🗺️ Roadmap – die Zelle wächst

- [ ] **Pose-Persistenz** – Programme als Datei speichern und laden
- [ ] **Kartesische Steuerung** – TCP verfahren per Inverser Kinematik (statt nur Achswinkel)
- [ ] **Bewegungsarten** – PTP, LIN und CIRC wie auf der echten KUKA-Steuerung
- [ ] **Greifer / Werkzeug** – Tool am Flansch inkl. Greifen und Ablegen von Objekten
- [ ] **Werkstücke & Förderband** – Teile zuführen, aufnehmen, ablegen
- [ ] **Zellenaufbau** – Sicherheitszaun, Lichtschranken, Not-Halt
- [ ] **Ablaufsteuerung** – Ein-/Ausgänge (I/O), Zustandsautomat, Handshake mit der Peripherie
- [ ] **Kollisionserkennung** – Selbst- und Umgebungskollision
- [ ] **KRL-nahe Programmierung** – Programme im Stil der KUKA Robot Language
- [ ] **Bessere UI** – Teach-Pendant-Optik statt Debug-GUI
- [ ] **Anbindung** – ROS / OPC UA / reale Steuerung als Ausblick

## 🧰 Tech-Stack

- **Unity 6** (`6000.3.6f1`)
- **ArticulationBody** für die physikalisch korrekte Gelenkkette
- **[Unity URDF Importer](https://github.com/Unity-Technologies/URDF-Importer)** – Robotermodell aus `kr800.urdf`
- **Input System** (`com.unity.inputsystem`)

## 🚀 Loslegen

1. Repository klonen
2. Mit **Unity Hub** den Ordner öffnen (Unity `6000.3.6f1`, andere Version nur bei Bedarf)
3. Szene `Assets/Scenes/SampleScene.unity` öffnen
4. **Play** drücken und den Roboter steuern

## 📁 Projektstruktur

```
Assets/
├── Scenes/SampleScene.unity        # Hauptszene
└── kr800/
    ├── kr800.urdf                  # Robotermodell (KR 800 R2800 fortec)
    ├── kuka_fortec_support/meshes/ # Visual- (.dae) und Collision-Meshes (.stl)
    └── Scripts/                    # Steuerung, Teach & Playback
```

## 📜 Hinweis

Privates Lern- und Hobbyprojekt. KUKA ist eine Marke der KUKA AG; dieses Projekt steht in keiner Verbindung zu KUKA.
