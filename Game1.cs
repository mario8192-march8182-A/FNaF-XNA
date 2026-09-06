using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using FNaF_XNA.Systems;

namespace FNaF_XNA
{
    /// <summary>
    /// Main game logic class for Five Nights at Freddy's in XNA.
    /// Manages game state, animatronic behavior, and player interactions.
    /// </summary>
    public class Game1
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private ContentManager contentManager;
        private GameState gameState;
        private float nightTimer;
        private float nightDuration;
        private SpriteFont gameFont;

        // Game systems
        private AnimatronicSystem animatronicSystem;
        private CameraSystem cameraSystem;
        private AudioSystem audioSystem;
        private UISystem uiSystem;

        public enum GameState
        {
            Menu,
            Playing,
            GameOver,
            Won,
            Paused
        }

        public Game1(GraphicsDeviceManager graphics, ContentManager content)
        {
            graphicsDeviceManager = graphics;
            contentManager = content;
            gameState = GameState.Menu;
            nightTimer = 0f;
            nightDuration = 480f; // 8 minutes per night (in seconds)

            // Initialize game systems
            animatronicSystem = new AnimatronicSystem();
            cameraSystem = new CameraSystem(graphics);
            audioSystem = new AudioSystem();
            uiSystem = new UISystem();
        }

        public void Initialize()
        {
            // Set graphics device settings
            graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            graphicsDeviceManager.PreferredBackBufferHeight = 720;
            graphicsDeviceManager.IsFullScreen = false;
            graphicsDeviceManager.ApplyChanges();

            // Initialize all systems
            animatronicSystem.Initialize();
            cameraSystem.Initialize();
            audioSystem.Initialize();
            uiSystem.Initialize();
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            try
            {
                // Load font
                gameFont = contentManager.Load<SpriteFont>("Fonts/GameFont");

                // Load game assets
                animatronicSystem.LoadContent(contentManager);
                audioSystem.LoadContent(contentManager);
                uiSystem.LoadContent(contentManager);

                // Initialize UI system with spritebatch
                uiSystem.SetSpriteBatch(spriteBatch);
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Content Load Error: {ex.Message}");
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    UpdateMenu(keyboardState);
                    break;

                case GameState.Playing:
                    UpdateGameplay(gameTime, keyboardState);
                    break;

                case GameState.Paused:
                    UpdatePaused(keyboardState);
                    break;

                case GameState.GameOver:
                    UpdateGameOver(keyboardState);
                    break;

                case GameState.Won:
                    UpdateWon(keyboardState);
                    break;
            }
        }

        private void UpdateMenu(KeyboardState keyboardState)
        {
            // Press Enter to start game
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                gameState = GameState.Playing;
                nightTimer = 0f;
            }
        }

        private void UpdateGameplay(GameTime gameTime, KeyboardState keyboardState)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update night timer
            nightTimer += deltaTime;

            // Update all systems
            animatronicSystem.Update(gameTime, nightTimer, nightDuration);
            cameraSystem.Update(gameTime);
            audioSystem.Update(gameTime);
            uiSystem.Update(gameTime);

            // Check for pause input
            if (keyboardState.IsKeyDown(Keys.P))
            {
                gameState = GameState.Paused;
            }

            // Check win condition (survive the night)
            if (nightTimer >= nightDuration)
            {
                gameState = GameState.Won;
            }

            // Check loss condition
            if (animatronicSystem.IsGameOver())
            {
                gameState = GameState.GameOver;
            }
        }

        private void UpdatePaused(KeyboardState keyboardState)
        {
            // Press P to resume
            if (keyboardState.IsKeyDown(Keys.P))
            {
                gameState = GameState.Playing;
            }
        }

        private void UpdateGameOver(KeyboardState keyboardState)
        {
            // Press R to restart or Esc to menu
            if (keyboardState.IsKeyDown(Keys.R))
            {
                gameState = GameState.Menu;
                nightTimer = 0f;
                animatronicSystem.Reset();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                gameState = GameState.Menu;
            }
        }

        private void UpdateWon(KeyboardState keyboardState)
        {
            // Press any key to continue
            if (keyboardState.GetPressedKeys().Length > 0)
            {
                gameState = GameState.Menu;
                nightTimer = 0f;
                animatronicSystem.Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    DrawMenu(spriteBatch);
                    break;

                case GameState.Playing:
                    DrawGameplay(spriteBatch, gameTime);
                    break;

                case GameState.Paused:
                    DrawPaused(spriteBatch);
                    break;

                case GameState.GameOver:
                    DrawGameOver(spriteBatch);
                    break;

                case GameState.Won:
                    DrawWon(spriteBatch);
                    break;
            }
        }

        private void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameFont, "FIVE NIGHTS AT FREDDY'S", 
                new Vector2(400, 200), Color.Red);
            spriteBatch.DrawString(gameFont, "Press ENTER to Start", 
                new Vector2(450, 300), Color.White);
            spriteBatch.DrawString(gameFont, "Press ESC to Exit", 
                new Vector2(450, 350), Color.White);
        }

        private void DrawGameplay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw game scene
            animatronicSystem.Draw(spriteBatch);
            cameraSystem.Draw(spriteBatch);
            
            // Draw UI
            float timeRemaining = nightDuration - nightTimer;
            int hours = (int)(nightTimer / 60) + 12; // Starting at 12 AM
            int minutes = (int)(nightTimer % 60);
            
            spriteBatch.DrawString(gameFont, $"{hours:D2}:{minutes:D2}", 
                new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(gameFont, $"Time: {timeRemaining:F1}s", 
                new Vector2(10, 40), Color.White);
        }

        private void DrawPaused(SpriteBatch spriteBatch)
        {
            DrawGameplay(spriteBatch, null);
            spriteBatch.DrawString(gameFont, "PAUSED", 
                new Vector2(600, 300), Color.Yellow);
            spriteBatch.DrawString(gameFont, "Press P to Resume", 
                new Vector2(500, 350), Color.White);
        }

        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameFont, "GAME OVER", 
                new Vector2(500, 250), Color.Red);
            spriteBatch.DrawString(gameFont, "The animatronics got you!", 
                new Vector2(400, 300), Color.White);
            spriteBatch.DrawString(gameFont, "Press R to Restart or ESC for Menu", 
                new Vector2(350, 400), Color.White);
        }

        private void DrawWon(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameFont, "YOU SURVIVED!", 
                new Vector2(450, 300), Color.Green);
            spriteBatch.DrawString(gameFont, "Press any key to continue", 
                new Vector2(400, 400), Color.White);
        }
    }
}
