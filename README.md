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

[BackScene]
- display=0
- clean_memory=true
- show_logs=false
- start_minimized=true
- close_minimizes=true
- start_with_windows=true
- wallpaperPath=C:\Users\Admin\Videos\wallpapers\zidpVl1rabzWFWk_Yae Miko 4K Live Wallpaper.mp4

[Mpv]
- mute_audio=true
- shuffle=true
- play_at_startup=true
- limit_fps=true
- fps=30
- hardware_acceleration=true
- pause_on_fullscreen=true



## Showcase

<p align="center">
  <img src="https://github.com/user-attachments/assets/9c12cf07-9678-4892-b8ee-9511a65ebbe6" alt="Showcase Image 1" width="300"/>
  <img src="https://github.com/user-attachments/assets/945df96f-f4ed-48c5-9ee9-f3efb563ce27" alt="Showcase Image 2" width="300"/>
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

- **Windows 11** (Tested environment)
- [MPV](https://mpv.io/)
- [weebp](https://github.com/Francesco149/weebp)

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/BackScene.git


## Crossfade Script (Optional)

You can enhance your wallpapers with a smooth **fade in/out + zoom + blur** effect using MPV scripts. Place the script in the `BackScene/tools/mpv/scripts/` folder.

### crossfade_clean.lua

```lua
-- crossfade_clean.lua
local fade_duration = 0.5
local zoom_factor = 1.03
local blur_sigma = 0.8  -- gblur sigma, plus propre que boxblur

local function apply_vf()
    -- Guard: ne rien faire si pas de piste vidéo
    local vid = mp.get_property("vid")
    if not vid or vid == "no" then return end

    local duration = mp.get_property_number("duration")
    if not duration or duration <= fade_duration * 2 then return end

    local fade_out_start = duration - fade_duration

    -- Scale en entier pour éviter les artefacts de dimension impaire
    -- on utilise trunc(iw*zoom) pour rester sur des valeurs paires
    local vf_str = string.format(
        "scale=trunc(iw*%f/2)*2:trunc(ih*%f/2)*2," ..
        "gblur=sigma=%f," ..
        "fade=t=in:st=0:d=%f:alpha=0," ..
        "fade=t=out:st=%f:d=%f:alpha=0",
        zoom_factor, zoom_factor,
        blur_sigma,
        fade_duration,
        fade_out_start, fade_duration
    )

    -- "vf set" remplace tout d'un coup, sans besoin de clr séparé
    local ok, err = pcall(function()
        mp.commandv("vf", "set", vf_str)
    end)

    if ok then
        mp.msg.info("Filtres appliqués: " .. vf_str)
    else
        mp.msg.warn("Échec application filtres: " .. tostring(err))
    end
end

mp.register_event("file-loaded", function()
    -- 0.5s de délai pour laisser mpv initialiser la piste vidéo
    mp.add_timeout(0.5, apply_vf)
end)

mp.msg.info("crossfade_clean.lua actif")
