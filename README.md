# Special Delivery

Special Delivery is a rhythm-based game where players timing-click and interact with incoming beats to deliver packages.

![Special Delivery Screenshot](https://i.postimg.cc/gJ3FFzSz/Screenshot-2026-11-05-125055.png)

## Overview

In **Special Delivery**, core gameplay centers on rhythmic precision. Players must match inputs to the beat of the music to navigate obstacles and ensure successful deliveries.

* **Genre:** Rhythm Game
* **Audio Engine:** FMOD integration for dynamic music, beat synchronization, and sound effects

## Gameplay Mechanics

* **Beat Synchronization:** Timing-based actions driven directly by FMOD audio events.
* **Dynamic Audio:** Music stems and interactive SFX adapt based on player accuracy and combo streaks.

## Development & Architecture

The project decouples gameplay events from sound playback using FMOD's event-driven architecture.

![Development Screenshot](https://i.postimg.cc/TPzM4L9p/Screenshot-2026-11-05-125100.png)

## Tech Stack

* **Engine:** Unity / Unreal Engine 
* **Audio:** FMOD Studio & FMOD API
