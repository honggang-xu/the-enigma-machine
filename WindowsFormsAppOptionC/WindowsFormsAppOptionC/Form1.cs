using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsAppOptionC
{
    public partial class Form1 : Form
    {
        //three arrays to store three rotors
        string[] rotorsLargeArray;
        string[] rotorsMiddleArray;
        string[] rotorsSmallArray;
        
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load rotors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadRotors_Click(object sender, EventArgs e)
        {
            
            //set filter for dialog control
            const string FILTER = "CSV File|*.csv";
            openFileDialog1.Filter = FILTER;
            //declare variables
            StreamReader inputFile;
            string line;
            //if user selectes file and clicks on OK button
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open the selected file
                inputFile = File.OpenText(openFileDialog1.FileName);
                //while not the end of file
                while (!inputFile.EndOfStream)
                {
                    //read one line from the file
                    line = inputFile.ReadLine();
                    //store string in a array
                    rotorsLargeArray = line.Split(',');
                    //read one line from the file
                    line = inputFile.ReadLine();
                    //store string in a array
                    rotorsMiddleArray = line.Split(',');
                    //read one line from the file
                    line = inputFile.ReadLine();
                    //store string in a array
                    rotorsSmallArray = line.Split(',');
                }
                //close the file
                inputFile.Close();
            }
        }
        /// <summary>
        /// decode the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDecodeFile_Click(object sender, EventArgs e)
        {
            //set filter for dialog control
            const string FILTER = "Text File|*.txt";
            openFileDialog1.Filter = FILTER;
            saveFileDialog1.Filter = FILTER;
            //declare variables
            StreamReader inputFile;
            StreamWriter outputFile;
            string line;
            string character;

            //try catch method
            try
            {
                //if user selectes file and clicks on OK button
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //open the selected file
                    inputFile = File.OpenText(openFileDialog1.FileName);
                    //if user selectes file and clicks on OK button
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //create the file
                        outputFile = File.CreateText(saveFileDialog1.FileName);
                        //while not the end of file
                        while (!inputFile.EndOfStream)
                        {
                            //read one line from the file
                            line = inputFile.ReadLine();
                            //for each character
                            for (int i = 0; i < line.Length; i++)
                            {
                                //store the current character into a variable
                                character = line.Substring(i, 1);
                                //call DecodeLetter method to decode the character
                                string decodedCharacter = DecodeLetter(character);
                                //shuffle the rotors
                                ShuffleRotors();
                                //write the decoded character to console
                                Console.Write(decodedCharacter);
                                //write the decode character to the new file
                                outputFile.Write(decodedCharacter);
                            }
                        }
                        Console.WriteLine("");
                        //close the input file and output file
                        inputFile.Close();
                        outputFile.Close();
                    }

                }
            }
            //catch error
            catch (Exception ex)
            {
                //show error message
                MessageBox.Show(ex.Message);
            }
            

        }
        /// <summary>
        /// decode the current letter
        /// </summary>
        /// <param name="character">the current letter to decode</param>
        /// <returns></returns>
        private string DecodeLetter(string character)
        {
            //declare variables
            string letterOnMiddle = "";
            string letterOnSmall = "";

            //loop through the large rotors
            for (int i = 0; i < rotorsLargeArray.Length; i++)
            {
                //if the character is found on large rotor
                if (rotorsLargeArray[i] == character)
                {
                    //store the character at the same position on the middle rotor
                    letterOnMiddle = rotorsMiddleArray[i];
                }
            }
            //loop through the large rotors
            for (int count = 0; count < rotorsLargeArray.Length; count++)
            {
                //if the character on the middle rotor is found on large rotor
                if (rotorsLargeArray[count] == letterOnMiddle)
                {
                    //store the character at the same position on the small rotor
                    letterOnSmall = rotorsSmallArray[count];
                }
            }
            //return character on the small rotor
            return letterOnSmall;
        }
        /// <summary>
        /// shuffle the rotors
        /// </summary>
        private void ShuffleRotors()
        {
            //if the space gets to the end of the middle rotor
            if (rotorsMiddleArray[rotorsMiddleArray.Length - 1] == " ")
            {
                //store the current last character in large rotor into a variable
                string lastLargeCharacter = rotorsLargeArray[rotorsLargeArray.Length - 1];
                //for each character to shuffle starting from the end
                for (int i = rotorsLargeArray.Length - 1; i > 0; i--)
                {
                    //current character equals the character before it
                    rotorsLargeArray[i] = rotorsLargeArray[i - 1];
                }
                //shift the stored last character to the first character in the array
                rotorsLargeArray[0] = lastLargeCharacter;
            }

            //if the space gets to the end of the small rotor
            if (rotorsSmallArray[rotorsSmallArray.Length - 1] == " ")
            {
                //store the current last character in middel rotor into a variable
                string lastMiddleCharacter = rotorsMiddleArray[rotorsMiddleArray.Length - 1];
                //for each character to shuffle starting from the end
                for (int i = rotorsMiddleArray.Length - 1; i > 0; i--)
                {
                    //current character equals the character before it
                    rotorsMiddleArray[i] = rotorsMiddleArray[i - 1];
                }
                //shift the stored last character to the first character in the array
                rotorsMiddleArray[0] = lastMiddleCharacter;
            }

            //store the current last character into a variable
            string lastSmallCharacter = rotorsSmallArray[rotorsSmallArray.Length - 1];
            //for each character to shuffle starting from the end
            for (int i = rotorsSmallArray.Length - 1; i > 0; i--)
            {
                ////current character equals the character before it
                rotorsSmallArray[i] = rotorsSmallArray[i - 1];
            }
            //shift the stored last character to the first character in the array
            rotorsSmallArray[0] = lastSmallCharacter;
        }
        /// <summary>
        /// encrypt the current character
        /// </summary>
        /// <param name="character">the current character to encrypt</param>
        /// <returns></returns>
        private string EncryptLetter(string character)
        {
            //declare variables
            string LetterOnLarge = "";
            string encryptedLetter = "";
            //loop through the small rotors
            for (int i = 0; i < rotorsSmallArray.Length; i++)
            {
                //if the character is found on small rotor
                if (rotorsSmallArray[i] == character)
                {
                    //store the character at the same position on the large rotor
                    LetterOnLarge = rotorsLargeArray[i];
                }
            }
            //loop through the middle rotors
            for (int count = 0; count < rotorsMiddleArray.Length; count++)
            {
                //if the character is found on middle rotor
                if (rotorsMiddleArray[count] == LetterOnLarge)
                {
                    ////store the character at the same position on the large rotor
                    encryptedLetter = rotorsLargeArray[count];
                }
            }
            //return the character on the large rotor
            return encryptedLetter;
        }
        /// <summary>
        /// encrypt the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            //set filter for dialog control
            const string FILTER = "Text File|*.txt";
            openFileDialog1.Filter = FILTER;
            saveFileDialog1.Filter = FILTER;
            //declare variables
            StreamReader inputFile;
            StreamWriter outputFile;
            string line;
            string character;

            //try catch method
            try
            {
                //if user selectes file and clicks on OK button
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //open the selected file
                    inputFile = File.OpenText(openFileDialog1.FileName);
                    //if user selectes file and clicks on OK button
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //create the file
                        outputFile = File.CreateText(saveFileDialog1.FileName);
                        //while not the end of the file
                        while (!inputFile.EndOfStream)
                        {
                            ////read one line from the file
                            line = inputFile.ReadLine();
                            //for each character
                            for (int i = 0; i < line.Length; i++)
                            {
                                //store the current character into a variable
                                character = line.Substring(i, 1);
                                //call EncryptLetter method to encrypt the character
                                string encryptedCharacter = EncryptLetter(character);
                                //write the encrypted character to console
                                Console.Write(encryptedCharacter);
                                //write the encrypted character to file
                                outputFile.Write(encryptedCharacter);
                                //shuffle rotors
                                ShuffleRotors();
                            }
                        }
                        Console.WriteLine("");
                        //close the input file and output file
                        inputFile.Close();
                        outputFile.Close();
                    }

                }
            }
            //catch error
            catch (Exception ex)
            {
                //show error message
                MessageBox.Show(ex.Message);
            }
        }
    }
}
