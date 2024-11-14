using System;
using System.Collections.Immutable;

namespace Homework5;

public class DriverClass
{
	static void Main(string[] args)
	{
		//Creating at least three skills, at least one for each difficulty curve.
		Skill skill1 = new Skill("coding", "coding description");
		Skill skill2 = new Skill("reading", "reading description");
		Skill skill3 = new Skill("writing", "writing description");
		Skill skill4 = new Skill("typing", "tuyping description");


		ImmutableDictionary<Skill, int> _skills1 = new Dictionary<Skill, int> { { skill1, 2 } }.ToImmutableDictionary();
		ImmutableDictionary<Skill, int> _skills2 = new Dictionary<Skill, int> { { skill2, 3 } }.ToImmutableDictionary();
		ImmutableDictionary<Skill, int> _skills3 = new Dictionary<Skill, int> { { skill3, 5 } }.ToImmutableDictionary();
		ImmutableDictionary<Skill, int> _skills4 = new Dictionary<Skill, int> { { skill4, 7 } }.ToImmutableDictionary();

		//Creating at least three Tasks, each with at least one skll 
		Task task1 = new Task("Task1", "task1 Description", 2, _skills1);
        Task task2 = new Task("task2", "task2 Description", 1, _skills2);
        Task task3 = new Task("task3", "task3 Description", 4, _skills3);
        Task task4 = new Task("task4", "task4 Description", 6, _skills4);

		List<Task> tasks = new List<Task>();

		tasks.Add(task1);
        tasks.Add(task2);
        tasks.Add(task3);
        tasks.Add(task4);

		//Creating an empty Company
        Company company = new Company("Company");
		//Reading in a file. 
		string fileName = "../../../Input.txt";

		// Applying the IOrgChange to the Company.
		List<IOrgChange> orgChanges = new List<IOrgChange>();
		using (StreamReader read = new StreamReader(fileName))
		{
			string line;
			while ((line = read.ReadLine()) != null)
			{
				string[] inputs = line.Split(' ');
				if (inputs[0] == "h")
				{
                    orgChanges.Add(new HireOperation(inputs[1]));
                } else if (inputs[0] == "t" ) {
					if (inputs.Length > 2)
					{
						orgChanges.Add(new TransferOperation(inputs[1], inputs[2]));
                    } else
					{
						orgChanges.Add(new TransferOperation(inputs[1], null));
					}

                } else if (inputs[0] == "c")
				{
					orgChanges.Add(new CreateTeamOperation(inputs[1]));
				}

			}
		}

		for (int i = 0; i < orgChanges.Count; i++) {
			orgChanges[i].Apply(company);
			List<Team> teams = company.GetTeams();

			int taskCompletionTime = 0;
			for(int j = 0; j < teams.Count; j++)
			{
				taskCompletionTime += EstimateTastCompletion(teams[j], tasks);
			}
			//Printing out each Team's cost estimate for each task.
			Console.WriteLine("It takes " + taskCompletionTime + " to complete all tasks");
			Console.WriteLine(company);
		}
    }

	// This is a method that calculates each Team's cost estiamte for each Task.
	private static int EstimateTastCompletion(Team team, List<Task> tasks)
	{
		int totalCompletionTime = 0;

		for (int i = 0; i < tasks.Count; i++)
		{
			totalCompletionTime += team.EstimateCompletionTime(tasks[i]);
        }
		return totalCompletionTime;
    }
}

