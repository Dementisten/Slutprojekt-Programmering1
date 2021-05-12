using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool play = true;
        bool explode = false;



        Texture2D player;
        Texture2D enemy;
        Texture2D explosion;
        Texture2D background;
        Texture2D bullet;


        Vector2 playerPos = new Vector2(100, 100);
        Vector2 enemyPos = new Vector2(1000, 500);
        Vector2 spacePos = new Vector2(0, 0);
        Vector2 explosionPos = new Vector2(0, 0);


        Vector2 enemySpeed = new Vector2(550, 550);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {


            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = this.Content.Load<Texture2D>("xwing");
            enemy = this.Content.Load<Texture2D>("tie");
            explosion = this.Content.Load<Texture2D>("explosion");
            background = this.Content.Load<Texture2D>("space");


        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //Player
            Rectangle playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, player.Width, player.Height);
            //Move
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A) && playerPos.X > 0)
            {
                playerPos.X = playerPos.X - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) && playerPos.X + player.Width < 1920)
            {
                playerPos.X = playerPos.X + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) && playerPos.Y > 0)
            {
                playerPos.Y = playerPos.Y - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) && playerPos.Y + player.Height < 1080)
            {
                playerPos.Y = playerPos.Y + 5;
            }

            //Enemy
            Rectangle enemyRect = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemy.Width, enemy.Height);

            int maxX = GraphicsDevice.Viewport.Width - enemy.Width;
            int maxY = GraphicsDevice.Viewport.Height - enemy.Height;

            enemyPos += enemySpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (enemyPos.X > maxX || enemyPos.X < 0)
            {
                enemySpeed.X *= -1;
            }
            if (enemyPos.Y > maxY || enemyPos.Y < 0)
            {
                enemySpeed.Y *= -1;
            }

            base.Update(gameTime);

            //Logik

            if(playerRect.Intersects(enemyRect))
            {
                explode = true;
                play = false;
            }

            if(play == false)
            {
                enemyPos.X = -200;
                enemyPos.Y = -200;
                playerPos.X = -100;
                playerPos.Y = -100;
            }

            //Score


        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Rita upp saker
            spriteBatch.Begin();
            spriteBatch.Draw(background, spacePos, Color.White);
            spriteBatch.Draw(player, playerPos, Color.White);
            spriteBatch.Draw(enemy, enemyPos, Color.White);
            if (explode == true)
            {
                Rectangle rect = new Rectangle((int)0, (int)0, (int)1920, (int)1080);
                spriteBatch.Draw(explosion, rect, Color.White);
            }
                

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
