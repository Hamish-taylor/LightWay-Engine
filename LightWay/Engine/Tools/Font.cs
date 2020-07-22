using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
            StreamReader fontFile = null;
            try
            {   // Open the text file using a stream reader.
                using (fontFile = new StreamReader("Fonts/Default.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    Console.WriteLine(fontFile);


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
                            else if (fontTextureData[col + (row * fontMasterTexture.Width)] != Color.Black && start != -1) completeRow = true;
                        }
                        if (completeRow)
                        {

                            Characters.Add((char)(fontFile.Read()), new Vector2(start, col));

                            count++;
                            start = -1;
                            completeRow = false;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message); 
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

        public Texture2D CreateTextureFromString(string text,int wordPixelSpacing,int letterPixelSpacing, Color color)
        {
            float widthOfOutput = 0;
            Color[] data;

            foreach (char c in text) {
                if (c == ' ') widthOfOutput += wordPixelSpacing;
                else widthOfOutput += Characters[c].Y - Characters[c].X;
            }

            widthOfOutput += text.Length*letterPixelSpacing;
            data = new Color[(int)((widthOfOutput) * fontMasterTexture.Height)];
            
            int offset = 0;

            foreach (char c in text)
            {
                if(c != ' ')
                {
                    for (int row = 0; row < fontMasterTexture.Height; row++)
                    {
                        for (int col = (int)Characters[c].X; col < (int)Characters[c].Y; col++)
                        {
                            try
                            {
                                data[(row) * (int)widthOfOutput + offset + col - (int)Characters[c].X] = fontTextureData[row * fontMasterTexture.Width + col] == Color.Black ? color: Color.Transparent;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }       
                    
                    offset += (int)Characters[c].Y - (int)Characters[c].X;
                    InsertSpace(data, offset, (int)widthOfOutput, letterPixelSpacing);
                    offset += letterPixelSpacing;

                } else
                {
                    InsertSpace(data, offset, (int)widthOfOutput, wordPixelSpacing);
                    offset += wordPixelSpacing;
                }            
            }
            Texture2D output = new Texture2D(GraphicsDevice, (int)widthOfOutput, fontMasterTexture.Height);
            output.SetData<Color>(data);
            return output;
        }
        private void InsertSpace(Color[] data,int offset,int widthOfOutput, int spacePixelLen)
        {
            for(int row = 0; row < fontMasterTexture.Height; row++)
            {
                for (int col = offset; col < offset+ spacePixelLen; col++)
                {
                    data[(row) * widthOfOutput + col] = Color.Transparent;
                }
            }


        }


        /*public Texture2D CreateTextureFromString(string text, int wordPixelSpacing, int letterPixelSpacing)
        {
            float widthOfOutput = 0;
            Color[] data;

            letterPixelSpacing -= letterPixelSpacing > 0 ? 1 : 0; //The font texture already has a 1 pixle spacing built in, cannot have a smaller then one space

            foreach (char c in text)
            {
                if (c == ' ') widthOfOutput += wordPixelSpacing;
                else widthOfOutput += Characters[c].Y - Characters[c].X;
            }
            widthOfOutput += text.Length * letterPixelSpacing;
            data = new Color[(int)((widthOfOutput) * fontMasterTexture.Height)];
            int offset = 0;
            foreach (char c in text)
            {
                int x = c == ' ' ? 0 : (int)Characters[c].X;
                int y = c == ' ' ? wordPixelSpacing - 1 : (int)Characters[c].Y;
                for (int row = 0; row < fontMasterTexture.Height; row++)
                {
                    for (int col = x; col < y; col++)
                    {
                        try
                        {
                            data[(row) * (int)widthOfOutput + offset + col - x] = c == ' ' ? Color.Transparent : fontTextureData[row * fontMasterTexture.Width + col];
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                offset += y - x;
            }
            Texture2D output = new Texture2D(GraphicsDevice, (int)widthOfOutput, fontMasterTexture.Height);
            output.SetData<Color>(data);
            return output;
        }*/
    }
}
