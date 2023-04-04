using System;
using static System.Console;
using System.Globalization;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;

public class Contestant
{
    private char talentCode;
    public char TalentCode
    {
        get { return talentCode; }
        set
        {
            if (Array.IndexOf(talentCodes, value) >= 0)
            {
                talentCode = value;
            }
            else
            {
                talentCode = 'I';
            }

            AssignTalent(talentCode);
        }
    }

    private void AssignTalent(char talentCodeValue)
    {
        int index = Array.IndexOf(talentCodes, talentCodeValue);
        if (index >= 0)
        {
            talent = talentStrings[index];
        }
        else
        {
            talent = "Invalid";
        }
    }

    private string talent;
    public string Talent
    {
        get { return talent; }
    }

    public string Name { get; set; }
    public int Fee { get; set; }
    public int Age { get; set; }
    public static char[] talentCodes = { 'S', 'D', 'M', 'O' };
    public static string[] talentStrings = { "Singing", "Dancing", "Musical instrument", "Other" };
}
public class ChildContestant : Contestant
{
    //Child contestants are 12 years old and younger, and their entry fee is $15.
    public int fee; 
    new public int Fee
    //set the entry fee field to the correct value

    {
        get {
            fee = 15;
            return fee; 
        } 
    }
    public override string ToString()
        //override the ToString() method to return a string that includes all the contestant data, including the age category and the entry fee.
    {
        return "Child Contestant " + Name + " " + TalentCode + "   Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
        // Child Contestant Joeph S   Fee $15.00
    }
}
public class TeenContestant : Contestant
{
    //Teen contestants are between 13 and 17 years old, inclusive, and their entry fee is $20.
    public int fee;
    new public int Fee
    //set the entry fee field to the correct value

    {
        get
        {
            fee = 20;
            return fee;
        }
    }
    //override the ToString() method to return a string that includes all the contestant data, including the age category and the entry fee
    public override string ToString()
    {
        return "Teen Contestant " + Name + " " + TalentCode + "   Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
    }
    //Teen Contestant Sara M   Fee $20.00
}

public class AdultContestant : Contestant
{
    //Adult contestants are 18 years old and older, and their entry fee is $30
    public int fee;
    new public int Fee
    //set the entry fee field to the correct value
    {
        get
        {
            fee = 30;
            return fee;
        }
    }
    //override the ToString() method to return a string that includes all the contestant data, including the age category and the entry fee
    public override string ToString()
    {
        return "Adult Contestant " + Name + " " + TalentCode + "   Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
    }
    //Adult Contestant Joy D   Fee $30.00
}
/*the output from ToString() should be displayed in the following format:

Child Contestant Joeph S   Fee $15.00
Teen Contestant Sara M   Fee $20.00
Adult Contestant Joy D   Fee $30.00
*/

class GreenvilleRevenue
{
    public static string nl = Environment.NewLine;

    static void Main()
    {
        const int MIN_CONTESTANTS = 0;
        const int MAX_CONTESTANTS = 30;
        const int MIN_AGE = 1;
        const int MAX_AGE = 100;
        const double ENTRANCE_FEE = 25;
        string nl = Environment.NewLine;

        int numContestantsThisYear;
        char[] talentCodes = { 'S', 'D', 'M', 'O' };
        string[] talentStrings = { "Singing", "Dancing", "Musical instrument", "Other" };
        int[] talentCounts = { 0, 0, 0, 0 };
        double revenue;

        numContestantsThisYear = getContestantNumber("this", MIN_CONTESTANTS, MAX_CONTESTANTS);

        revenue = numContestantsThisYear * ENTRANCE_FEE;
        WriteLine("Revenue expected this year is {0}", revenue.ToString("C", CultureInfo.GetCultureInfo("en-US")));

        Contestant[] contestants = new Contestant[numContestantsThisYear-1];
        getContestantData(numContestantsThisYear, contestants, talentCodes, talentStrings, talentCounts,MIN_AGE,MAX_AGE);
        Contestant[] detailedContestants = new Contestant[numContestantsThisYear - 1];
        InstContestantType(numContestantsThisYear, contestants, detailedContestants);

        getLists(numContestantsThisYear, talentCodes, talentStrings, detailedContestants, talentCounts);

    }

    public static int getContestantNumber(string competitionYear, int min, int max)
    {
        int numContestants = -1;
        while (numContestants < min || numContestants > max)
        {
            Write($"Enter number of contestants {competitionYear} year >> ");
            if (int.TryParse(ReadLine(), out numContestants))
            {
                if (numContestants < min || numContestants > max)
                {
                    WriteLine($"invalid input. Number of contestants must be between {min} and {max}. Please try again.");
                }
            }
            else
            {
                WriteLine("invalid input. must enter an integer");
                numContestants = -1; // This is necessary to reinitialize the numContestants back to -1 as TryParse will default the int variable to 0 
            }
        }
        return numContestants;
    }


