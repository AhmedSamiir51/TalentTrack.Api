using System.ComponentModel.DataAnnotations;

namespace TalentTrack.Core.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
