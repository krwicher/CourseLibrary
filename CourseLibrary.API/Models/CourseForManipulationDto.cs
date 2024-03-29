﻿using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public abstract class CourseForManipulationDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public virtual string Description { get; set; }
    }
}