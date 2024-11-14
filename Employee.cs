using System;
using System.Collections.Immutable;

namespace Homework5
{
    /* Class invarients: 
     * - The total hours the employee has worked must always be non-negative.
     * - The name of the employee should never be null
     * - Skill proficienty should never be negative number
     * - Task completion time should always be non-negative
     * - Once the task is marked complete, its completion status should remain constant
     */
    public class Employee
    {
        private string _name;
        private int _hours;
        private ImmutableDictionary<Skill, int> _skills;

        public Employee(string name, ImmutableDictionary<Skill, int> skills)
        {
            this._name = name;
            this._hours = 0;
            _skills = skills != null ? skills.ToImmutableDictionary() : ImmutableDictionary<Skill, int>.Empty;
        }

        public int getHours
        {
            get { return _hours; }
        }

        public string getName
        {
            get { return _name; }
        }

        public ImmutableDictionary<Skill, int> getSkills
        {
            get { return _skills; }
        }

        public Employee(Employee other)
        {
            _name = other._name;
            _hours = other._hours;
            _skills = other._skills;
        }

        // Precondition: A task will never be null.
        /*Postcondition: 
            * - The method returns estimated total time to complete the task
            * - The totalTimeToComplete is calculated based on the task's base time and the proficiency 
            * of the employee in each skill required for the task.
            * - The totalTimeToComplete is a non-negative value, and the method ensures that any negative value
            * returned are replaced with zero. 
        */
        public int estimateCompletionTime(Task task)
        {
            int totalTimeToComplete = task.BaseTime;

            foreach (KeyValuePair<Skill, int> taskSkill in task.Skills)
            {
                int employeeProficiency = _skills.GetValueOrDefault(taskSkill.Key, 0);
                totalTimeToComplete += taskSkill.Key.CalculateCost(task.BaseTime, taskSkill.Value, employeeProficiency);
            }

            return totalTimeToComplete;
        }

        // Precondition: A task will never be null.
        /*Postcondition: 
         * - The completion of the task is updated and marked as complete.
         * - the total hours worked by the employee are updated based on totalTimeToComplete
         * - The proficiency of the employee skills is updated based on the skills required for the task.
         */
        public void DoTask(Task task)
        {
            if (task.IsComplete)
            {
                return;
            }

            int totalTimeToComplete = task.BaseTime;
            var updateSkills = new Dictionary<Skill, int>(_skills);

            foreach (KeyValuePair<Skill, int> taskSkill in task.Skills)
            {
                int proficiency = updateSkills.GetValueOrDefault(taskSkill.Key, 0);
                int taskDifficulty = taskSkill.Value;

                totalTimeToComplete += taskSkill.Key.CalculateCost(task.BaseTime, taskSkill.Value, proficiency);
                // Update Employee skill proficiency
                if (taskDifficulty > proficiency)
                {
                    updateSkills[taskSkill.Key] = Math.Max(proficiency + 1, taskDifficulty);
                }
            }
            _hours += totalTimeToComplete;
            task.MarkAsComplete();
        }
    }
}
