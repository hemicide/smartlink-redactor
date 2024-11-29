using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redactor.Domain.Entities;

[Table("smartlinks")]
public partial class Smartlinks
{
    [Column("link")]
    public string Link { get; set; } = null!;

    [Column("rules", TypeName = "jsonb")]
    public string Rules { get; set; } = null!;

    [Key]
    [Column("id")]
    public Guid Id { get; set; }
}
