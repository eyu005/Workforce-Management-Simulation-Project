using System;
using System.Xml.Linq;

namespace Homework5
{
    /* Class invarients:
     * - No two employees should have the same name within the company. 
     * - No two teams should have the same name within the company.
     * - When an employyee is removed from the company, they should also be removed from any team they 
     * were part of
     * - Every employee within the company should either be a member of one or more teams or not a member of 
     * any team at all. 
     * - The identity of the company, represented by its name, should remain constant throughout its lifetime. 
     */

    public class Company
	{
        private string _name;
        private List<Employee> _employees;
        private List<Team> _teams;

        // Precondition: It takes a string parameter name from the user
        /* Postcondition: It creates an instance of a class Company with the
         * given name and creates an empty _employees and _teams list. 
        */
        public Company(string name)
        {
            _name = name;
            _employees = new List<Employee>();
            _teams = new List<Team>();
        }

        // Precondition: It takes an employee object
        /* Postcondition: It modifies the state of the current instance and adds the
         * provided Employee to the _employees list 
         */
        public void RegisterEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        // Precondition: It takes an Employee object
        /* Postcondition: It modifies the state of the current instance. It removes the
         * provided Employee from the _employee list. It removes the provided
         * Employee from any teams associated with the company.
         */
        public void FireEmployee(string employeeName)
        {
            int index = 0;

            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].getName == employeeName)
                {
                    index = i;
                }
            }
            _employees.RemoveAt(index);
        }

        // Precondition: It takes a Team object
        /* Postcondition: It modifies the state of the current instance. Adds the
         * provided Team to the _teams list.
         */
        public void CreateTeam(Team team)
        {
            _teams.Add(team);
        }

        // Precondition: It takes a pointer to a Team object
        /* Postcondition: It modifies the state of the current instance. Removes the
         * provided Team from the _team list.
         */
        public void CancelTeam(Team team)
        {
            int index = 0;

            for (int i = 0; i < _teams.Count; i++)
            {
                if (_teams[i].GetName == team.GetName)
                {
                    index = i;
                }
            }
            _teams.RemoveAt(index);
        }

        // Precondition: It takes a string representing name of the team to search for.
        /* Postcondition: it returns a Team object associated with the name. It does not modify the state
         * of the current instance.
         */
        public Team GetTeam(string name)
        {
            foreach (Team team in _teams)
            {
                if (team.GetName == name)
                {
                    return team;
                }
            }
            return null;
        }

        public List<Team> GetTeams()
        {
            return _teams;
        }

        // Precondition: It takes a string representing name of the Employee to search for.
        /* Postcondition: it returns a employee object associated wit the name. It does not modify the state
         * of the current instance.
         */
        public Employee GetEmployee(string name)
        {
            foreach (Employee employee in _employees)
            {
                if (employee.getName == name)
                {
                    return employee;
                }
            }
            return null;
        }

        // Precondition: other is not null. 
        /* Postcondition: it creates and returns a deep copy of the current employee object. It does not modify the state
         * of the current instance.
         */
        public Company(Company other)
        {
            _name = other._name;

            _employees = new List<Employee>();
            foreach (Employee employee in other._employees)
            {
                _employees.Add(new Employee(employee));
            }

            _teams = new List<Team>();

            foreach (Team team in other._teams)
            {
                _teams.Add(new Team(team));
            }
        }

        public void RemoveEmployeeFromAllTeams(string employeeName)
        {
            foreach (var team in _teams)
            {
                team.RemoveEmployee(employeeName);
            }
        }
    }
}

