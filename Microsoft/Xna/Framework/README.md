# Microsoft XNA Framework References

This directory contains documentation and references for the Microsoft XNA Framework integration with the FNaF game.

## Included References

- **Game.cs** - Base game class for XNA Framework
- **GraphicsDeviceManager.cs** - Graphics device management
- **SpriteBatch.cs** - 2D sprite rendering
- **Content Management** - Asset loading and management
- **Input Handling** - Keyboard, mouse, and gamepad input
- **GameTime** - Game timing and delta time

## Key Components

### Graphics
- Viewport and display management
- Sprite rendering and transformations
- Texture loading and management

### Game Loop
- Initialize() - One-time initialization
- LoadContent() - Asset loading
- Update(GameTime) - Game logic updates
- Draw(GameTime) - Rendering

### Input System
- KeyboardState - Keyboard input
- MouseState - Mouse input
- GamePadState - Controller input

### Content Pipeline
- ContentManager - Asset management
- Texture2D - 2D textures
- SpriteFont - Font rendering
- SoundEffect - Audio playback

## Architecture Overview

```
AppMain (Inherits from Game)
  ├── Graphics Management
  ├── Content Loading
  ├── Game1 (Core Logic)
  │   ├── AnimatronicSystem
  │   ├── CameraSystem
  │   ├── AudioSystem
  │   └── UISystem
  └── Xbox Live Integration (Optional)
```

## Additional Resources

For more information about XNA Framework, visit:
- https://en.wikipedia.org/wiki/XNA
- XNA Creator's Club documentation
- Monogame (XNA successor): https://www.monogame.net/

## Notes

- This project uses XNA Framework 4.0
- Compatible with Windows and Xbox 360
- Direct3D based graphics rendering
