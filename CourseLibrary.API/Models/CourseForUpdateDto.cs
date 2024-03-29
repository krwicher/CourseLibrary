﻿using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
        [Required]
        public override string Description { get; set; }
    }
}