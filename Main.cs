using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ezkiv
{    
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private StorageDevice storageDevice;

        Texture2D dotSpr;
        Texture2D leftSpr;
        Texture2D upSpr;
        Texture2D rightSpr;
        Texture2D downSpr;
        Texture2D objspr;
        Texture2D playerSpr;
        Texture2D background;
        Texture2D numbFont;
        Texture2D letFont;

        bool difficulty;//0-easy,1-hard
        int score;
        int topScore;

        Display8Seg scoreSpr;
        Display8Seg topScoreSpr;
        Display8Seg scoreLet;
        Display8Seg topScoreLet;

        List<GameObject> objs;

        Random rand;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Zune.
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0);

            IAsyncResult r = Guide.BeginShowStorageDeviceSelector(null, null);
            while (!r.IsCompleted) { }
            storageDevice = Guide.EndShowStorageDeviceSelector(r); 

        }

        protected override void Initialize()
        {

            base.Initialize();
            rand = new Random();
            objs = new List<GameObject>();
            objs.Add(new Player(playerSpr,upSpr,rightSpr,leftSpr,downSpr,"player"));
            objs.Add(new Goal(new Rectangle(0,0,225,305),objspr, "goal"));
            score = 0;
            loadScore();
            scoreSpr = new Display8Seg(new Vector2(198,20),numbFont,letFont);
            scoreLet = new Display8Seg(new Vector2(229, 20), numbFont, letFont);
            topScoreSpr = new Display8Seg(new Vector2(17,20), numbFont, letFont);
            topScoreLet = new Display8Seg(new Vector2(48, 20), numbFont, letFont);
            scoreSpr.setDigit(score);
            topScoreSpr.setDigit(topScore);
            scoreLet.setLetter("scre");
            topScoreLet.setLetter("best");

            //string name = "ball";
            /*for (int i = 0; i < 5; i++)
            {
                objs.Add(new Ball(rand.Next(272), rand.Next(480), rand.Next(8), rand.Next(8), objspr, name));
            }*/
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager content = new ContentManager(Services, "Content");
            leftSpr = content.Load<Texture2D>("Up");
            rightSpr = content.Load<Texture2D>("Down");
            upSpr = content.Load<Texture2D>("Right");
            downSpr = content.Load<Texture2D>("Left");
            dotSpr = content.Load<Texture2D>("dot");
            objspr = content.Load<Texture2D>("ball");
            playerSpr = content.Load<Texture2D>("Player");
            background = content.Load<Texture2D>("Background");
            numbFont = content.Load<Texture2D>("numbersFont");
            letFont = content.Load<Texture2D>("lettersFont");

        }
        protected override void UnloadContent()
        {
            saveScore();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            int collision = 0;
            foreach (GameObject obj in objs)
            {
                obj.update();
                collision += obj.collide(objs[0]);
                
            }
            if (collision == 1)
            {
                int v = 5;
                Ball temp;
                if (rand.Next(2) == 1)
                    v = -5;
                do
                {
                    if (rand.Next(2) == 1)
                        temp = new Ball(rand.Next(230) , rand.Next(310), v, 0, objspr, "ball");
                    else
                        temp = new Ball(rand.Next(230), rand.Next(310), 0, v, objspr, "ball");
                } while (temp.collide(objs[0]) != 0);
                objs.Add(temp);
                score += 5;
                scoreSpr.setDigit(score);
                scoreLet.setLetter("scre");
                topScoreLet.setLetter("best");
                if (score > topScore)
                {
                    topScore = score;
                    topScoreSpr.setDigit(topScore);
                }
            }
            if (collision >= 2)
            {
                GameObject player = objs[0];
                GameObject goal = objs[1];
                objs.Clear();
                objs.Add(player);
                objs.Add(goal);
                topScoreLet.setDigit(score);
                scoreLet.setLetter("game");
                scoreSpr.setLetter("over");
                score = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //spriteBatch.Draw(dotSpr, new Rectangle(100, 100, 30, 15), Color.Red);
            //spriteBatch.Draw(objspr, new Rectangle(100, 200, 10, 10), Color.White);
            //spriteBatch.Draw(background, new Vector2(), Color.SteelBlue);
            foreach (GameObject obj in objs)
            {
                obj.Draw(spriteBatch);
            }
            /*scoreSpr.Draw(spriteBatch);
            topScoreSpr.Draw(spriteBatch);
            scoreLet.Draw(spriteBatch);
            topScoreLet.Draw(spriteBatch);*/
            spriteBatch.End();

            base.Draw(gameTime);
        }
        void saveScore()
        {
            using (StorageContainer container = storageDevice.OpenContainer("Ezkiv"))
            {
                string fullPath = Path.Combine(container.Path, "highScore.txt");
                StreamWriter writer = new StreamWriter(fullPath, false);
                writer.WriteLine(topScore);
                writer.Close();
            }
        }
        void loadScore()
        {
            using (StorageContainer container = storageDevice.OpenContainer("Ezkiv"))
            {
                string fullPath = Path.Combine(container.Path, "highScore.txt");
                if (File.Exists(fullPath))
                {
                    StreamReader reader = new StreamReader(fullPath);
                    String scoreString = reader.ReadLine();
                    topScore = Convert.ToInt32(scoreString);
                    reader.Close();
                }
                else
                {
                    topScore = 0;
                }
            }
        }
    }
}
