using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Tools
{
    public class Font
    {
        public Dictionary<char, Vector2> Characters { get; private set; } = new Dictionary<char, Vector2>();

        public Color[] fontTextureData;
        public Texture2D fontMasterTexture;

        public GraphicsDevice GraphicsDevice;

        public Font(Texture2D fontMasterTexture, GraphicsDevice graphicsDevice)
        {
            this.fontTextureData = new Color[(int)fontMasterTexture.Width*(int)fontMasterTexture.Height];
            fontMasterTexture.GetData<Color>(this.fontTextureData);
            this.fontMasterTexture = fontMasterTexture;
            this.GraphicsDevice = graphicsDevice;
            GenerateCharOffsets();
        }

        private void GenerateCharOffsets()
        {
            int count = 65;
            bool completeRow = false;
            int start = -1;

            //find first col with black, then find nex col with no black
            for (int col = 0; col < fontMasterTexture.Width; col++)
            {
                 for (int row = 0; row < fontMasterTexture.Height; row++)
                 {
                    if (fontTextureData[col + (row * fontMasterTexture.Width)] == Color.Black)
                    {
                        start = start == -1 ? col : start;
                        completeRow = false;
                        break;
                    }
                    else if (fontTextureData[col + (row * fontMasterTexture.Width)] != Color.Black) completeRow = true;
                 }
                if (completeRow)
                {
 
                    Characters.Add((char)count, new Vector2(start, col));
                  
                    count++;
                    start = -1;
                    completeRow = false;
                }
            }

        }

        /// <summary>
        /// Adds a charicter definition to the font
        /// </summary>
        /// <param name="pos">x = the min index of the char, y= the max index of the char</param>
        public void AddChar(char character, Vector2 pos)
        {
            Characters.Add(character, pos);
        }

        public Texture2D CreateTextureFromString(string text)
        {
            float widthOfOutput = 0;
            Color[] data;

            foreach (char c in text) {
                 widthOfOutput += Characters[c].Y - Characters[c].X;
            }
            widthOfOutput += text.Length;
            data = new Color[(int)((widthOfOutput) * fontMasterTexture.Height)];
            int offset = 0;
            foreach (char c in text)
            {       
                for (int row = 0; row < fontMasterTexture.Height; row++)
                {
                    for (int col = (int)Characters[c].X; col < Characters[c].Y; col++)
                    {
                        try
                        {
                            data[(row) * (int)widthOfOutput + offset + col - (int)Characters[c].X] = fontTextureData[row * fontMasterTexture.Width + col];
                        }
                        catch (Exception)
                        {

                        }
                    }                      
                }
                offset += ((int)Characters[c].Y- (int)Characters[c].X)+1;
            }
            Texture2D output = new Texture2D(GraphicsDevice, (int)widthOfOutput, fontMasterTexture.Height);
            output.SetData<Color>(data);
            return output;
        }

    }
}
