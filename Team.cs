using System;
namespace Homework5
{
    /* Class Invarients:
     * - The _name field of a Team instance must always be non-null.
     * - The _employees list of a Team instace must always be non-null.
     * - All employees within the _employees list of a Team instance must be unique. No two employees in the 
     * list should refer to the same instance.
     * - Each employee within the _employee list of a Team instance must be non-null.
     */
	public class Team
	{
        private string _name;
        List<Employee> _employees;

        public Team(string name)
        {
            this._name = name;
            this._employees = new List<Employee>();
        }

        public string GetName
        {
            get { return _name; }
        }
        
        public List<Employee> GetEmployees
        {
            get { return _employees; }
        }

        // Precondition: employee is not null.
        /* Postcondition: Adds the provided employee to the list of employees (_employees) associated with the current instance of the Company class.
         * If the provided employee is null, the method throws an ArgumentNullException.
         * After the execution of this method, the provided employee will be added to the list of employees maintained by the company.
         */
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        // Precondition: name is not null or empty.
        /* Postcondition: Removes the employee with the specified name from the list of employees (_employees) associated with 
         * the current instance of the Company class.
         * If the provided name is null or empty, the method throws an ArgumentNullException or ArgumentException accordingly.
         * If no employee with the specified name is found in the list, the method does nothing.
         * After the execution of this method, if an employee with the specified name exists in the list, it will be removed.
         */
        public void RemoveEmployee(string name)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].getName == name)
                { 
                    _employees.RemoveAt(i);
                }
            }

        }

        // Precondition: task is not null.
        /* Postcondition: Calculates and returns the estimated completion time for the provided task.
         * The estimated completion time is calculated by summing up the base time of the task and the estimated completion time 
         * contributed by each employee assigned to the task.
         * If the provided task is null, the method throws an ArgumentNullException.
         * The method does not modify the state of the current instance.
         */
        public int EstimateCompletionTime(Task task)
        {
            int totalTimeToComplete = task.BaseTime;
            foreach (Employee employeeI in _employees)
            {
                totalTimeToComplete += employeeI.estimateCompletionTime(task);
            }
            return totalTimeToComplete;
        }

        // Precondition: other is not null.
        /* Postcondition: Initializes a new instance of the Team class by creating a deep copy of the provided Team object (other).
         * The new Team instance has the same name (_name) as the provided Team.
         * Each employee in the provided Team's employee list (_employees) is deep-copied, and the copied employees are added to the new Team's employee list.
         * The method ensures that modifying the state of the new Team instance or its employees does not affect the original Team or its employees.
         */
        public Team(Team other)
        {
            _name = other._name;

            _employees = new List<Employee>();

            foreach (Employee otherEmployee in other._employees)
            {
                Employee newEmployee = new Employee(otherEmployee);
                _employees.Add(newEmployee);
            }
        }
    }
}

