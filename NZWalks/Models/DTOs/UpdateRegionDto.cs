﻿namespace NZWalks.Models.DTOs
{
    public class UpdateRegionDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
