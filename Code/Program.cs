using System;

class Program
{
  struct Town
  {
    public string name;
    public int[] adjacentTownID;
    public int covidRate;
    public Town(string name, int[] adjacentTownID)
    {
      this.name = name;
      this.adjacentTownID = adjacentTownID;
      this.covidRate = 0;
    }
  }
  static void Main(string[] args)
  {
    int numOfTown = int.Parse(Console.ReadLine());
    Town[] townArray = new Town[numOfTown];
    Input(ref townArray);
    CovidStatus(townArray);
    CovidBegin(ref townArray);
  }
  // 1.INPUT-----------------------------------------------------------------------
  static void Input(ref Town[] townArray)
  {
    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      string name = Console.ReadLine();
      int numOfAdjacentTown = int.Parse(Console.ReadLine());
      int[] adjacentTownID = new int[numOfAdjacentTown];
      SetupIntArrayto_Neg1(ref adjacentTownID); // for case of input 0

      for (int currentAdjacentTown = 0; currentAdjacentTown < adjacentTownID.Length; currentAdjacentTown++)
      {
        int addAdjacentTownID = int.Parse(Console.ReadLine());
        bool isInvalidInput = addAdjacentTownID > townArray.Length || addAdjacentTownID == thisTownID || addAdjacentTownID < 0 || adjacentTownID.Contains(addAdjacentTownID);
        if (isInvalidInput)
        {
          Console.WriteLine("Invalid ID");
          currentAdjacentTown--;
        }
        else
        {
          adjacentTownID[currentAdjacentTown] = addAdjacentTownID;
        }
      }
      townArray[thisTownID] = new Town(name, adjacentTownID);
    }
  }

  // 2.UPDATE STATUS---------------------------------------------------------------
  static void CovidStatus(Town[] townArray)
  {
    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      Console.WriteLine("{0} {1} {2}", thisTownID, townArray[thisTownID].name, townArray[thisTownID].covidRate);
    }
  }

  static void TestPrint(Town[] townArray)
  {
    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      Console.WriteLine("Name: {0} ID: {1}", townArray[thisTownID].name, thisTownID);
      Console.WriteLine("adjacent town:");
      for (int currentAdjacentTown = 0; currentAdjacentTown < townArray[thisTownID].adjacentTownID.Length; currentAdjacentTown++)
      {
        int currentAdjacentTownID = townArray[thisTownID].adjacentTownID[currentAdjacentTown];
        Console.WriteLine("{0} ID: {1} Name:{2}", currentAdjacentTown, currentAdjacentTownID, townArray[currentAdjacentTownID].name);
      }
      Console.WriteLine("---------------------------");
    }
  }

  // 3.COVID BEGIN-----------------------------------------------------------------
  static void CovidBegin(ref Town[] townArray)
  {
    bool inLoop = true;
    string cityReport;
    int cityReportID = 0;
    int selfInfect = 0;
    int adjacentInfect = 0;

    while (inLoop)
    {
      cityReport = Console.ReadLine();
      if (cityReport == "Outbreak" || cityReport == "Vaccinate" || cityReport == "Lock down")
      {
        cityReportID = int.Parse(Console.ReadLine());
      }
      switch (cityReport)
      {
        case "Outbreak":
          selfInfect = 2;
          adjacentInfect = 1;
          break;
        case "Vaccinate":
          selfInfect = -4;
          adjacentInfect = 0;
          break;
        case "Lock down":
          selfInfect = -1;
          adjacentInfect = -1;
          break;
        case "Spread":
          Spread(ref townArray);
          selfInfect = 0;
          adjacentInfect = 0;
          break;
        case "Exit":
          inLoop = false;
          break;
        default:
          Console.WriteLine("Invalid");
          selfInfect = 0;
          adjacentInfect = 0;
          break;
      }
      if (cityReport == "Outbreak" || cityReport == "Vaccinate" || cityReport == "Lock down" || cityReport == "Spread")
      {
        CovidUpdate(ref townArray, cityReportID, selfInfect, adjacentInfect);
        CovidStatus(townArray);
      }
    }
  }

  // 4.COVID UPDATE----------------------------------------------------------------
  static void CovidUpdate(ref Town[] townArray, int cityReportID, int selfInfect, int adjacentInfect)
  {
    townArray[cityReportID].covidRate += selfInfect;
    Clamp(ref townArray[cityReportID].covidRate, 0, 3);
    for (int currentAdjacentTown = 0; currentAdjacentTown < townArray[cityReportID].adjacentTownID.Length; currentAdjacentTown++)
    {
      int currentAdjacentTownID = townArray[cityReportID].adjacentTownID[currentAdjacentTown];
      townArray[currentAdjacentTownID].covidRate += adjacentInfect;
      Clamp(ref townArray[currentAdjacentTownID].covidRate, 0, 3);
    }
  }

  static void Spread(ref Town[] townArray)
  {
    bool[] isGotInfect = new bool[townArray.Length];
    SetupBoolArrayto_False(ref isGotInfect);
    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      for (int currentAdjacentTown = 0; currentAdjacentTown < townArray[thisTownID].adjacentTownID.Length; currentAdjacentTown++)
      {
        int currentAdjacentTownID = townArray[thisTownID].adjacentTownID[currentAdjacentTown];
        if (townArray[thisTownID].covidRate < townArray[currentAdjacentTownID].covidRate)
        {
          isGotInfect[thisTownID] = true;
        }
      }
    }

    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      if (isGotInfect[thisTownID])
      {
        townArray[thisTownID].covidRate += 1;
      }
    }
  }

  // 6.UTILITIES-------------------------------------------------------------------
  static void Clamp(ref int num, int min, int max)
  {
    num = (num < min) ? min : (num > max) ? max : num;
  }

  static void SetupIntArrayto_Neg1(ref int[] array)
  {
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = -1;
    }
  }

  static void SetupBoolArrayto_False(ref bool[] array)
  {
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = false;
    }
  }
}