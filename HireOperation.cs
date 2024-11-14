using System;
using System.Collections.Immutable;

namespace Homework5;

public interface IOrgChange
{
    /* Precondition: Company object is not null and Company is in modifiable state. 
	* Postcondition: Company should be in valid state.
	*/
    void Apply(Company company);
}

public class HireOperation : IOrgChange
{
	private string _personName;
	private IRandomNumberGenerator _randomNumberGenerator;

    /* Precondition: Company parameter should not be null 
	 * Postcondition: After applying the organizational change, a new employee with the specified name and skills is registered 
	 * with the company
	 */
    public void Apply(Company company)
	{

		RandomizedSkill randomizedSkill = new RandomizedSkill("RandomSkill", "Description", _randomNumberGenerator);
		ImmutableDictionary<Skill, int> _skills = new Dictionary<Skill, int> { { randomizedSkill, 2 } }.ToImmutableDictionary();
		Employee employee = new Employee(_personName, _skills);
		company.RegisterEmployee(employee);
	}

    /* Precondition: This is a constructor that takes in a parameter personName which is an employee name.
	 * Postcondition: a new instance of the class is created with the name and random number generator initialized. 
	 */
    public HireOperation(string personName)
	{
		_personName = personName;
		_randomNumberGenerator = new RandomNumberGenerator();
	}
};

public class CreateTeamOperation : IOrgChange
{
	private string _teamName;

    /* Precondition: The company parameter should not be null and teamName should also not be null.
	 * Postcondition: After applying the organizational change, a new team with the specified name is created 
	 * and added to the company.
	 */
    public void Apply(Company company)
	{
		Team team = new Team(_teamName);
		company.CreateTeam(team);
	}

    /* Precondition: This is a constructor that takes in a parameter teamName which is a team name.
	 * Postcondition: a new instance of the class is created with the name initialized.  
	 */
    public CreateTeamOperation(string teamName) 
	{
		_teamName = teamName;
	}
}

public class TransferOperation : IOrgChange
{
	private string _employeeName;
	private string? _teamName;

    /* Precondition: This is a constructor that takes in a parameter employeeName and teamName.
	 * Postcondition: a new instance of the class is created with the employeeName and teamName initialized. 
	 */
    public TransferOperation(string employeeName, string teamName)
	{
		_employeeName = employeeName;
		_teamName = teamName;
	}

    /* Precondition: The company parameter should not be null and employeeName should not be null.
	 * Postcondition: If the employee is found in the company, they are removed from all teams.
	 * If _teamName is not null, a new team with the specified name is created and the employee is added to it.
	 */
    public void Apply(Company company)
	{
		Employee employee = company.GetEmployee(_employeeName);
		if (employee == null)
		{
			throw new InvalidOperationException("Employee not found.");
		}

		company.RemoveEmployeeFromAllTeams(_employeeName);

		if(_teamName != null)
		{
			Team team = new Team(_teamName);
			team.AddEmployee(employee);
			company.CreateTeam(team);
		} 
	}
}