    public static void displayRelationship(int numContestantsThisYear, int numContestantsLastYear)
    {
        if (numContestantsThisYear > numContestantsLastYear)
        {
            WriteLine("The competition is bigger than ever!");
        }
        else if (numContestantsThisYear < numContestantsLastYear)
        {
            WriteLine("A tighter race this year! Come out and cast your vote!");
        }
        else
        {
            WriteLine("The competition is the same as last year - BORING!");
        }
    }
    public static void getContestantData(int numThisYear, Contestant[] contestants, char[] talentCodes, string[] talentCodesStrings, int[] counts, int ageMin, int ageMax)
    {
        for (int i = 0; i < numThisYear; i++)
        {
            Contestant contestant = new Contestant();

            Write("Enter contestant's name: ");
            string nameInput = ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                WriteLine("invalid input. Please try again.");
                i--; // Decrement i so that the loop will ask for the same index again
                continue;
            }
            else if (nameInput.Length > 15)
            {
                WriteLine("invalid input. Name cannot be more than 15 characters.");
                i--; // Decrement i so that the loop will ask for the same index again
                continue;
            }
            contestant.Name = nameInput;

            bool validTalentCode = false;
            do
            {
                WriteLine("Talent codes are:");
                for (int a = 0; a < talentCodes.Length; a++)
                {
                    WriteLine($"  {talentCodes[a]}   {talentCodesStrings[a]}");
                }

                Write("Enter talent code >> ");

                if (char.TryParse(ReadLine(), out char tCodeInput))
                {
                    if (Array.IndexOf(talentCodes, tCodeInput) >= 0)
                    {
                        contestant.TalentCode = tCodeInput;
                        validTalentCode = true;
                    }
                    else
                    {
                        WriteLine("invalid talent code. Please try again.");
                    }
                }
                else
                {
                    WriteLine("invalid talent code. Please try again.");
                }
            } while (!validTalentCode);
                int enteredAge = -1;
                while (enteredAge < ageMin || enteredAge > ageMax)
                {
                    Write($"Enter age of contestant >> ");
                    if (int.TryParse(ReadLine(), out enteredAge))
                    {
                        if (enteredAge < ageMin || enteredAge > ageMax)
                        {
                            WriteLine($"invalid input. Number of contestants must be between {ageMin} and {ageMax}. Please try again.");
                        }
                    }
                    else
                    {
                        WriteLine("invalid input. must enter an integer");
                        enteredAge = -1; // This is necessary to reinitialize the numContestants back to -1 as TryParse will default the int variable to 0 
                    }
                }
                contestant.Age = enteredAge;

                contestants[i] = contestant;

            //int talentIndex = Array.IndexOf(talentCodes, contestant.TalentCode);
            //counts[talentIndex]++;
        }
    }

    public static void getLists(int numContestants, char[] talentCodes, string[] talentStrings, Contestant[] detailedContestants, int[] counts)
    {
        WriteLine("The types of talent are:");

        for (int i = 0; i < talentCodes.Length; ++i)
        {
            WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
        }
        WriteLine("Enter a talent code to see a list of contestants, or enter Z to quit");

        while (true)
        {
            string input = ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter the letter of a valid talent code or Z to quit.");
                continue;
            }

            if (!char.TryParse(input, out char tCodeInput))
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter only one character.");
                continue;
            }

            if (Array.IndexOf(talentCodes, tCodeInput) >= 0 || tCodeInput == 'Z')
                if (tCodeInput == 'Z')
                {
                    break;
                }

            if (Array.IndexOf(talentCodes, tCodeInput) >= 0)
            {
                WriteLine($"Contestants with talent {talentStrings[Array.IndexOf(talentCodes, tCodeInput)]} are:");
                for (int j = 0; j < numContestants; j++)
                {
                    if (detailedContestants[j].TalentCode == tCodeInput)
                    {
                        WriteLine($"{detailedContestants[j].Name}");
                    }
                }
            }
            else
            {
                WriteLine($"Invalid input.{nl}" +
                    "The types of talent are:");
                for (int i = 0; i < talentCodes.Length; ++i)
                {
                    WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
                }
                WriteLine("Please enter the letter of a valid talent code or Z to quit.");
            }

            WriteLine("The types of talent are:");
            for (int i = 0; i < talentCodes.Length; ++i)
            {
                WriteLine("{0,-25}{1,5}", talentStrings[i], counts[i]);
            }
            WriteLine("Enter a talent code to see a list of contestants, or enter Z to quit");
        }
    }
    public static void InstContestantType(int numContestants, Contestant[] contestants, Contestant[] detailedContestants)
    {
        for (int j = 0; j < numContestants; j++)
        {
            if (contestants[j].Age <= 12)
            {
                ChildContestant contestant = new ChildContestant();

                detailedContestants[i] = contestant;
            }
            else if (contestants[j].Age >= 13 && contestants[j].Age <= 17 )
            {
                TeenContestant contestant = new TeenContestant();

                contestants[i] = contestant;
            }
            else if (contestants[j].Age >= 18)
            {
                AdultContestant contestant = new AdultContestant();

               
            }
            contestants[j] = contestant;
        }
    }


}
/*
 Instructions

Previously, you created a Contestant class for the Greenville Idol competition. The class includes a contestant’s name, talent code, and talent description. The competition has become so popular that separate contests with differing entry fees have been established for children, teenagers, and adults.

Now, modify your program so the Contestant class contains the field Fee that holds the entry fee for each category, and add get and set accessors.

Extend the Contestant class to create three subclasses: ChildContestant, TeenContestant, and AdultContestant. Child contestants are 12 years old and younger, and their entry fee is $15. Teen contestants are between 13 and 17 years old, inclusive, and their entry fee is $20. Adult contestants are 18 years old and older, and their entry fee is $30.

In each subclass, set the entry fee field to the correct value, and override the ToString() method to return a string that includes all the contestant data, including the age category and the entry fee. For example, the output from ToString() should be displayed in the following format:

Child Contestant Joeph S   Fee $15.00
Teen Contestant Sara M   Fee $20.00
Adult Contestant Joy D   Fee $30.00
In order to prepend the $ to currency values, the program will need to use the CultureInfo.GetCultureInfo method. In order to do this, include the statement using System.Globalization; at the top of your program and format the output statements as follows: WriteLine("This is an example: {0}", value.ToString("C", CultureInfo.GetCultureInfo("en-US")));

Step 2

Modify the GreenvilleRevenue program so that it performs the following tasks:

The program prompts the user for the number of contestants in this year’s competition, which must be between 0 and 30. The program continues to prompt the user until a valid value is entered.
The program prompts the user for the name, talent code, and age for each contestant. Along with the prompt for a talent code, display a list of valid categories. Based on the age entered for each contestant, create an object of the correct type (adult, teen, or child), and store it in an array of Contestant objects. Talent code categories should be displayed in the following format:
Talent codes are:
S   Singing
D   Dancing
M   Musical instrument
O   Other
After data entry is complete, display the total expected revenue, which is the sum of the entry fees for the contestants. The total expected revenue should be displayed in the following format:
Revenue expected this year is $65.00
After data entry is complete, display the valid talent categories and then continuously prompt the user for talent codes, and display all the data for all the contestants in each category (using the ToString method for each contestant). Display an appropriate message if the entered code is not a character or a valid code.
--------------------------------------------
Test Case:
Correct program execution

Input:
3
Joe
S
10
Sara
M
15
Joy
D
18
S
D
Z

Expected Output:
Talent codes are:
S Singing
D Dancing
M Musical instrument
O Other
Revenue expected this year is $65.00
Child Contestant Joe S Fee $15.00
Adult Contestant Joy D Fee $30.00
--------------------------------------------
Unit Test
ChildContestant class defined correctly
Test Contents
[TestFixture]
public class ChildContestantClassTest
{
  [Test]
  public void ChildContestantTest()
  {
    Assert.IsTrue(typeof(ChildContestant).IsSubclassOf(typeof(Contestant)),
    "`ChildContestant` class should inherit from `Contestant` class");
  }

  [Test]
  public void ChildContestantTest2()
  {
    double fee = 15;
    ChildContestant c = new ChildContestant();
    Assert.AreEqual(fee, c.Fee);
    c.Name = "Kevin";
    c.TalentCode = 'S';
    string output = c.ToString();
    string expected = "Child Contestant Kevin S   Fee " + fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
    Assert.AreEqual(output, expected);
  }
}
--------------------------------------------
Unit Test 
TeenContestant class defined correctly
Test Contents
[TestFixture]
public class TeenContestantClassTest
{
  [Test]
  public void TeenContestantTest()
  {
    Assert.IsTrue(typeof(TeenContestant).IsSubclassOf(typeof(Contestant)),
    "`TeenContestant` class should inherit from `Contestant` class");
  }

  [Test]
  public void TeenContestantTest2()
  {
    double fee = 20;
    TeenContestant c = new TeenContestant();
    Assert.AreEqual(fee, c.Fee);
    c.Name = "Ellen";
    c.TalentCode = 'M';
    string output = c.ToString();
    string expected = "Teen Contestant Ellen M   Fee " + fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
    Assert.AreEqual(output, expected);
  }
}
--------------------------------------------
Unit Test 
AdultContestant class defined correctly
Test Contents
[TestFixture]
public class AdultContestantClassTest
{
  [Test]
  public void AdultContestantTest()
  {
    Assert.IsTrue(typeof(AdultContestant).IsSubclassOf(typeof(Contestant)),
    "`AdultContestant` class should inherit from `Contestant` class");
  }

  [Test]
  public void AdultContestantTest2()
  {
    double fee = 30;
    AdultContestant c = new AdultContestant();
    Assert.AreEqual(fee, c.Fee);
    c.Name = "Kelly";
    c.TalentCode = 'D';
    string output = c.ToString();
    string expected = "Adult Contestant Kelly D   Fee " + fee.ToString("C", CultureInfo.GetCultureInfo("en-US"));
    Assert.AreEqual(output, expected);
  }
}

*/