using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using LearningForAbp.Users;

namespace LearningForAbp.Tasks
{
    public class Task : Entity, IHasCreationTime
    {
        public long? AssignedPersonId { get; set; }

        [ForeignKey("AssignedPersonId")]
        public virtual User AssignedPerson { get; set; }

        [Required]
        [MaxLength]
        public string Title { get; set; }

        [Required, MaxLength]
        public string Description { get; set; }

        public TaskState State { get; set; }

        public DateTime CreationTime { get; set; } 

        public Task()
        {
            this.State  = TaskState.Open;
            this.CreationTime = Clock.Now;
        }

        public Task(string title, string description = null) : this()
        {
            Title = title;
            Description = description;
        }
    }

    public enum TaskState
    {
        Open = 0,
        Completed = 1
    }
}
