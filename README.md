# BackScene

BackScene is a tool designed to display video wallpapers on your Windows desktop background using MPV and weebp.

<p align="center">
  <img src="https://github.com/user-attachments/assets/4e662b48-062e-4a5e-95b4-bb13ee1219ac" alt="Icon" width="100"/>
</p>

<p align="center">
  <img src="https://img.shields.io/github/v/release/ProbablyXS/BackScene" alt="GitHub Release"/>
  <img src="https://img.shields.io/github/downloads/ProbablyXS/BackScene/total" alt="GitHub Downloads"/>
  <a href="https://github.com/ProbablyXS/BackScene/issues">
    <img src="https://img.shields.io/github/issues/ProbablyXS/BackScene" alt="GitHub Issues"/>
  </a>
  <a href="https://github.com/ProbablyXS/BackScene/network">
    <img src="https://img.shields.io/github/forks/ProbablyXS/BackScene" alt="GitHub Forks"/>
  </a>
  <a href="https://github.com/ProbablyXS/BackScene/stargazers">
    <img src="https://img.shields.io/github/stars/ProbablyXS/BackScene" alt="GitHub Stars"/>
  </a>
</p>

## Information

- **Drag and drop** to change the wallpaper folder.

## [CONFIG.INI]
- [BackScene]
- fps=60
- start_minimized=false
- close_minimizes=false
- clean_memory=false
- start_with_windows=false
- show_logs=false
- limit_fps=false
- play_at_startup=false
- shuffle=false
- mute_audio=false
- wallpaperPath=

## Showcase

<p align="center">
  <img src="https://github.com/user-attachments/assets/88938ef4-c028-4d37-bd5e-fd93391f24e6" alt="Showcase Image 1" width="300"/>
  <img src="https://github.com/user-attachments/assets/144b425b-8dd8-4e62-99cd-62baf5a170f4" alt="Showcase Image 2" width="300"/>
</p>

## Features

- **Display video wallpapers** on your Windows desktop background.
- **Utilizes MPV** for video playback.
- **Uses weebp** for WebP image decoding.

##### BackScene Settings

- **Show logs**: Display the console logs.
- **Start minimized**: Start BackScene in a minimized state.
- **Close minimizes**: Close BackScene when minimized.
- **Clean memory**: Clean process memory for "BackScene" and "MPV".
- **Start with windows**: Launch BackScene at Windows startup.

##### MPV Settings

- **Mute audio**: Mute the audio for the video running.
- **Shuffle**: Start the video randomly from the playlist folders.
- **Play at startup**: Start MPV when BackScene is running.
- **Limit FPS**: Limit the frame rate of the video.

##### Systray Options

Right-click on the BackScene icon in the system tray to access the following options:

- **Start**: Start MPV video.
- **Stop**: Stop MPV video.
- **Next**: Play the next video.
- **Previous**: Play the previous video.
- **Play**: Play the current video.
- **Pause**: Pause the current video.
- **Mute**: Mute the video.
- **Unmute**: Unmute the video.
- **Show**: Display the BackScene program.
- **Settings**: Open the settings menu.
- **Help**: Open the GitHub page for help.
- **Exit**: Exit the program.

## Installation

### Prerequisites

- **Windows 10** (Tested environment)
- [MPV](https://mpv.io/)
- [weebp](https://github.com/Francesco149/weebp)

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/BackScene.git
