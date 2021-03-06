﻿using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Dto
{
    public class GenreDto
    {
        public byte id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}