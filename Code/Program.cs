using System;

class Program
{
  struct Town
  {
    public string name;
    public int id;
    public int[] adjacentTownID;
    public int covidRate;
    public Town(string name,int id, int[] adjacentTownID)
    {
      this.name = name;
      this.id = id;
      this.adjacentTownID = adjacentTownID;
      this.covidRate = 0;
    }
  }
  static void Main(string[] args)
  {
    int numOfTown = int.Parse(Console.ReadLine());
    Town[] townArray = new Town[numOfTown];
    Input(ref townArray);
    TestPrint(townArray);


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
      townArray[thisTownID] = new Town(name,thisTownID, adjacentTownID);
    }

  }
  static void SetupArray(ref int[] array){
    for(int i = 0; i < array.Length;i++){
      array[i] = -1;
    }
  }

  static void TestPrint(Town[] town){
    for(int thisTown = 0; thisTown < town.Length;thisTown++){
      Console.WriteLine("Name: {0} ID: {1}",town[thisTown].name,town[thisTown].id);
      Console.WriteLine("adjacent town:");
      for(int currentAdjacentTown = 0; currentAdjacentTown < town[thisTown].adjacentTownID.Length; currentAdjacentTown++){
        Console.WriteLine("{0} ID: {1} Name:{2}",currentAdjacentTown,town[thisTown].adjacentTownID[currentAdjacentTown],town[town[thisTown].adjacentTownID[currentAdjacentTown]].name);
      }
      Console.WriteLine("---------------------------");
    }

  }
}