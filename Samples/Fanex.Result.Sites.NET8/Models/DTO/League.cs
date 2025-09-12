namespace Fanex.Result.Sites.NET8.Models
{
    using System;

    /// <summary>
    /// The league DTO.
    /// </summary>
    public class League
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime? EventDate { get; set; }
    }
}