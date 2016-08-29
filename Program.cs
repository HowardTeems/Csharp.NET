// C# console-run program takes path of comma-delimited text file as argument and
// performs a sort on 3 fields, generating the result in a file <name>-graded.txt
// Programmer: Howard Teems  Released: 30 August 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;  // Added to perform text file I/O

namespace TRANSMAXrequirements1
{
    // Data will be stored in Person objects, which will be added to a List<Person>.
    class Person : IComparable  // Interface lets us compare one Person to another. 
    {
        protected string surname;
        protected string firstname;
        protected int score;

        public int CompareTo(object obj)  //  This method Must be overloaded.
        {
            if (obj == null) return 1;

            Person bPerson = obj as Person;
            if (bPerson != null)
                // Modify this code to change the ordering of the sort.
                if (this.score != bPerson.score)
                {
                    return bPerson.score.CompareTo(this.score);  // Sort score descending.
                }
                else if (this.surname != bPerson.surname)
                {
                    return this.surname.CompareTo(bPerson.surname);
                }
                else
                {
                    return this.firstname.CompareTo(bPerson.firstname);
                }
                    

            else
                throw new ArgumentException("Object is not a Person");
        }

        public string GetPersonData()
        {
            return this.surname + ", " + this.firstname + ", " + this.score;
        }

        public Person(string strName1, string strName2, int intScore)
        {
            this.surname = strName1;
            this.firstname = strName2;
            this.score = intScore;
        }
    }
    
    class Program
    {
        static int Main(string[] args)  // Entry point
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the path to a file as an argument.");
                Console.WriteLine("Usage: TRANSMAXrequirements1 file-path");
                return 1;
            }
            string strFileIn = args[0];
 
            List<Person> listPersons = new List<Person>();
            
            try
            {   
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(strFileIn))
                {
	                // Read each stream record into a Person object.
                    while (!sr.EndOfStream)
                    {
                        string strRec = sr.ReadLine();
                        string[] strData = strRec.Split(',');

                        try
                        {
                            // Remove leading whitespace.
                            Person aPerson = new Person(
                                strData[0].TrimStart(),
                                strData[1].TrimStart(),
                                Convert.ToInt32(strData[2]));
                            listPersons.Add(aPerson);
                        }
                        // Skip bad record, or line, in input file.
                        catch {}
                    }
                }
            }
            
            catch (Exception e)
            {
                Console.WriteLine("The file " + strFileIn + " could not be read:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press <Enter> to continue.");
                Console.ReadLine();
                return 1;
            }

            //Console.WriteLine("Count = " + listPersons.Count);
            Console.WriteLine("Finished reading from file: " + strFileIn);

            listPersons.Sort();  // The crux of the program

            string strFileOut =
                strFileIn.Substring(0, strFileIn.IndexOf('.')) + "-graded.txt";
            using (StreamWriter fileOut = new StreamWriter(strFileOut))
            {
                foreach (Person cPerson in listPersons)
                {
                    string strOutput = cPerson.GetPersonData();
                    Console.WriteLine(strOutput);
                    fileOut.WriteLine(strOutput);
                }
            }
            Console.WriteLine("Finished: created " + strFileOut + ".  Press <Enter> to continue.");
            Console.ReadLine();
            return 0;
        }
    }
}