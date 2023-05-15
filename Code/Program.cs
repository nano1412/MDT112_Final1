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
    // TestPrint(townArray);
    CovidStatus(townArray);


    bool inLoop = true;
    int cityReportID = 0;
    int selfInfect = 0;
    int adjacentInfect = 0;
    while (inLoop)
    {
      string cityReport = Console.ReadLine();
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
          // call spread function here
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
      if (cityReport == "Outbreak" || cityReport == "Vaccinate" || cityReport == "Lock down")
      {
        CovidUpdate(ref townArray, cityReportID, selfInfect, adjacentInfect);
        CovidStatus(townArray);
      }
    }
  }

  static void Input(ref Town[] townArray)
  {
    for (int thisTownID = 0; thisTownID < townArray.Length; thisTownID++)
    {
      int addAdjacentTownID;
      string name = Console.ReadLine();
      int numOfAdjacentTown = int.Parse(Console.ReadLine());
      int[] adjacentTownID = new int[numOfAdjacentTown];
      SetupArray(ref adjacentTownID);
      for (int currentAdjacentTown = 0; currentAdjacentTown < adjacentTownID.Length; currentAdjacentTown++)
      {
        addAdjacentTownID = int.Parse(Console.ReadLine());

        if (addAdjacentTownID > townArray.Length || addAdjacentTownID == thisTownID || addAdjacentTownID < 0 || adjacentTownID.Contains(addAdjacentTownID))
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
  static void SetupArray(ref int[] array)
  {
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = -1;
    }
  }

  static void CovidStatus(Town[] town)
  {
    for (int thisTownID = 0; thisTownID < town.Length; thisTownID++)
    {
      Console.WriteLine("{0} {1} {2}", thisTownID, town[thisTownID].name, town[thisTownID].covidRate);
    }
  }

  static void TestPrint(Town[] town)
  {
    for (int thisTownID = 0; thisTownID < town.Length; thisTownID++)
    {
      Console.WriteLine("Name: {0} ID: {1}", town[thisTownID].name, thisTownID);
      Console.WriteLine("adjacent town:");
      for (int currentAdjacentTown = 0; currentAdjacentTown < town[thisTownID].adjacentTownID.Length; currentAdjacentTown++)
      {
        int currentAdjacentTownID = town[thisTownID].adjacentTownID[currentAdjacentTown];
        Console.WriteLine("{0} ID: {1} Name:{2}", currentAdjacentTown, currentAdjacentTownID, town[currentAdjacentTownID].name);
      }
      Console.WriteLine("---------------------------");
    }
  }

  static void CovidUpdate(ref Town[] townArray, int cityReportID, int selfInfect, int adjacentInfect)
  {
    townArray[cityReportID].covidRate += selfInfect;
    Clamp(ref townArray[cityReportID].covidRate, 0, 3);
    for (int currentAdjacentTownID = 0; currentAdjacentTownID < townArray[cityReportID].adjacentTownID.Length; currentAdjacentTownID++)
    {
      townArray[townArray[cityReportID].adjacentTownID[currentAdjacentTownID]].covidRate += adjacentInfect;
      Clamp(ref townArray[townArray[cityReportID].adjacentTownID[currentAdjacentTownID]].covidRate, 0, 3);
    }
  }
  static void Clamp(ref int num, int min, int max)
  {
    num = (num < min) ? min : (num > max) ? max : num;
  }
}